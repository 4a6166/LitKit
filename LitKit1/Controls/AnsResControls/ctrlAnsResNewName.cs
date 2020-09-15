using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Response;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.Controls.AnsResControls
{
    public partial class ctrlAnsResNewName : UserControl
    {
        public ctrlAnsResNewName(ResponseRepository ResponseRepo, string text, string docType)
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
            this.repository = ResponseRepo;
            this.text = text;
            this.docType = docType;
        }

        Word.Application _app;
        ResponseRepository repository;
        string text;
        string docType;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;

                bool c = false;
                bool a = false;
                bool p = false;
                bool i = false;
                switch (docType)
                {
                    case "Answer a Complaint":
                        c = true;
                        a = false;
                        p = false;
                        i = false;
                        break;
                    case "Respond to Requests for Admission":
                        c = false;
                        a = false;
                        p = true;
                        i = false;
                        break;
                    case "Respond to Requests for Production of Documents":
                        c = false;
                        a = false;
                        p = true;
                        i = false;
                        break;
                    case "Respond to Interrogatories":
                        c = false;
                        a = false;
                        p = false;
                        i = true;
                        break;
                    default:
                        throw new Exception("docType incorrect");
                }

                repository.AddCustomResponse(name, c, a, p, i, text);

                this.FindForm().Close();

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
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #302"); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
