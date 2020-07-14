using System;
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
    public partial class ctrlAnsResCustomize : UserControl
    {
        public ctrlAnsResCustomize()
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
        }

        Word.Application _app;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctrlAnsResView AnsResCtrl = new ctrlAnsResView();
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(AnsResCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            AnsResCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctrlAnsResView AnsResCtrl = new ctrlAnsResView();
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
