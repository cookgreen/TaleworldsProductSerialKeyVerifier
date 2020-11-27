using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TaleworldsProductSerialKeyVerifier
{
    public partial class frmVerifier : Form
    {
        private Scenario scenario;
        private ModProfile profile;
        private BackgroundWorker worker;
        private string serverIP;
        private const string TALEWORLDS_PRODUCT_SERIAL_KEY_FORMAT = "^([A-Z0-9]{4}-){3}[A-Z0-9]{4}$";
        public frmVerifier(Scenario scenario, ModProfile profile)
        {
            InitializeComponent();
            this.scenario = scenario;
            this.profile = profile;
            Text += " - " + profile.DisplayName;
            if (Regex.IsMatch(profile.Key, TALEWORLDS_PRODUCT_SERIAL_KEY_FORMAT))
            {
                string[] tokens = profile.Key.Split('-');
                txtSerialKeyTuple1.Text = tokens[0];
                txtSerialKeyTuple2.Text = tokens[1];
                txtSerialKeyTuple3.Text = tokens[2];
                txtSerialKeyTuple4.Text = tokens[3];
            }
        }

        private List<string> getGameServersList(int gameTypeIndex)
        {
            try
            {
                string gameType = null;
                if (gameTypeIndex == 0)
                {
                    gameType = "wb";
                }
                else if (gameTypeIndex == 1)
                {
                    gameType = "wfas";
                }
                else
                {
                    gameType = "unknown";
                }

                string reqUrl = string.Format("http://warbandmain.taleworlds.com/handlerservers.ashx?type=list&gametype={0}", gameType);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUrl);
                request.UserAgent = "UserAgent";
                request.Timeout = 120000;
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                string responseTxt = null;
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseTxt = reader.ReadToEnd();
                }

                string[] tokens = responseTxt.Split('|');
                return tokens.ToList();
            }
            catch {
                return new List<string>();
            }
        }

        private string getFirstServerByScenario(Scenario scenario)
        {
            string module = null;
            switch(scenario)
            {
                case Scenario.warband:
                    module = "Native";
                    break;
                case Scenario.nw:
                    module = "Napoleonic Wars";
                    break;
                case Scenario.vc:
                    module = "Viking Conquest";
                    break;
            }
            var servers = getGameServersList(0);
            foreach (var serverAddr in servers)
            {
                var serverInfo = fetchGameServerInfo(serverAddr);
                if (serverInfo != null && serverInfo.Module == module)
                {
                    return serverAddr;
                }
            }
            return null;
        }
        
        private MBServerInfo fetchGameServerInfo(string serverIPWithPort)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}",
                   serverIPWithPort));
                request.UserAgent = "UserAgent";
                request.Timeout = int.MaxValue;
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                string responseTxt = null;
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseTxt = reader.ReadToEnd();
                }

                XmlDocument document = new XmlDocument();
                document.LoadXml(responseTxt);
                var eleName = document.GetElementsByTagName("Name")[0];
                var eleModule = document.GetElementsByTagName("ModuleName")[0];
                var eleMap = document.GetElementsByTagName("MapName")[0];
                var eleMapType = document.GetElementsByTagName("MapTypeName")[0];
                var eleActivePlayer = document.GetElementsByTagName("NumberOfActivePlayers")[0];
                var eleMaxNumberPlayer = document.GetElementsByTagName("MaxNumberOfPlayers")[0];
                var eleDedicated = document.GetElementsByTagName("IsDedicated")[0];
                var eleHasPassword = document.GetElementsByTagName("HasPassword")[0];

                if (eleName != null)
                {
                    MBServerInfo serverInfo = new MBServerInfo();
                    serverInfo.Name = eleModule.InnerText;
                    serverInfo.Map = eleMap.InnerText;
                    serverInfo.MapType = eleMapType.InnerText;
                    serverInfo.Players = string.Format("{0}/{1}", eleActivePlayer.InnerText, eleMaxNumberPlayer.InnerText);
                    serverInfo.Dedicated = eleDedicated.InnerText;
                    serverInfo.HasPassword = eleHasPassword.InnerText;
                    return serverInfo;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string serialKey = string.Format("{0}-{1}-{2}-{3}",
                txtSerialKeyTuple1.Text, 
                txtSerialKeyTuple2.Text, 
                txtSerialKeyTuple3.Text,
                txtSerialKeyTuple4.Text);

            switch(scenario)
            {
                case Scenario.warband:
                    serverIP = getFirstServerByScenario(scenario);
                    break;
                case Scenario.nw:
                    serverIP = getFirstServerByScenario(scenario);
                    break;
                case Scenario.vc:
                    serverIP = getFirstServerByScenario(scenario);
                    break;
                case Scenario.wfas:
                    serverIP = getGameServersList(1)[0];
                    break;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://warbandmain.taleworlds.com/handlerservers.ashx?type=chkserial&serial={0}&ip={1}&gametype={2}",
               serialKey, serverIP, scenario.ToString()));
            request.UserAgent = "UserAgent";
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            string responseTxt = null;
            using (StreamReader reader = new StreamReader(stream))
            {
                responseTxt = reader.ReadToEnd();
            }
            e.Result = responseTxt;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnOK.Enabled = true;
            if (e.Result.ToString() == "-1" || e.Result.ToString() == "0|0")
            {
                MessageBox.Show("Verify failed!");
            }
            else
            {
                MessageBox.Show("Verify success!");
                profile.Start();
            }
        }

        private void txtSerialKeyTuple1_TextChanged(object sender, EventArgs e)
        {
            if (txtSerialKeyTuple1.Text.Length >= 4)
            {
                txtSerialKeyTuple1.Text = txtSerialKeyTuple1.Text.Substring(0, 4);
            }
            txtSerialKeyTuple2.Focus();
        }

        private void txtSerialKeyTuple2_TextChanged(object sender, EventArgs e)
        {
            if (txtSerialKeyTuple2.Text.Length >= 4)
            {
                txtSerialKeyTuple2.Text = txtSerialKeyTuple2.Text.Substring(0, 4);
            }
            txtSerialKeyTuple3.Focus();
        }

        private void txtSerialKeyTuple3_TextChanged(object sender, EventArgs e)
        {
            if (txtSerialKeyTuple3.Text.Length >= 4)
            {
                txtSerialKeyTuple3.Text = txtSerialKeyTuple3.Text.Substring(0, 4);
            }
            txtSerialKeyTuple4.Focus();
        }

        private void txtSerialKeyTuple4_TextChanged(object sender, EventArgs e)
        {
            if (txtSerialKeyTuple4.Text.Length >= 4)
            {
                txtSerialKeyTuple4.Text = txtSerialKeyTuple4.Text.Substring(0, 4);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSerialKeyTuple1.Text) ||
                string.IsNullOrEmpty(txtSerialKeyTuple2.Text) ||
                string.IsNullOrEmpty(txtSerialKeyTuple3.Text) ||
                string.IsNullOrEmpty(txtSerialKeyTuple4.Text))
            {
                MessageBox.Show("Please input a valid serialkey!");
            }
            else
            {
                btnOK.Enabled = false;
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
        }
    }

    public class MBServerInfo
    {
        public string Name { get; set; }
        public string Module { get; set; }
        public string Map { get; set; }
        public string MapType { get; set; }
        public string Players { get; set; }
        public string Dedicated { get; set; }
        public string HasPassword { get; set; }
    }
}
