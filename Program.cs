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

            var argList = args.ToList();
            if (argList.Contains("-scenario") && argList.Count == 2)
            {
                Scenario scenario = (Scenario)Enum.Parse(typeof(Scenario), argList[1]);
                Application.Run(new frmSerialKeyChecker(scenario));
            }
            else
            {
                Application.Run(new frmMain());
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
