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
            this.btnCreateExhibitIndex = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RefreshNumbering = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.btnRemoveExhibitLocks = new System.Windows.Forms.Button();
            this.toolTipAdd = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipEdit = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipDelete = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRefresh = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFormat = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRemoveLocks = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ExhibitFormatting = new System.Windows.Forms.Button();
            this.NewExhibit = new System.Windows.Forms.Button();
            this.ClearReferencesToExhibit = new System.Windows.Forms.Button();
            this.EditExhibit = new System.Windows.Forms.Button();
            this.DeleteExhibit = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.NewCite = new System.Windows.Forms.Button();
            this.ClearReferencesToCite = new System.Windows.Forms.Button();
            this.EditCite = new System.Windows.Forms.Button();
            this.DeleteCite = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.toolTipIndex = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipClearFromDoc = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCiteExhibit = new System.Windows.Forms.ToolTip(this.components);
            this.btnRemovePincite = new System.Windows.Forms.Button();
            this.btnAddPincite = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MinimumSize = new System.Drawing.Size(50, 150);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(297, 472);
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
            this.btnCiteToExhibit.Size = new System.Drawing.Size(251, 35);
            this.btnCiteToExhibit.TabIndex = 2;
            this.btnCiteToExhibit.Text = "Cite to Selected Exhibit";
            this.btnCiteToExhibit.UseVisualStyleBackColor = true;
            this.btnCiteToExhibit.Click += new System.EventHandler(this.button2_Click);
            this.btnCiteToExhibit.MouseHover += new System.EventHandler(this.btnCiteToExhibit_MouseHover);
            // 
            // btnCreateExhibitIndex
            // 
            this.btnCreateExhibitIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateExhibitIndex.Location = new System.Drawing.Point(210, 112);
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
            this.panel1.Controls.Add(this.RefreshNumbering);
            this.panel1.Controls.Add(this.ErrorLabel);
            this.panel1.Controls.Add(this.btnRemoveExhibitLocks);
            this.panel1.Controls.Add(this.btnCiteToExhibit);
            this.panel1.Controls.Add(this.btnCreateExhibitIndex);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 514);
            this.panel1.MinimumSize = new System.Drawing.Size(112, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(297, 155);
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
            this.ErrorLabel.Location = new System.Drawing.Point(28, 3);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(263, 19);
            this.ErrorLabel.TabIndex = 14;
            this.ErrorLabel.Text = "[PLACEHOLDER TEXT}";
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ErrorLabel.Visible = false;
            this.ErrorLabel.Click += new System.EventHandler(this.ErrorLabel_Click);
            // 
            // btnRemoveExhibitLocks
            // 
            this.btnRemoveExhibitLocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveExhibitLocks.Location = new System.Drawing.Point(4, 112);
            this.btnRemoveExhibitLocks.MinimumSize = new System.Drawing.Size(50, 0);
            this.btnRemoveExhibitLocks.Name = "btnRemoveExhibitLocks";
            this.btnRemoveExhibitLocks.Size = new System.Drawing.Size(84, 40);
            this.btnRemoveExhibitLocks.TabIndex = 15;
            this.btnRemoveExhibitLocks.Text = "Remove Citation Locks";
            this.btnRemoveExhibitLocks.UseVisualStyleBackColor = true;
            this.btnRemoveExhibitLocks.Click += new System.EventHandler(this.btnRemoveExhibitLocks_Click);
            this.btnRemoveExhibitLocks.MouseHover += new System.EventHandler(this.button2_MouseHover);
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(311, 698);
            this.tabControl1.TabIndex = 19;
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(303, 672);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Exhibits";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.ExhibitFormatting);
            this.panel2.Controls.Add(this.NewExhibit);
            this.panel2.Controls.Add(this.ClearReferencesToExhibit);
            this.panel2.Controls.Add(this.EditExhibit);
            this.panel2.Controls.Add(this.DeleteExhibit);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(297, 472);
            this.panel2.TabIndex = 9;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // ExhibitFormatting
            // 
            this.ExhibitFormatting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExhibitFormatting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExhibitFormatting.Location = new System.Drawing.Point(161, 443);
            this.ExhibitFormatting.Name = "ExhibitFormatting";
            this.ExhibitFormatting.Size = new System.Drawing.Size(101, 24);
            this.ExhibitFormatting.TabIndex = 8;
            this.ExhibitFormatting.Text = " Formatting";
            this.ExhibitFormatting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExhibitFormatting.UseVisualStyleBackColor = true;
            this.ExhibitFormatting.Click += new System.EventHandler(this.ExhibitFormatting_Click);
            this.ExhibitFormatting.MouseHover += new System.EventHandler(this.ExhibitFormatting_MouseHover);
            // 
            // NewExhibit
            // 
            this.NewExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_plus_math_60;
            this.NewExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NewExhibit.Location = new System.Drawing.Point(268, 353);
            this.NewExhibit.Name = "NewExhibit";
            this.NewExhibit.Size = new System.Drawing.Size(24, 24);
            this.NewExhibit.TabIndex = 4;
            this.NewExhibit.Text = " ";
            this.NewExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewExhibit.UseVisualStyleBackColor = true;
            this.NewExhibit.Click += new System.EventHandler(this.NewExhibit_Click);
            this.NewExhibit.MouseHover += new System.EventHandler(this.NewExhibit_MouseHover);
            // 
            // ClearReferencesToExhibit
            // 
            this.ClearReferencesToExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearReferencesToExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_erase_64;
            this.ClearReferencesToExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClearReferencesToExhibit.Location = new System.Drawing.Point(268, 413);
            this.ClearReferencesToExhibit.Name = "ClearReferencesToExhibit";
            this.ClearReferencesToExhibit.Size = new System.Drawing.Size(24, 24);
            this.ClearReferencesToExhibit.TabIndex = 6;
            this.ClearReferencesToExhibit.Text = " ";
            this.ClearReferencesToExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ClearReferencesToExhibit.UseVisualStyleBackColor = true;
            this.ClearReferencesToExhibit.Click += new System.EventHandler(this.ClearReferencesToExhibit_Click);
            this.ClearReferencesToExhibit.MouseHover += new System.EventHandler(this.ClearReferencesToExhibit_MouseHover);
            // 
            // EditExhibit
            // 
            this.EditExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_pencil_drawing_64;
            this.EditExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EditExhibit.Location = new System.Drawing.Point(268, 383);
            this.EditExhibit.Name = "EditExhibit";
            this.EditExhibit.Size = new System.Drawing.Size(24, 24);
            this.EditExhibit.TabIndex = 5;
            this.EditExhibit.Text = " ";
            this.EditExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditExhibit.UseVisualStyleBackColor = true;
            this.EditExhibit.Click += new System.EventHandler(this.EditExhibit_Click);
            this.EditExhibit.MouseHover += new System.EventHandler(this.EditExhibit_MouseHover);
            // 
            // DeleteExhibit
            // 
            this.DeleteExhibit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteExhibit.BackgroundImage = global::LitKit1.Properties.Resources.icons8_delete_64;
            this.DeleteExhibit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteExhibit.Location = new System.Drawing.Point(268, 443);
            this.DeleteExhibit.Name = "DeleteExhibit";
            this.DeleteExhibit.Size = new System.Drawing.Size(24, 24);
            this.DeleteExhibit.TabIndex = 7;
            this.DeleteExhibit.Text = " ";
            this.DeleteExhibit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteExhibit.UseVisualStyleBackColor = true;
            this.DeleteExhibit.Click += new System.EventHandler(this.button2_Click_1);
            this.DeleteExhibit.MouseHover += new System.EventHandler(this.DeleteExhibit_MouseHover);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(303, 672);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Legal and Record Citations";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button6);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.button8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 549);
            this.panel4.MinimumSize = new System.Drawing.Size(112, 49);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(297, 120);
            this.panel4.TabIndex = 12;
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::LitKit1.Properties.Resources.icons8_refresh_64;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Location = new System.Drawing.Point(5, 23);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(35, 35);
            this.button6.TabIndex = 3;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.MouseHover += new System.EventHandler(this.button6_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "[PLACEHOLDER TEXT}";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(42, 23);
            this.button8.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(251, 35);
            this.button8.TabIndex = 2;
            this.button8.Text = "Insert Selected Citation";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.MouseHover += new System.EventHandler(this.button8_MouseHover);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.NewCite);
            this.panel3.Controls.Add(this.ClearReferencesToCite);
            this.panel3.Controls.Add(this.EditCite);
            this.panel3.Controls.Add(this.DeleteCite);
            this.panel3.Controls.Add(this.listView2);
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(297, 543);
            this.panel3.TabIndex = 0;
            // 
            // NewCite
            // 
            this.NewCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_plus_math_60;
            this.NewCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NewCite.Location = new System.Drawing.Point(268, 424);
            this.NewCite.Name = "NewCite";
            this.NewCite.Size = new System.Drawing.Size(24, 24);
            this.NewCite.TabIndex = 10;
            this.NewCite.Text = " ";
            this.NewCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewCite.UseVisualStyleBackColor = true;
            this.NewCite.Click += new System.EventHandler(this.button2_Click_2);
            this.NewCite.MouseHover += new System.EventHandler(this.NewCite_MouseHover);
            // 
            // ClearReferencesToCite
            // 
            this.ClearReferencesToCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearReferencesToCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_erase_64;
            this.ClearReferencesToCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClearReferencesToCite.Location = new System.Drawing.Point(268, 484);
            this.ClearReferencesToCite.Name = "ClearReferencesToCite";
            this.ClearReferencesToCite.Size = new System.Drawing.Size(24, 24);
            this.ClearReferencesToCite.TabIndex = 12;
            this.ClearReferencesToCite.Text = " ";
            this.ClearReferencesToCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ClearReferencesToCite.UseVisualStyleBackColor = true;
            this.ClearReferencesToCite.Click += new System.EventHandler(this.button3_Click);
            this.ClearReferencesToCite.MouseHover += new System.EventHandler(this.ClearReferencesToCite_MouseHover);
            // 
            // EditCite
            // 
            this.EditCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_pencil_drawing_64;
            this.EditCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EditCite.Location = new System.Drawing.Point(268, 454);
            this.EditCite.Name = "EditCite";
            this.EditCite.Size = new System.Drawing.Size(24, 24);
            this.EditCite.TabIndex = 11;
            this.EditCite.Text = " ";
            this.EditCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditCite.UseVisualStyleBackColor = true;
            this.EditCite.Click += new System.EventHandler(this.button4_Click);
            this.EditCite.MouseHover += new System.EventHandler(this.EditCite_MouseHover);
            // 
            // DeleteCite
            // 
            this.DeleteCite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteCite.BackgroundImage = global::LitKit1.Properties.Resources.icons8_delete_64;
            this.DeleteCite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DeleteCite.Location = new System.Drawing.Point(268, 514);
            this.DeleteCite.Name = "DeleteCite";
            this.DeleteCite.Size = new System.Drawing.Size(24, 24);
            this.DeleteCite.TabIndex = 13;
            this.DeleteCite.Text = " ";
            this.DeleteCite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteCite.UseVisualStyleBackColor = true;
            this.DeleteCite.Click += new System.EventHandler(this.button5_Click);
            this.DeleteCite.MouseHover += new System.EventHandler(this.DeleteCite_MouseHover);
            // 
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.MinimumSize = new System.Drawing.Size(50, 150);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(297, 543);
            this.listView2.TabIndex = 9;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            // 
            // btnRemovePincite
            // 
            this.btnRemovePincite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemovePincite.Image = global::LitKit1.Properties.Resources.RemovePincite_16px;
            this.btnRemovePincite.Location = new System.Drawing.Point(284, 6);
            this.btnRemovePincite.Name = "btnRemovePincite";
            this.btnRemovePincite.Size = new System.Drawing.Size(25, 25);
            this.btnRemovePincite.TabIndex = 20;
            this.btnRemovePincite.UseVisualStyleBackColor = true;
            this.btnRemovePincite.Click += new System.EventHandler(this.btnRemovePincite_Click);
            this.btnRemovePincite.MouseHover += new System.EventHandler(this.btnRemovePincite_MouseHover);
            // 
            // btnAddPincite
            // 
            this.btnAddPincite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPincite.Image = global::LitKit1.Properties.Resources.AddPincite_16px;
            this.btnAddPincite.Location = new System.Drawing.Point(258, 6);
            this.btnAddPincite.Name = "btnAddPincite";
            this.btnAddPincite.Size = new System.Drawing.Size(25, 25);
            this.btnAddPincite.TabIndex = 16;
            this.btnAddPincite.UseVisualStyleBackColor = true;
            this.btnAddPincite.Click += new System.EventHandler(this.button1_Click_2);
            this.btnAddPincite.MouseHover += new System.EventHandler(this.btnAddPincite_MouseHover);
            // 
            // ctrlExhibitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnRemovePincite);
            this.Controls.Add(this.btnAddPincite);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(317, 200);
            this.Name = "ctrlExhibitView";
            this.Size = new System.Drawing.Size(317, 738);
            this.Load += new System.EventHandler(this.ExhibitCtrl_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnCiteToExhibit;
        private System.Windows.Forms.Button RefreshNumbering;
        private System.Windows.Forms.Button btnCreateExhibitIndex;
        private System.Windows.Forms.Button EditExhibit;
        private System.Windows.Forms.Button NewExhibit;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.ToolTip toolTipIndex;
        private System.Windows.Forms.ToolTip toolTipClearFromDoc;
        private System.Windows.Forms.ToolTip toolTipCiteExhibit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ExhibitFormatting;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button NewCite;
        private System.Windows.Forms.Button ClearReferencesToCite;
        private System.Windows.Forms.Button EditCite;
        private System.Windows.Forms.Button DeleteCite;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btnAddPincite;
        private System.Windows.Forms.Button btnRemovePincite;
    }
}
