﻿//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Tools.Exhibit;

//namespace LitKit1.Controls.ExhibitControls
//{
//    public partial class ctrlExhibitUpdateAdd : UserControl
//    {
//        public ctrlExhibitUpdateAdd()
//        {
//            InitializeComponent();
            
//            _app = Globals.ThisAddIn.Application;
//        }

//        readonly Microsoft.Office.Interop.Word.Application _app;
//        public string ID = string.Empty;

//        private string lcExample = "Description of Exhibit";
//        private string scExample = "ABC0001234";




//        private void button1_Click(object sender, EventArgs e)
//        {
//            if (textBox1.Text == "Description of Exhibit" || textBox1.Text == "" || string.IsNullOrWhiteSpace(textBox1.Text))
//            {
//                MessageBox.Show("No exhibit information provided");
//            }

//            else if (string.IsNullOrEmpty(ID))
//            {
//                ExhibitRepository repository = new ExhibitRepository(Globals.ThisAddIn.Application);

//                string description = string.Empty;
//                if (textBox1.Text != "Description of Exhibit")
//                {
//                    description = textBox1.Text;
//                }

//                string bates = string.Empty;
//                if (textBox3.Text != "ABC0001234")
//                {
//                    bates = textBox3.Text;
//                }

//                repository.AddExhibit(description, bates);


//                button3_Click(sender, e);
//            }
//            else
//            {
//                ExhibitRepository repository = new ExhibitRepository(Globals.ThisAddIn.Application);

//                string description = string.Empty;
//                if (textBox1.Text != "Description of Exhibit")
//                {
//                    description = textBox1.Text;
//                }

//                string bates = string.Empty;
//                if (textBox3.Text != "ABC0001234")
//                {
//                    bates = textBox3.Text;
//                }

//                repository.UpdateExhibit(ID, description, bates);

//                button3_Click(sender, e);
//            }
//        }


//        private void button3_Click(object sender, EventArgs e)
//        {
//            ctrlExhibitView exhibitCtrl = new ctrlExhibitView();
//            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
//            ActivePane.Control.Controls.Clear();
//            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

//            ActivePane.Control.Controls.Add(exhibitCtrl);
//            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
//            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
//            TabControl tabControl = (TabControl)exhibitCtrl.Controls[2]; // the index of the tab control moves depending on what control is closes to the top of the form.
//            tabControl.SelectedIndex = 0; //To set the tab to the list that was just being edited (Exhibit tab here)

//            ActivePane.Visible = true;
//            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
//        }

//        private void textBox1_Enter(object sender, EventArgs e)
//        {
//            if (textBox1.Text == lcExample)
//            {
//                textBox1.Text = "";
//                textBox1.ForeColor = Color.Black;
//            }
//        }

//        private void textBox3_Enter(object sender, EventArgs e)
//        {
//            if (textBox3.Text == scExample)
//            {
//                textBox3.Text = "";
//                textBox3.ForeColor = Color.Black;
//            }
//        }

//        private void textBox3_Leave(object sender, EventArgs e)
//        {
//            if (textBox3.Text == "")
//            {
//                textBox3.Text = scExample;
//                textBox3.ForeColor = Color.Gray;
//            }
//        }

//        private void textBox1_Leave(object sender, EventArgs e)
//        {
//            if (textBox1.Text == "")
//            {
//                textBox1.Text = lcExample;
//                textBox1.ForeColor = Color.Gray;
//            }
            
//        }

//        public void GrayExampleText()
//        {
//            if (textBox1.Text != lcExample)
//            { textBox1.ForeColor = Color.Black; }
//            else textBox1.ForeColor = Color.Gray;

//            if (textBox3.Text != scExample)
//            { textBox3.ForeColor = Color.Black; }
//            else textBox3.ForeColor = Color.Gray;

//        }

//        private void textBox3_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void button1_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Return)
//            {
//                button1.PerformClick();
//            }
//        }

//        private void button3_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Return)
//            {
//                button3.PerformClick();
//            }
//        }

//        private void textBox1_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Return)
//            {
//                button1.PerformClick();
//            }
//        }

//        private void textBox3_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Return)
//            {
//                button1.PerformClick();
//            }
//        }
//    }
//}
