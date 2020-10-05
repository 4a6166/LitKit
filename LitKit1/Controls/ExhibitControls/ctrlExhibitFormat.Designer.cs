namespace LitKit1.Controls.ExhibitControls
{
    partial class ctrlExhibitFormat
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCustomizeFormatting = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbUniformCitesStandard = new System.Windows.Forms.CheckBox();
            this.checkbIdCite = new System.Windows.Forms.CheckBox();
            this.checkbParentheses = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDescBatesFormat = new System.Windows.Forms.ComboBox();
            this.cbNumbering = new System.Windows.Forms.ComboBox();
            this.cbIntroMark = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LongCiteExampleText = new System.Windows.Forms.Label();
            this.LongCiteExample = new System.Windows.Forms.Label();
            this.ShortCiteExampleText = new System.Windows.Forms.Label();
            this.ShortCiteExample = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 220);
            this.button3.MaximumSize = new System.Drawing.Size(101, 40);
            this.button3.MinimumSize = new System.Drawing.Size(101, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 40);
            this.button3.TabIndex = 8;
            this.button3.Text = "Return to\r\nExhibit List";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnCustomizeFormatting);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(26, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(785, 321);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // btnCustomizeFormatting
            // 
            this.btnCustomizeFormatting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCustomizeFormatting.Location = new System.Drawing.Point(3, 305);
            this.btnCustomizeFormatting.Name = "btnCustomizeFormatting";
            this.btnCustomizeFormatting.Size = new System.Drawing.Size(779, 13);
            this.btnCustomizeFormatting.TabIndex = 21;
            this.btnCustomizeFormatting.TabStop = true;
            this.btnCustomizeFormatting.Text = "Customize Formatting";
            this.btnCustomizeFormatting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCustomizeFormatting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCustomizeFormatting_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Exhibit Formatting";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.cbUniformCitesStandard);
            this.panel2.Controls.Add(this.checkbIdCite);
            this.panel2.Controls.Add(this.checkbParentheses);
            this.panel2.Location = new System.Drawing.Point(3, 76);
            this.panel2.MinimumSize = new System.Drawing.Size(0, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(779, 104);
            this.panel2.TabIndex = 24;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // cbUniformCitesStandard
            // 
            this.cbUniformCitesStandard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUniformCitesStandard.AutoSize = true;
            this.cbUniformCitesStandard.Location = new System.Drawing.Point(56, 18);
            this.cbUniformCitesStandard.Name = "cbUniformCitesStandard";
            this.cbUniformCitesStandard.Size = new System.Drawing.Size(213, 17);
            this.cbUniformCitesStandard.TabIndex = 19;
            this.cbUniformCitesStandard.Text = "Description and Bates in Initial Cite Only";
            this.cbUniformCitesStandard.UseVisualStyleBackColor = true;
            this.cbUniformCitesStandard.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkbIdCite
            // 
            this.checkbIdCite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbIdCite.AutoSize = true;
            this.checkbIdCite.Checked = true;
            this.checkbIdCite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbIdCite.Location = new System.Drawing.Point(56, 41);
            this.checkbIdCite.Name = "checkbIdCite";
            this.checkbIdCite.Size = new System.Drawing.Size(172, 17);
            this.checkbIdCite.TabIndex = 5;
            this.checkbIdCite.Text = "Use \"Id.\" for repeated citations";
            this.checkbIdCite.UseVisualStyleBackColor = true;
            this.checkbIdCite.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkbParentheses
            // 
            this.checkbParentheses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbParentheses.AutoSize = true;
            this.checkbParentheses.Location = new System.Drawing.Point(56, 64);
            this.checkbParentheses.Name = "checkbParentheses";
            this.checkbParentheses.Size = new System.Drawing.Size(207, 17);
            this.checkbParentheses.TabIndex = 6;
            this.checkbParentheses.Text = "Enclose entire citations in parentheses";
            this.checkbParentheses.UseVisualStyleBackColor = true;
            this.checkbParentheses.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbDescBatesFormat);
            this.panel1.Controls.Add(this.cbNumbering);
            this.panel1.Controls.Add(this.cbIntroMark);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 54);
            this.panel1.TabIndex = 19;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "Intro Mark";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(86, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "Numbering";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cbDescBatesFormat
            // 
            this.cbDescBatesFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDescBatesFormat.FormattingEnabled = true;
            this.cbDescBatesFormat.Items.AddRange(new object[] {
            "Description",
            "Description, Bates",
            "(Description)",
            "(Description, Bates)"});
            this.cbDescBatesFormat.Location = new System.Drawing.Point(163, 22);
            this.cbDescBatesFormat.MinimumSize = new System.Drawing.Size(30, 0);
            this.cbDescBatesFormat.Name = "cbDescBatesFormat";
            this.cbDescBatesFormat.Size = new System.Drawing.Size(601, 21);
            this.cbDescBatesFormat.TabIndex = 3;
            this.cbDescBatesFormat.Text = "Description, Bates";
            this.cbDescBatesFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // cbNumbering
            // 
            this.cbNumbering.FormattingEnabled = true;
            this.cbNumbering.Items.AddRange(new object[] {
            "1, 2, 3...",
            "A, B, C...",
            "I, II, III..."});
            this.cbNumbering.Location = new System.Drawing.Point(88, 22);
            this.cbNumbering.MinimumSize = new System.Drawing.Size(30, 0);
            this.cbNumbering.Name = "cbNumbering";
            this.cbNumbering.Size = new System.Drawing.Size(63, 21);
            this.cbNumbering.TabIndex = 2;
            this.cbNumbering.Text = "1, 2, 3...";
            this.cbNumbering.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // cbIntroMark
            // 
            this.cbIntroMark.FormattingEnabled = true;
            this.cbIntroMark.Items.AddRange(new object[] {
            "Exhibit",
            "Ex.",
            "Exh.",
            "Appendix",
            "Appx.",
            "Tab"});
            this.cbIntroMark.Location = new System.Drawing.Point(13, 22);
            this.cbIntroMark.MinimumSize = new System.Drawing.Size(30, 0);
            this.cbIntroMark.Name = "cbIntroMark";
            this.cbIntroMark.Size = new System.Drawing.Size(63, 21);
            this.cbIntroMark.TabIndex = 1;
            this.cbIntroMark.Text = "Exhibit";
            this.cbIntroMark.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(162, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(543, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "Description and Bates";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(666, 220);
            this.button2.MaximumSize = new System.Drawing.Size(101, 40);
            this.button2.MinimumSize = new System.Drawing.Size(101, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 40);
            this.button2.TabIndex = 7;
            this.button2.Text = "Save Exhibit\r\nFormatting";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UpdateExhibitFormatting_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.LongCiteExampleText);
            this.groupBox1.Controls.Add(this.LongCiteExample);
            this.groupBox1.Controls.Add(this.ShortCiteExampleText);
            this.groupBox1.Controls.Add(this.ShortCiteExample);
            this.groupBox1.Location = new System.Drawing.Point(26, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 69);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // LongCiteExampleText
            // 
            this.LongCiteExampleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LongCiteExampleText.AutoEllipsis = true;
            this.LongCiteExampleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LongCiteExampleText.Location = new System.Drawing.Point(45, 15);
            this.LongCiteExampleText.Name = "LongCiteExampleText";
            this.LongCiteExampleText.Size = new System.Drawing.Size(672, 18);
            this.LongCiteExampleText.TabIndex = 10;
            this.LongCiteExampleText.Text = "Exhibit 1, an example exhibit (A123)";
            this.LongCiteExampleText.Click += new System.EventHandler(this.LongCiteExampleText_Click);
            // 
            // LongCiteExample
            // 
            this.LongCiteExample.AutoSize = true;
            this.LongCiteExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.LongCiteExample.Location = new System.Drawing.Point(6, 15);
            this.LongCiteExample.Name = "LongCiteExample";
            this.LongCiteExample.Size = new System.Drawing.Size(39, 15);
            this.LongCiteExample.TabIndex = 9;
            this.LongCiteExample.Text = "Initial:";
            // 
            // ShortCiteExampleText
            // 
            this.ShortCiteExampleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortCiteExampleText.AutoEllipsis = true;
            this.ShortCiteExampleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ShortCiteExampleText.Location = new System.Drawing.Point(45, 43);
            this.ShortCiteExampleText.Name = "ShortCiteExampleText";
            this.ShortCiteExampleText.Size = new System.Drawing.Size(672, 18);
            this.ShortCiteExampleText.TabIndex = 12;
            this.ShortCiteExampleText.Text = "Exhibit 1";
            // 
            // ShortCiteExample
            // 
            this.ShortCiteExample.AutoSize = true;
            this.ShortCiteExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ShortCiteExample.Location = new System.Drawing.Point(6, 43);
            this.ShortCiteExample.Name = "ShortCiteExample";
            this.ShortCiteExample.Size = new System.Drawing.Size(39, 15);
            this.ShortCiteExample.TabIndex = 11;
            this.ShortCiteExample.Text = "Short:";
            // 
            // ctrlExhibitFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(350, 400);
            this.Name = "ctrlExhibitFormat";
            this.Size = new System.Drawing.Size(850, 548);
            this.Load += new System.EventHandler(this.ctrlExhibitFormat_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkbIdCite;
        private System.Windows.Forms.ComboBox cbDescBatesFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkbParentheses;
        private System.Windows.Forms.ComboBox cbIntroMark;
        private System.Windows.Forms.ComboBox cbNumbering;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LongCiteExampleText;
        private System.Windows.Forms.Label LongCiteExample;
        private System.Windows.Forms.Label ShortCiteExampleText;
        private System.Windows.Forms.Label ShortCiteExample;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel btnCustomizeFormatting;
        private System.Windows.Forms.CheckBox cbUniformCitesStandard;
    }
}
