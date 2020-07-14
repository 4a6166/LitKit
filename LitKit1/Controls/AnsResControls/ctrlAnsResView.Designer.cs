namespace LitKit1.Controls.AnsResControls
{
    partial class ctrlAnsResView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Cumulative");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Equally Available");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Overbroad");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Premature Contention");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlAnsResView));
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Premature Expert");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Privilege");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Proportionality");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Publicly Available");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Repetitive");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Response");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Rule 33(d)");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Custom...");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Objections", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Admit");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Deny");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Deny Remaining");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Incorporate Prior");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Lack Knowledge");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Legal Allegation");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Quotes Document");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Response");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Custom...");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Responses", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22});
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolTipPropoundingParty = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRespondingParty = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.toolTipCustomize = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Answer to Complaint",
            "Interrogatories",
            "Requests for Documents",
            "Requests for Admission"});
            this.comboBox1.Location = new System.Drawing.Point(9, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Answer to Complaint";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Document Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Responding Party Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Propounding Party Name";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(9, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(298, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(9, 187);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(298, 20);
            this.textBox2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic);
            this.label4.Location = new System.Drawing.Point(6, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "(as you would like it to appear in the document)";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic);
            this.checkBox1.Location = new System.Drawing.Point(155, 133);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(149, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Responding Party is Plural";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic);
            this.label6.Location = new System.Drawing.Point(6, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "(as you would like it to appear in the document)";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.HotTracking = true;
            this.treeView1.Location = new System.Drawing.Point(9, 248);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node1";
            treeNode1.Tag = "[Responding Party] [object/objects] to this [Request/Interrogatory] as cumulative" +
    " of prior discovery request. ";
            treeNode1.Text = "Cumulative";
            treeNode2.Name = "Node6";
            treeNode2.Tag = "[Responding Party] [object/objects] to this [Request/Interrogatory] as requesting" +
    " [documents/information] equally available to [Propounding Party].";
            treeNode2.Text = "Equally Available";
            treeNode3.Name = "Node7";
            treeNode3.Tag = "[Responding Party] [object/objects] to this [Request/Interrogatory] as overbroad " +
    "and burdensome.";
            treeNode3.Text = "Overbroad";
            treeNode4.Name = "Node8";
            treeNode4.Tag = resources.GetString("treeNode4.Tag");
            treeNode4.Text = "Premature Contention";
            treeNode5.Name = "Node9";
            treeNode5.Tag = resources.GetString("treeNode5.Tag");
            treeNode5.Text = "Premature Expert";
            treeNode6.Name = "Node10";
            treeNode6.Tag = resources.GetString("treeNode6.Tag");
            treeNode6.Text = "Privilege";
            treeNode7.Name = "Node11";
            treeNode7.Tag = resources.GetString("treeNode7.Tag");
            treeNode7.Text = "Proportionality";
            treeNode8.Name = "Node12";
            treeNode8.Tag = "[Responding Party] [object/objects] to this [Request/Interrogatory] as requesting" +
    " [documents/information] that are publicly available.";
            treeNode8.Text = "Publicly Available";
            treeNode9.Name = "Node13";
            treeNode9.Tag = "[Responding Party] [object/objects] to this [Request/Interrogatory] as repetitive" +
    " of prior discovery request and directs [Propounding Party] to . . . ";
            treeNode9.Text = "Repetitive";
            treeNode10.Name = "Node14";
            treeNode10.Tag = "[INSERT RESPONSE]";
            treeNode10.Text = "Response";
            treeNode11.Name = "Node15";
            treeNode11.Tag = "As permitted by Rule 33(d) of the Federal Rule of Civil Procedure, [Responding Pa" +
    "rty] [direct/directs] [Propounding Party] to the following documents.";
            treeNode11.Text = "Rule 33(d)";
            treeNode12.Name = "Node16";
            treeNode12.Tag = "[INSERT CUTSOM LANGUAGE]";
            treeNode12.Text = "Custom...";
            treeNode13.Name = "Node0";
            treeNode13.Tag = "";
            treeNode13.Text = "Objections";
            treeNode14.Name = "Node3";
            treeNode14.Tag = "[Responding Party] [admit/admits] the allegations contained in this Paragraph.";
            treeNode14.Text = "Admit";
            treeNode15.Name = "Node17";
            treeNode15.Tag = "[Responding Party] [deny/denies] the allegations contained in this Paragraph.";
            treeNode15.Text = "Deny";
            treeNode16.Name = "Node18";
            treeNode16.Tag = "[Responding Party] [deny/denies] the remainder of the allegations contained in th" +
    "is Paragraph.";
            treeNode16.Text = "Deny Remaining";
            treeNode17.Name = "Node19";
            treeNode17.Tag = "[Responding Party] hereby [incorporate/incorporates] the responses from the prece" +
    "ding paragraphs.";
            treeNode17.Text = "Incorporate Prior";
            treeNode18.Name = "Node20";
            treeNode18.Tag = "[Responding Party] [lack/lacks] knowledge or information sufficient to form a bel" +
    "ief about the truth of the allegations contained in this paragraph.";
            treeNode18.Text = "Lack Knowledge";
            treeNode19.Name = "Node21";
            treeNode19.Tag = "This Paragraph contains legal allegations to which no answer is necessary.";
            treeNode19.Text = "Legal Allegation";
            treeNode20.Name = "Node22";
            treeNode20.Tag = "[Responding Party] [admit/admits] that this paragraph accurately quotes a documen" +
    "t, but denies any characterization of the document and states that the document " +
    "speaks for itself.";
            treeNode20.Text = "Quotes Document";
            treeNode21.Name = "Node23";
            treeNode21.Tag = "Subject to and without waiving the foregoing objections, [Responding Party] [stat" +
    "e/states] ";
            treeNode21.Text = "Response";
            treeNode22.Name = "Node24";
            treeNode22.Tag = "[INSERT CUTSOM LANGUAGE]";
            treeNode22.Text = "Custom...";
            treeNode23.Name = "Node2";
            treeNode23.Tag = "";
            treeNode23.Text = "Responses";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode23});
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(298, 341);
            this.treeView1.TabIndex = 11;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // toolTipPropoundingParty
            // 
            this.toolTipPropoundingParty.Tag = "Add the name of the party or parties propounding the discovery requests, as you w" +
    "ould like them to appear in the discovery response (for example \"Mr. Smith\", \"Re" +
    "spondent\", or \"Defendants\").";
            // 
            // toolTipRespondingParty
            // 
            this.toolTipRespondingParty.Tag = resources.GetString("toolTipRespondingParty.Tag");
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(9, 595);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(298, 30);
            this.button1.TabIndex = 12;
            this.button1.Text = "Customize Objections and Responses";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolTipCustomize
            // 
            this.toolTipCustomize.Tag = "Users may add, edit, or delete responses. All edits stay with the document and do" +
    " not affect preset options for other documents using LitKit\'s Response Tool.";
            // 
            // ctrlAnsResView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.MinimumSize = new System.Drawing.Size(317, 200);
            this.Name = "ctrlAnsResView";
            this.Size = new System.Drawing.Size(317, 638);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolTip toolTipPropoundingParty;
        private System.Windows.Forms.ToolTip toolTipRespondingParty;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTipCustomize;
    }
}
