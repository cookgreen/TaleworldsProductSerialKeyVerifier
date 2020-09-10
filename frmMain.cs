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
            SearchProfileAndLoadVerifier();
        }

        private void btnStartWFaSVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.wfas;
            SearchProfileAndLoadVerifier();
        }

        private void btnStartNapoleonVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.nw;
            SearchProfileAndLoadVerifier();
        }

        private void btnStartVikingConquestVerifier_Click(object sender, EventArgs e)
        {
            scenario = Scenario.vc;
            SearchProfileAndLoadVerifier();
        }

        private void SearchProfileAndLoadVerifier()
        {
            if (ModProfileManager.Instance.Profiles.Count == 1)
            {
                frmVerifier frmVerifier = new frmVerifier(scenario, ModProfileManager.Instance.Profiles[0]);
                frmVerifier.ShowDialog();
            }
            else
            {
                frmProfileSelect frmProfileSelect = new frmProfileSelect();
                if (frmProfileSelect.ShowDialog() == DialogResult.OK)
                {
                    var profile = frmProfileSelect.SelectedProfile;
                    frmVerifier frmVerifier = new frmVerifier(scenario, profile);
                    frmVerifier.ShowDialog();
                }
            }
        }
    }
}
