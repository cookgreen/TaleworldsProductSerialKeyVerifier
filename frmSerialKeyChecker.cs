using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TaleworldsProductSerialKeyVerifier
{
    public partial class frmSerialKeyChecker : Form
    {
        private Scenario scenario;
        private KeyChecker keyChecker;

        public frmSerialKeyChecker(Scenario scenario)
        {
            InitializeComponent();
            this.scenario = scenario;

            keyChecker = new KeyChecker(3, scenario);
            keyChecker.KeyOnlineCheckFinished += KeyChecker_KeyOnlineCheckFinished;
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

                string serialKey = string.Format("{0}-{1}-{2}-{3}",
                    txtSerialKeyTuple1.Text,
                    txtSerialKeyTuple2.Text,
                    txtSerialKeyTuple3.Text,
                    txtSerialKeyTuple4.Text);

                btnOK.Enabled = false;
                keyChecker.CheckOnline(serialKey);
            }
        }

        private void KeyChecker_KeyOnlineCheckFinished(bool checkResult)
        {
            btnOK.Enabled = true;
            if (!checkResult)
            {
                MessageBox.Show("Verify failed!");
            }
            else
            {
                MessageBox.Show("Verify success!");
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
    }
}
