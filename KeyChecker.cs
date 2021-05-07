using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaleworldsProductSerialKeyVerifier
{
    public class KeyChecker
    {
        private const string TALEWORLDS_PRODUCT_SERIAL_KEY_FORMAT = "^([A-Z0-9]{4}-){3}[A-Z0-9]{4}$";
        private int splitCharNum;
        private Scenario scenario;
        private BackgroundWorker keyOnlineCheckWorker;
        private MBServerInfoFetcher fetcher;

        public event Action<bool> KeyOnlineCheckFinished;

        public KeyChecker(int splitCharNum, Scenario scenario)
        {
            fetcher = new MBServerInfoFetcher();
            this.splitCharNum = splitCharNum;
            this.scenario = scenario;
            keyOnlineCheckWorker = new BackgroundWorker();
            keyOnlineCheckWorker.DoWork += KeyOnlineCheckWorker_DoWork;
            keyOnlineCheckWorker.RunWorkerCompleted += KeyOnlineCheckWorker_RunWorkerCompleted;
        }

        private void KeyOnlineCheckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool checkResult = !(e.Result.ToString() == "-1" || e.Result.ToString() == "0|0");
            KeyOnlineCheckFinished?.Invoke(checkResult);
        }

        private void KeyOnlineCheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string serverIP = null;
            string serialKey = e.Argument.ToString();

            switch (scenario)
            {
                case Scenario.warband:
                    serverIP = fetcher.GetFirstServerByScenario(scenario);
                    break;
                case Scenario.nw:
                    serverIP = fetcher.GetFirstServerByScenario(scenario);
                    break;
                case Scenario.vc:
                    serverIP = fetcher.GetFirstServerByScenario(scenario);
                    break;
                case Scenario.wfas:
                    serverIP = fetcher.GetGameServersList(1)[0];
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

        public bool Check(string serialKey)
        {
            return serialKey.Where(o => o == '-').Count() == splitCharNum &&
                Regex.IsMatch(serialKey, TALEWORLDS_PRODUCT_SERIAL_KEY_FORMAT);
        }

        public void CheckOnline(string serialKey)
        {
            keyOnlineCheckWorker.RunWorkerAsync(serialKey);
        }
    }
}
