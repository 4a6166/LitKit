namespace LitKit1.Controls
{
    partial class ctrlExhibitView
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnCiteToExhibit = new System.Windows.Forms.Button();
            this.ExhibitFormatting = new System.Windows.Forms.Button();
            this.btnCreateExhibitIndex = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RefreshNumbering = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.btnRemoveExhibitLocks = new System.Windows.Forms.Button();
            this.ReorderExhibitsList = new System.Windows.Forms.Button();
            this.toolTipAdd = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipEdit = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipDelete = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRefresh = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFormat = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRemoveLocks = new System.Windows.Forms.ToolTip(this.components);
            this.ClearReferencesToExhibit = new System.Windows.Forms.Button();
            this.DeleteExhibit = new System.Windows.Forms.Button();
            this.NewExhibit = new System.Windows.Forms.Button();
            this.EditExhibit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolTipIndex = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipClearFromDoc = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCiteExhibit = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.ClearReferencesToCite = new System.Windows.Forms.Button();
            this.NewCite = new System.Windows.Forms.Button();
            this.EditCite = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MinimumSize = new System.Drawing.Size(50, 150);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(291, 402);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // btnCiteToExhibit
            // 
            this.btnCiteToExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCiteToExhibit.Location = new System.Drawing.Point(42, 23);
            this.btnCiteToExhibit.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.btnCiteToExhibit.Name = "btnCiteToExhibit";
            this.btnCiteToExhibit.Size = new System.Drawing.Size(271, 35);
            this.btnCiteToExhibit.TabIndex = 2;
            this.btnCiteToExhibit.Text = "Cite to Selected Exhibit";
            this.btnCiteToExhibit.UseVisualStyleBackColor = true;
            this.btnCiteToExhibit.Click += new System.EventHandler(this.button2_Click);
            this.btnCiteToExhibit.MouseHover += new System.EventHandler(this.btnCiteToExhibit_MouseHover);
            // 
            // ExhibitFormatting
            // 
            this.ExhibitFormatting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExhibitFormatting.Location = new System.Drawing.Point(230, 78);
            this.ExhibitFormatting.Name = "ExhibitFormatting";
            this.ExhibitFormatting.Size = new System.Drawing.Size(84, 24);
            this.ExhibitFormatting.TabIndex = 8;
            this.ExhibitFormatting.Text = "Formatting";
            this.ExhibitFormatting.UseVisualStyleBackColor = true;
            this.ExhibitFormatting.Click += new System.EventHandler(this.ExhibitFormatting_Click);
            this.ExhibitFormatting.MouseHover += new System.EventHandler(this.ExhibitFormatting_MouseHover);
            // 
            // btnCreateExhibitIndex
            // 
            this.btnCreateExhibitIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateExhibitIndex.Location = new System.Drawing.Point(230, 119);
            this.btnCreateExhibitIndex.MinimumSize = new System.Drawing.Size(50, 0);
            this.btnCreateExhibitIndex.Name = "btnCreateExhibitIndex";
            this.btnCreateExhibitIndex.Size = new System.Drawing.Size(84, 40);
            this.btnCreateExhibitIndex.TabIndex = 5;
            this.btnCreateExhibitIndex.Text = "Create Index of Exhibits";
            this.btnCreateExhibitIndex.UseVisualStyleBackColor = true;
            this.btnCreateExhibitIndex.Click += new System.EventHandler(this.button1_Click_1);
            this.btnCreateExhibitIndex.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ExhibitFormatting);
            this.panel1.Controls.Add(this.RefreshNumbering);
            this.panel1.Controls.Add(this.ErrorLabel);
            this.panel1.Controls.Add(this.btnRemoveExhibitLocks);
            this.panel1.Controls.Add(this.btnCiteToExhibit);
            this.panel1.Controls.Add(this.btnCreateExhibitIndex);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.MinimumSize = new System.Drawing.Size(112, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 162);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // RefreshNumbering
            // 
            this.RefreshNumbering.BackgroundImage = global::LitKit1.Properties.Resources.icons8_refresh_64;
            this.RefreshNumbering.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RefreshNumbering.Location = new System.Drawing.Point(5, 23);
            this.RefreshNumbering.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.RefreshNumbering.Name = "RefreshNumbering";
            this.RefreshNumbering.Size = new System.Drawing.Size(35, 35);
            this.RefreshNumbering.TabIndex = 3;
            this.RefreshNumbering.UseVisualStyleBackColor = true;
            this.RefreshNumbering.Click += new System.EventHandler(this.RefreshNumbering_Click);
            this.RefreshNumbering.MouseHover += new System.EventHandler(this.RefreshNumbering_MouseHover);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.Location = new System.Drawing.Point(48, 3);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(263, 19);
            this.ErrorLabel.TabIndex = 14;
            this.ErrorLabel.Text = "[PLACEHOLDER TEXT}";
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ErrorLabel.Click += new System.EventHandler(this.ErrorLabel_Click);
            // 
            // btnRemoveExhibitLocks
            // 
            this.btnRemoveExhibitLocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveExhibitLocks.Location = new System.Drawing.Point(4, 119);
            this.btnRemoveExhibitLocks.MinimumSize = new System.Drawing.Size(50, 0);
            this.btnRemoveExhibitLocks.Name = "btnRemoveExhibitLocks";
            this.btnRemoveExhibitLocks.Size = new System.Drawing.Size(84, 40);
            this.btnRemoveExhibitLocks.TabIndex = 15;
            this.btnRemoveExhibitLocks.Text = "Remove Exhibit Locks";
            this.btnRemoveExhibitLocks.UseVisualStyleBackColor = true;
            this.btnRemoveExhibitLocks.Click += new System.EventHandler(this.btnRemoveExhibitLocks_Click);
            this.btnRemoveExhibitLocks.MouseHover += new System.EventHandler(this.button2_MouseHover);
            // 
            // ReorderExhibitsList
            // 
            this.ReorderExhibitsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReorderExhibitsList.Enabled = false;
            this.ReorderExhibitsList.Location = new System.Drawing.Point(146, 378);
            this.ReorderExhibitsList.Name = "ReorderExhibitsList";
            this.ReorderExhibitsList.Size = new System.Drawing.Size(115, 23);
            this.ReorderExhibitsList.TabIndex = 13;
            this.ReorderExhibitsList.Text = "Reorder Exhibits List";
            this.ReorderExhibitsList.UseVisualStyleBackColor = true;
            this.ReorderExhibitsList.Visible = false;
            this.ReorderExhibitsList.Click += new System.EventHandler(this.ReorderExhibitsList_Click);
            // 
            // toolTipAdd
            // 
            this.toolTipAdd.AutoPopDelay = 5000;
            this.toolTipAdd.InitialDelay = 100;
            this.toolTipAdd.ReshowDelay = 100;
            this.toolTipAdd.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // toolTipEdit
            // 
            this.toolTipEdit.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip2_Popup);
            // 
            // toolTipDelete
            // 
            this.toolTipDelete.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip3_Popup);
            // 
            // toolTipRemoveLocks
            // 
            this.toolTipRemoveLocks.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup_1);
            // 
            // ClearReferencesToExhibit
            // 
            this.ClearReferencesToExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearReferencesToExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_erase_64;
            this.ClearReferencesToExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClearReferencesToExhibit.Location = new System.Drawing.Point(267, 378);
            this.ClearReferencesToExhibit.Name = "ClearReferencesToExhibit";
            this.ClearReferencesToExhibit.Size = new System.Drawing.Size(24, 24);
            this.ClearReferencesToExhibit.TabIndex = 6;
            this.ClearReferencesToExhibit.Text = " ";
            this.ClearReferencesToExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ClearReferencesToExhibit.UseVisualStyleBackColor = true;
            this.ClearReferencesToExhibit.Click += new System.EventHandler(this.ClearReferencesToExhibit_Click);
            this.ClearReferencesToExhibit.MouseHover += new System.EventHandler(this.ClearReferencesToExhibit_MouseHover);
            // 
            // DeleteExhibit
            // 
            this.DeleteExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_delete_64;
            this.DeleteExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteExhibit.Location = new System.Drawing.Point(287, 514);
            this.DeleteExhibit.Name = "DeleteExhibit";
            this.DeleteExhibit.Size = new System.Drawing.Size(24, 24);
            this.DeleteExhibit.TabIndex = 7;
            this.DeleteExhibit.Text = " ";
            this.DeleteExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteExhibit.UseVisualStyleBackColor = true;
            this.DeleteExhibit.Click += new System.EventHandler(this.button2_Click_1);
            this.DeleteExhibit.MouseHover += new System.EventHandler(this.DeleteExhibit_MouseHover);
            // 
            // NewExhibit
            // 
            this.NewExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_plus_math_60;
            this.NewExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NewExhibit.Location = new System.Drawing.Point(267, 318);
            this.NewExhibit.Name = "NewExhibit";
            this.NewExhibit.Size = new System.Drawing.Size(24, 24);
            this.NewExhibit.TabIndex = 4;
            this.NewExhibit.Text = " ";
            this.NewExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewExhibit.UseVisualStyleBackColor = true;
            this.NewExhibit.Click += new System.EventHandler(this.NewExhibit_Click);
            this.NewExhibit.MouseHover += new System.EventHandler(this.NewExhibit_MouseHover);
            // 
            // EditExhibit
            // 
            this.EditExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_pencil_drawing_64;
            this.EditExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EditExhibit.Location = new System.Drawing.Point(267, 348);
            this.EditExhibit.Name = "EditExhibit";
            this.EditExhibit.Size = new System.Drawing.Size(24, 24);
            this.EditExhibit.TabIndex = 5;
            this.EditExhibit.Text = " ";
            this.EditExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditExhibit.UseVisualStyleBackColor = true;
            this.EditExhibit.Click += new System.EventHandler(this.EditExhibit_Click);
            this.EditExhibit.MouseHover += new System.EventHandler(this.EditExhibit_MouseHover);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.DeleteExhibit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.MinimumSize = new System.Drawing.Size(200, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(317, 638);
            this.panel3.TabIndex = 18;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(305, 434);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.ClearReferencesToExhibit);
            this.tabPage1.Controls.Add(this.ReorderExhibitsList);
            this.tabPage1.Controls.Add(this.NewExhibit);
            this.tabPage1.Controls.Add(this.EditExhibit);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(297, 408);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Exhibits";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ClearReferencesToCite);
            this.tabPage2.Controls.Add(this.NewCite);
            this.tabPage2.Controls.Add(this.EditCite);
            this.tabPage2.Controls.Add(this.listView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(297, 408);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Legal and Record Citations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "References List";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ClearReferencesToCite
            // 
            this.ClearReferencesToCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearReferencesToCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_erase_64;
            this.ClearReferencesToCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClearReferencesToCite.Location = new System.Drawing.Point(267, 378);
            this.ClearReferencesToCite.Name = "ClearReferencesToCite";
            this.ClearReferencesToCite.Size = new System.Drawing.Size(24, 24);
            this.ClearReferencesToCite.TabIndex = 10;
            this.ClearReferencesToCite.Text = " ";
            this.ClearReferencesToCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ClearReferencesToCite.UseVisualStyleBackColor = true;
            // 
            // NewCite
            // 
            this.NewCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_plus_math_60;
            this.NewCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NewCite.Location = new System.Drawing.Point(267, 318);
            this.NewCite.Name = "NewCite";
            this.NewCite.Size = new System.Drawing.Size(24, 24);
            this.NewCite.TabIndex = 8;
            this.NewCite.Text = " ";
            this.NewCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewCite.UseVisualStyleBackColor = true;
            // 
            // EditCite
            // 
            this.EditCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_pencil_drawing_64;
            this.EditCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EditCite.Location = new System.Drawing.Point(267, 348);
            this.EditCite.Name = "EditCite";
            this.EditCite.Size = new System.Drawing.Size(24, 24);
            this.EditCite.TabIndex = 9;
            this.EditCite.Text = " ";
            this.EditCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditCite.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.MinimumSize = new System.Drawing.Size(50, 150);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(291, 402);
            this.listView2.TabIndex = 7;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // ctrlExhibitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel3);
            this.MinimumSize = new System.Drawing.Size(317, 200);
            this.Name = "ctrlExhibitView";
            this.Size = new System.Drawing.Size(317, 638);
            this.Load += new System.EventHandler(this.ExhibitCtrl_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnCiteToExhibit;
        private System.Windows.Forms.Button RefreshNumbering;
        private System.Windows.Forms.Button ExhibitFormatting;
        private System.Windows.Forms.Button btnCreateExhibitIndex;
        private System.Windows.Forms.Button EditExhibit;
        private System.Windows.Forms.Button NewExhibit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ReorderExhibitsList;
        private System.Windows.Forms.Button DeleteExhibit;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.ToolTip toolTipAdd;
        private System.Windows.Forms.ToolTip toolTipEdit;
        private System.Windows.Forms.ToolTip toolTipDelete;
        private System.Windows.Forms.ToolTip toolTipRefresh;
        private System.Windows.Forms.ToolTip toolTipFormat;
        private System.Windows.Forms.Button btnRemoveExhibitLocks;
        private System.Windows.Forms.ToolTip toolTipRemoveLocks;
        private System.Windows.Forms.Button ClearReferencesToExhibit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolTip toolTipIndex;
        private System.Windows.Forms.ToolTip toolTipClearFromDoc;
        private System.Windows.Forms.ToolTip toolTipCiteExhibit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClearReferencesToCite;
        private System.Windows.Forms.Button NewCite;
        private System.Windows.Forms.Button EditCite;
        private System.Windows.Forms.ListView listView2;
    }
}
