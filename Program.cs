using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaleworldsProductSerialKeyVerifier
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ModProfileManager.Instance.LoadAll();
            var argList = args.ToList();
            if (argList.Contains("-scenario") && argList.Contains("-profile") && argList.Count == 4)
            {
                Scenario scenario = (Scenario)Enum.Parse(typeof(Scenario), argList[1]);
                string profileID = argList[3];
                var profile = ModProfileManager.Instance.Profiles.Where(o => o.Name == profileID).FirstOrDefault();
                if (profile != null)
                {
                    Application.Run(new frmSerialKeyChecker(scenario, profile));
                }
                else
                {
                    Application.Run(new frmMain());
                }
            }
            else
            {
                if (ModProfileManager.Instance.Profiles.Count == 1)
                {
                    var profile = ModProfileManager.Instance.Profiles[0];
                    Application.Run(new frmSerialKeyChecker(profile.RequireDLC, profile));
                }
                else
                {
                    Application.Run(new frmMain());
                }
            }
        }
    }

    public enum Scenario
    {
        warband,
        wfas,
        nw,
        vc
    }
}
