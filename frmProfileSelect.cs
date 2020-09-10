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

        public frmProfileSelect()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            foreach (var profile in ModProfileManager.Instance.Profiles)
            {
                listBox1.Items.Add(profile.Name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid profile!");
                return;
            }

            SelectedProfile = ModProfileManager.Instance.Profiles.Where(o => o.Name == listBox1.SelectedItem.ToString()).FirstOrDefault();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
