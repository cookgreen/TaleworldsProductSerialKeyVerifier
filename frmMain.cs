using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaleworldsProductSerialKeyVerifier
{
    public partial class frmMain : Form
    {
        private Scenario scenario;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStartWarbandVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.warband;
            frmSerialKeyChecker frmVerifier = new frmSerialKeyChecker(scenario);
            frmVerifier.ShowDialog();
        }

        private void btnStartWFaSVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.wfas;
            frmSerialKeyChecker frmVerifier = new frmSerialKeyChecker(scenario);
            frmVerifier.ShowDialog();
        }

        private void btnStartNapoleonVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.nw;
            frmSerialKeyChecker frmVerifier = new frmSerialKeyChecker(scenario);
            frmVerifier.ShowDialog();
        }

        private void btnStartVikingConquestVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.vc;
            frmSerialKeyChecker frmVerifier = new frmSerialKeyChecker(scenario);
            frmVerifier.ShowDialog();
        }
    }
}
