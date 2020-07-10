using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Exhibit;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.Controls.ExhibitControls
{
    public partial class ctrlExhibitFormat : UserControl
    {
        public ctrlExhibitFormat()
        {
            InitializeComponent();
            
            _app = Globals.ThisAddIn.Application;
            repository = ExhibitRepositoryFactory.GetRepository("XML",_app);

            LoadFormatting(_app);

            cbIntroMark.Text = enumSwitch.IntroOptions_EnumSwitchText(intro);
            cbNumbering.Text = enumSwitch.NumberingOptions_EnumSwitchText(numbering);
            cbFirstOnly.Text = enumSwitch.FirstOnlyOptions_EnumSwitchText(firstOnly);

            cbDescBatesFormat.Text = enumSwitch.DescBatesFormatOptions_EnumSwitchText(descBatesFormat);

            
            if ( parentheses == "True" ) { checkbParentheses.Checked = true; } else { checkbParentheses.Checked = false; }
            if( idCite == "True") { checkbIdCite.Checked = true; } else { checkbIdCite.Checked = false; }
            
        }
        readonly Word.Application _app;
        readonly IExhibitRepository repository;

        private IntroOptions intro;
        private NumberingOptions numbering;
        private FirstOnlyOptions firstOnly;
        private DescBatesFormatOptions descBatesFormat;
        private string parentheses; //"True" or "False"
        private string idCite; //"True" or "False"
        readonly EnumSwitch enumSwitch = new EnumSwitch();
        readonly ExhibitHelper helper = new ExhibitHelper();



        private void LoadFormatting(Word.Application _app)
        {
            intro = enumSwitch.IntroOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.Intro));
            numbering = enumSwitch.NumberingOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.Numbering));
            firstOnly = enumSwitch.FirstOnlyOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.FirstOnly));
            descBatesFormat = enumSwitch.DescBatesFormatOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.DescBatesFormat));
            parentheses = repository.GetFormatting(FormatNodes.Parentheses);
            idCite = repository.GetFormatting(FormatNodes.IdCite);

        }

        public string GetExampleTextFromDescBatesText(string text) //TODO: break this type of code into another group? example text updates
        {
            string exampleText;
            switch (text)
            {
                case "Description":
                    exampleText = "Description";
                    break;
                case "Description_Bates":
                    exampleText = "Description, Bates";
                    break;
                case "Description_P_Bates_P_":
                    exampleText = "Description (Bates)";
                    break;
                case "_P_Description_Bates_P_":
                    exampleText = "(Description, Bates)";
                    break;
                case "_P_Description_P_":
                    exampleText = "(Description)";
                    break;

                case "Description (Bates)":
                    exampleText = "Description (Bates)";
                    break;
                case "(Description, Bates)":
                    exampleText = "(Description, Bates)";
                    break;
                case "(Description)":
                    exampleText = "(Description)";
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return exampleText;
        }

        public void UpdateExampleCiteText()
        {
            string Intro = cbIntroMark.Text;
            string Numbering = cbNumbering.Text;
            string FirstOnly = cbFirstOnly.Text;
            string DescBatesFormat = cbDescBatesFormat.Text;
            string Parentheses = checkbParentheses.Checked.ToString();
            string IdCite = checkbIdCite.Checked.ToString();

            repository.UpdateFormatting(Intro, Numbering, FirstOnly, DescBatesFormat, Parentheses, IdCite);

            Exhibit exhibit = new Exhibit("Description", "BATES000123");

            LongCiteExampleText.Text = helper.FormatFirstCite(exhibit, 1, _app);
            ShortCiteExampleText.Text = helper.FormatFollowingCite(exhibit, 1, _app);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            ctrlExhibitView exhibitCtrl = new ctrlExhibitView();
            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(exhibitCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            exhibitCtrl.LoadListView();

            ActivePane.Visible = true;
            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
        }

        private void UpdateExhibitFormatting_Click(object sender, EventArgs e)
        {
            string Intro = cbIntroMark.Text;
            string Numbering = cbNumbering.Text;
            string FirstOnly = cbFirstOnly.Text;
            string DescBatesFormat = cbDescBatesFormat.Text;
            string Parentheses = checkbParentheses.Checked.ToString();
            string IdCite = checkbIdCite.Checked.ToString();

            repository.UpdateFormatting(Intro, Numbering, FirstOnly, DescBatesFormat, Parentheses, IdCite);

            helper.RefreshInsertedExhibits(_app);

            button3_Click(sender, e);

            Globals.ThisAddIn.ReturnFocus();


        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExampleCiteText();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExampleCiteText();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExampleCiteText();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExampleCiteText();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateExampleCiteText();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void LongCiteExampleText_Click(object sender, EventArgs e)
        {

        }
    }
}
