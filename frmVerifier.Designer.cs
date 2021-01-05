namespace TaleworldsProductSerialKeyVerifier
{
    partial class frmVerifier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVerifier));
            this.txtSerialKeyTuple1 = new System.Windows.Forms.TextBox();
            this.txtSerialKeyTuple2 = new System.Windows.Forms.TextBox();
            this.txtSerialKeyTuple3 = new System.Windows.Forms.TextBox();
            this.txtSerialKeyTuple4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSerialKeyTuple1
            // 
            this.txtSerialKeyTuple1.Location = new System.Drawing.Point(40, 41);
            this.txtSerialKeyTuple1.Name = "txtSerialKeyTuple1";
            this.txtSerialKeyTuple1.Size = new System.Drawing.Size(49, 21);
            this.txtSerialKeyTuple1.TabIndex = 0;
            this.txtSerialKeyTuple1.TextChanged += new System.EventHandler(this.txtSerialKeyTuple1_TextChanged);
            // 
            // txtSerialKeyTuple2
            // 
            this.txtSerialKeyTuple2.Location = new System.Drawing.Point(112, 41);
            this.txtSerialKeyTuple2.Name = "txtSerialKeyTuple2";
            this.txtSerialKeyTuple2.Size = new System.Drawing.Size(49, 21);
            this.txtSerialKeyTuple2.TabIndex = 1;
            this.txtSerialKeyTuple2.TextChanged += new System.EventHandler(this.txtSerialKeyTuple2_TextChanged);
            // 
            // txtSerialKeyTuple3
            // 
            this.txtSerialKeyTuple3.Location = new System.Drawing.Point(184, 41);
            this.txtSerialKeyTuple3.Name = "txtSerialKeyTuple3";
            this.txtSerialKeyTuple3.Size = new System.Drawing.Size(49, 21);
            this.txtSerialKeyTuple3.TabIndex = 2;
            this.txtSerialKeyTuple3.TextChanged += new System.EventHandler(this.txtSerialKeyTuple3_TextChanged);
            // 
            // txtSerialKeyTuple4
            // 
            this.txtSerialKeyTuple4.Location = new System.Drawing.Point(256, 41);
            this.txtSerialKeyTuple4.Name = "txtSerialKeyTuple4";
            this.txtSerialKeyTuple4.Size = new System.Drawing.Size(49, 21);
            this.txtSerialKeyTuple4.TabIndex = 3;
            this.txtSerialKeyTuple4.TextChanged += new System.EventHandler(this.txtSerialKeyTuple4_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "-";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(138, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmVerifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 125);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSerialKeyTuple4);
            this.Controls.Add(this.txtSerialKeyTuple3);
            this.Controls.Add(this.txtSerialKeyTuple2);
            this.Controls.Add(this.txtSerialKeyTuple1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVerifier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial Key Checker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSerialKeyTuple1;
        private System.Windows.Forms.TextBox txtSerialKeyTuple2;
        private System.Windows.Forms.TextBox txtSerialKeyTuple3;
        private System.Windows.Forms.TextBox txtSerialKeyTuple4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
    }
}