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
    public partial class frmProfileSelect : Form
    {
        public ModProfile SelectedProfile { get; internal set; }

        public frmProfileSelect(Scenario scenario)
        {
            InitializeComponent();
            lstProfiles.Items.Clear();
            foreach (var profile in ModProfileManager.Instance.Profiles)
            {
                if (profile.RequireDLC == scenario)
                {
                    lstProfiles.Items.Add(profile.Name);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstProfiles.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid profile!");
                return;
            }

            SelectedProfile = ModProfileManager.Instance.Profiles.Where(o => o.Name == lstProfiles.SelectedItem.ToString()).FirstOrDefault();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lstProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProfiles.SelectedIndex > -1)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }
    }
}
