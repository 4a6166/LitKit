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
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.Controls.ExhibitControls
{
    public partial class ctrlExhibitFormat : UserControl
    {
        public ctrlExhibitFormat()
        {
            InitializeComponent();
            
            _app = Globals.ThisAddIn.Application;
            repository = new ExhibitRepository(_app);
            helper = new ExhibitHelper(_app);

            LoadFormatting(_app);

            cbIntroMark.Text = intro;
            cbNumbering.Text = enumSwitch.NumberingOptions_EnumSwitchText(IndexStyle);
            cbUniformCitesStandard.Checked = !UniformCites;

            cbDescBatesFormat.Text = (descBatesFormat);

            
            if ( parentheses == "True" ) { checkbParentheses.Checked = true; } else { checkbParentheses.Checked = false; }
            if( idCite == true) { checkbIdCite.Checked = true; } else { checkbIdCite.Checked = false; }
            
        }
        readonly Word.Application _app;
        readonly ExhibitRepository repository;

        private string FirstCite;
        private string FollowingCites;
        private NumberingOptions IndexStyle;
        private int IndexStart;
        private bool UniformCites;
        private bool idCite; //"True" or "False"
        private bool FormatCustomized;

        private string intro;
        private string descBatesFormat;
        private string parentheses;

        readonly EnumSwitch enumSwitch = new EnumSwitch();
        readonly ExhibitHelper helper;



        private void LoadFormatting(Word.Application _app)
        {
            FirstCite = repository.GetFormatting(FormatNodes.FirstCite);
            FollowingCites = repository.GetFormatting(FormatNodes.FollowingCites);

            IndexStyle = enumSwitch.NumberingOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.IndexStyle));

            IndexStart = Int32.Parse(repository.GetFormatting(FormatNodes.IndexStart));

            UniformCites = bool.Parse(repository.GetFormatting(FormatNodes.UniformCites));
            idCite = bool.Parse(repository.GetFormatting(FormatNodes.IdCite));
            FormatCustomized = bool.Parse(repository.GetFormatting(FormatNodes.FormatCustomized));

            intro = repository.GetFormatting(FormatNodes.Intro);
            descBatesFormat = repository.GetFormatting(FormatNodes.DescBatesFormat);
            parentheses = repository.GetFormatting(FormatNodes.Parentheses);


            cbDescBatesFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIntroMark.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNumbering.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public string GetExampleTextFromDescBatesText(string text) 
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

            Exhibit exhibit = new Exhibit("Description", "BATES000123");

            LongCiteExampleText.Text = ExhibitFormatter.FormatCite(exhibit, FirstCite, IndexStyle, 1, 1);
            ShortCiteExampleText.Text = ExhibitFormatter.FormatCite(exhibit, FollowingCites, IndexStyle, 1, 1);
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
            UpdateExampleCiteText();

            helper.RefreshInsertedExhibits();

            EnumSwitch enumSwitch = new EnumSwitch();

            repository.UpdateFormatting(FirstCite, FollowingCites, enumSwitch.NumberingOptions_EnumSwitchText(IndexStyle), IndexStart.ToString(), UniformCites, idCite, FormatCustomized);

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
            // Don't update examples. Check state is pulled when Saved.
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

        private void ctrlExhibitFormat_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            panelCustomCite.Visible = false;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            checkbIdCite.Checked = checkBox1.Checked;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panelCustomCite_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCustomizeFormatting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkBox1.Checked = checkbIdCite.Checked;
            comboBox1.SelectedIndex = cbNumbering.SelectedIndex;

            panelCustomCite.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbNumbering.SelectedIndex = comboBox1.SelectedIndex;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            UpdateExampleCiteText();

            helper.RefreshInsertedExhibits();

            button3_Click(sender, e);

            Globals.ThisAddIn.ReturnFocus();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateExampleCiteText();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
