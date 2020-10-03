using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Exhibit;

namespace LitKit1.Controls.ExhibitControls
{
    public partial class ctrlLegalRecordUpdateAdd : UserControl
    {
        public ctrlLegalRecordUpdateAdd()
        {
            InitializeComponent();

            _app = Globals.ThisAddIn.Application;

        }

        readonly Microsoft.Office.Interop.Word.Application _app;
        public string ID = string.Empty;

        private string lcExample = "Palsgraf v. Long Island R.R. Co., 162 N.E. 99, 101 (N.Y. 1928)";
        private string scExample = "Palsgraf, 162 N.E. at 101";


        private void button3_Click(object sender, EventArgs e)
        {
            ctrlExhibitView exhibitCtrl = new ctrlExhibitView();
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(exhibitCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            TabControl tabControl = (TabControl) exhibitCtrl.Controls[2]; // the index of the tab control moves depending on what control is closes to the top of the form.
            tabControl.SelectedIndex = 1; //To set the tab to the list that was just being edited (legal/record cites here)

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == lcExample || textBox1.Text == "" || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("No citation information provided");
            }

            else
            {
                ExhibitRepository repository = new ExhibitRepository(Globals.ThisAddIn.Application);

                string LongCite = textBox1.Text;
                string ShortCite = string.Empty;
                if (textBox3.Text != scExample)
                {
                    ShortCite = textBox3.Text;
                }
                else ShortCite = textBox1.Text;


                if (string.IsNullOrEmpty(ID))
                {
                    repository.AddLRCite(LongCite, ShortCite);

                    button3_Click(sender, e);
                }
                else
                {
                    repository.UpdateLRCite(ID, LongCite, ShortCite);

                    button3_Click(sender, e);
                }
            }

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == lcExample)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = lcExample;
                textBox1.ForeColor = Color.Gray;
            }
        }


        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == scExample)
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = scExample;
                textBox3.ForeColor = Color.Gray;
            }
        }


        public void GrayExampleText()
        {
            if (textBox1.Text != lcExample)
            { textBox1.ForeColor = Color.Black; }
            else textBox1.ForeColor = Color.Gray;

            if (textBox3.Text != scExample)
            { textBox3.ForeColor = Color.Black; }
            else textBox3.ForeColor = Color.Gray;

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button1.PerformClick();
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button3.PerformClick();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button1.PerformClick();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button1.PerformClick();
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
