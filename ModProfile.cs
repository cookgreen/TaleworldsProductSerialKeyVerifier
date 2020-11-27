using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaleworldsProductSerialKeyVerifier
{
    public class ModProfile
    {
        private string gameRunPath;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ModID { get; set; }
        public Scenario RequireDLC { get; set; }
        public string Key { get; set; }

        public ModProfile(string profileFullPath, string gameRunPath)
        {
            this.gameRunPath = gameRunPath;
            parseProfile(profileFullPath);
        }
        public ModProfile(string gameRunPath)
        {
            this.gameRunPath = gameRunPath;
        }

        private void parseProfile(string profileFullPath)
        {
            using (StreamReader reader = new StreamReader(profileFullPath))
            {
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    string[] tokens = line.Split('=');
                    string key = tokens[0].Trim();
                    switch (key)
                    {
                        case "Name":
                            Name = tokens[1].Trim();
                            break;
                        case "DisplayName":
                            DisplayName = tokens[1].Trim();
                            break;
                        case "ModID":
                            ModID = tokens[1].Trim();
                            break;
                        case "RequireDLC":
                            RequireDLC = (Scenario)Enum.Parse(typeof(Scenario), tokens[1].Trim());
                            break;
                        case "Key":
                            Key = tokens[1].Trim();
                            break;
                    }
                }
            }
        }

        public void Start()
        {

        }
    }
}
