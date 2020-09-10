using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaleworldsProductSerialKeyVerifier
{
    public partial class frmVerifier : Form
    {
        private Scenario scenario;
        private ModProfile profile;
        private BackgroundWorker worker;
        private string serverIP;
        public frmVerifier(Scenario scenario, ModProfile profile)
        {
            InitializeComponent();
            this.scenario = scenario;
            this.profile = profile;
            Text += " - " + profile.DisplayName;
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
                case Scenario.nw:
                    serverIP = "213.202.240.107:8210";
                    break;
                case Scenario.wfas:
                    serverIP = "195.2.84.212";
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
}
