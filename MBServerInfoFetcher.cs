using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TaleworldsProductSerialKeyVerifier
{
    public class MBServerInfoFetcher
    {
        internal List<string> GetGameServersList(int gameTypeIndex)
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
            catch
            {
                return new List<string>();
            }
        }

        internal string GetFirstServerByScenario(Scenario scenario)
        {
            string module = null;
            switch (scenario)
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
            var servers = GetGameServersList(0);
            foreach (var serverAddr in servers)
            {
                var serverInfo = FetchGameServerInfo(serverAddr);
                if (serverInfo != null && serverInfo.Module == module)
                {
                    return serverAddr;
                }
            }
            return null;
        }

        internal MBServerInfo FetchGameServerInfo(string serverIPWithPort)
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
    }
}
