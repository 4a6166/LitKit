namespace LicenseGenerator
{
    partial class Form1
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
            this.btnGetKeys = new System.Windows.Forms.Button();
            this.btnNewLicense = new System.Windows.Forms.Button();
            this.btnValidateLicense = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRadioTest = new System.Windows.Forms.RadioButton();
            this.btnRadioTrial = new System.Windows.Forms.RadioButton();
            this.btnRadioStandard = new System.Windows.Forms.RadioButton();
            this.labelResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbExpiration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCustName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetKeys
            // 
            this.btnGetKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetKeys.Location = new System.Drawing.Point(433, 229);
            this.btnGetKeys.Name = "btnGetKeys";
            this.btnGetKeys.Size = new System.Drawing.Size(151, 23);
            this.btnGetKeys.TabIndex = 0;
            this.btnGetKeys.Text = "Import KeyPairs";
            this.btnGetKeys.UseVisualStyleBackColor = true;
            this.btnGetKeys.Click += new System.EventHandler(this.btnGetKeys_Click);
            // 
            // btnNewLicense
            // 
            this.btnNewLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewLicense.Location = new System.Drawing.Point(433, 146);
            this.btnNewLicense.Name = "btnNewLicense";
            this.btnNewLicense.Size = new System.Drawing.Size(151, 23);
            this.btnNewLicense.TabIndex = 1;
            this.btnNewLicense.Text = "Generate New License";
            this.btnNewLicense.UseVisualStyleBackColor = true;
            this.btnNewLicense.Click += new System.EventHandler(this.btnNewLicense_Click);
            // 
            // btnValidateLicense
            // 
            this.btnValidateLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidateLicense.Location = new System.Drawing.Point(433, 175);
            this.btnValidateLicense.Name = "btnValidateLicense";
            this.btnValidateLicense.Size = new System.Drawing.Size(151, 23);
            this.btnValidateLicense.TabIndex = 2;
            this.btnValidateLicense.Text = "Validate A License";
            this.btnValidateLicense.UseVisualStyleBackColor = true;
            this.btnValidateLicense.Click += new System.EventHandler(this.btnValidateLicense_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnRadioTest);
            this.panel1.Controls.Add(this.btnRadioTrial);
            this.panel1.Controls.Add(this.btnRadioStandard);
            this.panel1.Controls.Add(this.labelResult);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbExpiration);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbCustName);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 244);
            this.panel1.TabIndex = 3;
            // 
            // btnRadioTest
            // 
            this.btnRadioTest.AutoSize = true;
            this.btnRadioTest.Location = new System.Drawing.Point(285, 107);
            this.btnRadioTest.Name = "btnRadioTest";
            this.btnRadioTest.Size = new System.Drawing.Size(46, 17);
            this.btnRadioTest.TabIndex = 10;
            this.btnRadioTest.TabStop = true;
            this.btnRadioTest.Text = "Test";
            this.btnRadioTest.UseVisualStyleBackColor = true;
            // 
            // btnRadioTrial
            // 
            this.btnRadioTrial.AutoSize = true;
            this.btnRadioTrial.Location = new System.Drawing.Point(207, 107);
            this.btnRadioTrial.Name = "btnRadioTrial";
            this.btnRadioTrial.Size = new System.Drawing.Size(45, 17);
            this.btnRadioTrial.TabIndex = 9;
            this.btnRadioTrial.TabStop = true;
            this.btnRadioTrial.Text = "Trial";
            this.btnRadioTrial.UseVisualStyleBackColor = true;
            // 
            // btnRadioStandard
            // 
            this.btnRadioStandard.AutoSize = true;
            this.btnRadioStandard.Location = new System.Drawing.Point(112, 107);
            this.btnRadioStandard.Name = "btnRadioStandard";
            this.btnRadioStandard.Size = new System.Drawing.Size(68, 17);
            this.btnRadioStandard.TabIndex = 8;
            this.btnRadioStandard.TabStop = true;
            this.btnRadioStandard.Text = "Standard";
            this.btnRadioStandard.UseVisualStyleBackColor = true;
            // 
            // labelResult
            // 
            this.labelResult.Location = new System.Drawing.Point(-1, 217);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(403, 23);
            this.labelResult.TabIndex = 7;
            this.labelResult.Text = "[OPERATION RESULT]";
            this.labelResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "License Type";
            // 
            // tbExpiration
            // 
            this.tbExpiration.Location = new System.Drawing.Point(112, 64);
            this.tbExpiration.Name = "tbExpiration";
            this.tbExpiration.Size = new System.Drawing.Size(250, 20);
            this.tbExpiration.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Expiration Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customer Name";
            // 
            // tbCustName
            // 
            this.tbCustName.Location = new System.Drawing.Point(112, 31);
            this.tbCustName.Name = "tbCustName";
            this.tbCustName.Size = new System.Drawing.Size(250, 20);
            this.tbCustName.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::LicenseGenerator.Properties.Resources.Pilcrow2_B;
            this.pictureBox1.Location = new System.Drawing.Point(28, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.GenerateNewKeys);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(443, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(130, 106);
            this.panel2.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 272);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnValidateLicense);
            this.Controls.Add(this.btnNewLicense);
            this.Controls.Add(this.btnGetKeys);
            this.MaximumSize = new System.Drawing.Size(612, 311);
            this.MinimumSize = new System.Drawing.Size(612, 311);
            this.Name = "Form1";
            this.Text = "Prelimine LitKit License Generator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetKeys;
        private System.Windows.Forms.Button btnNewLicense;
        private System.Windows.Forms.Button btnValidateLicense;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCustName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbExpiration;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton btnRadioTest;
        private System.Windows.Forms.RadioButton btnRadioTrial;
        private System.Windows.Forms.RadioButton btnRadioStandard;
    }
}