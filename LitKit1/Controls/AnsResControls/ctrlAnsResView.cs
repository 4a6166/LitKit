﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.Controls.AnsResControls
{
    public partial class ctrlAnsResView : UserControl
    {
        public ctrlAnsResView()
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
            treeView1.ExpandAll();
        }

        Word.Application _app;

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string insertText = treeView1.SelectedNode.Tag.ToString();

            _app.Selection.TypeText(insertText);

            Globals.ThisAddIn.ReturnFocus();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            ctrlAnsResCustomize AnsResCtrl = new ctrlAnsResCustomize();
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(AnsResCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            AnsResCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
        }
    }
}
