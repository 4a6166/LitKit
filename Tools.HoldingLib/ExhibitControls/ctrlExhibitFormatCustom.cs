//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Microsoft.Office.Interop.Word;
//using Tools.Exhibit;

//namespace LitKit1.Controls.ExhibitControls
//{
//    public partial class ctrlExhibitFormatCustom : UserControl
//    {
//        public ctrlExhibitFormatCustom()
//        {
//            InitializeComponent();

//            _app = Globals.ThisAddIn.Application;
//            repository = new ExhibitRepository(_app);
//            helper = new ExhibitHelper(_app);

//            LoadFormatting(_app);
//            SetFormOptions();

//        }

//        Microsoft.Office.Interop.Word.Application _app;
//        ExhibitRepository repository;
//        ExhibitHelper helper;
//        readonly EnumSwitch enumSwitch = new EnumSwitch();


//        private string FirstCite;
//        private string FollowingCites;
//        private NumberingOptions IndexStyle;
//        private int IndexStart;
//        private bool UniformCites;
//        private bool idCite;
//        private bool FormatCustomized;


//        private void LoadFormatting(Microsoft.Office.Interop.Word.Application _app)
//        {
//            FirstCite = repository.GetFormatting(FormatNodes.FirstCite);
//            FollowingCites = repository.GetFormatting(FormatNodes.FollowingCites);

//            IndexStyle = enumSwitch.NumberingOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.IndexStyle));

//            IndexStart = Int32.Parse(repository.GetFormatting(FormatNodes.IndexStart));

//            UniformCites = bool.Parse(repository.GetFormatting(FormatNodes.UniformCites));
//            idCite = bool.Parse(repository.GetFormatting(FormatNodes.IdCite));
//            FormatCustomized = bool.Parse(repository.GetFormatting(FormatNodes.FormatCustomized));

//        }

//        private void SetFormOptions()
//        {
//            txtbxLongCustom.Text = FirstCite;
//            txtbxShortCustom.Text = FollowingCites;
//            checkBox1.Checked = idCite;

//            switch (IndexStyle)
//            {
//                case NumberingOptions.Numbers:
//                    comboBox1.SelectedIndex = 0;
//                    break;
//                case NumberingOptions.Letters:
//                    comboBox1.SelectedIndex = 0;
//                    break;
//                case NumberingOptions.RomanNumerals:
//                    comboBox1.SelectedIndex = 0;
//                    break;
//                default:
//                    throw new Exception("Correct Node not sent to method");
//            }

//            numericUpDown1.Value = IndexStart;

//        }

//        private void ReturnToExhibitList_click(object sender, EventArgs e)
//        {
//            ctrlExhibitView exhibitCtrl = new ctrlExhibitView();
//            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
//            ActivePane.Control.Controls.Clear();
//            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

//            ActivePane.Control.Controls.Add(exhibitCtrl);
//            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);
//            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
//            exhibitCtrl.LoadExhibitListView();

//            ActivePane.Visible = true;
//            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
//        }

//        private void Save_click(object sender, EventArgs e)
//        {
//            FirstCite = txtbxLongCustom.Text;
//            FollowingCites = txtbxShortCustom.Text;

//            switch (comboBox1.Text)
//            {
//                case "1, 2, 3...":
//                    IndexStyle = NumberingOptions.Numbers;
//                    break;
//                case "A, B, C...":
//                    IndexStyle = NumberingOptions.Letters;
//                    break;
//                case "I, II, III...":
//                    IndexStyle = NumberingOptions.RomanNumerals;
//                    break;
//                default:
//                    throw new Exception("ComboBox from numbering does not have acceptable input.");
//            }

//            IndexStart = (Int32) numericUpDown1.Value;

//            if (string.IsNullOrWhiteSpace(txtbxShortCustom.Text) || txtbxShortCustom.Text == txtbxLongCustom.Text)
//            {
//                UniformCites = true;
//            }
//            else UniformCites = false;

//            idCite = checkBox1.Checked;


//            string IStyle = enumSwitch.NumberingOptions_EnumSwitchText(IndexStyle);
//            string IStart = IndexStart.ToString();

//            repository.UpdateFormatting(FirstCite, FollowingCites, IStyle, IStart, UniformCites, idCite, true);

//            helper.UpdateInsertedCites();


//            ReturnToExhibitList_click(sender, e);
//        }

//        private void txtbxLongCustom_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void txtbxShortCustom_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void ToDefaultFormat_click(object sender, LinkLabelLinkClickedEventArgs e)
//        {
//            ctrlExhibitFormat exhibitCtrl = new ctrlExhibitFormat();
//            Microsoft.Office.Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];

//            ActivePane.Control.Controls.Clear();
//            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

//            ActivePane.Control.Controls.Add(exhibitCtrl);
//            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);

//            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

//            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
//            ActivePane.Visible = true;

//        }

//        private void checkBox1_CheckedChanged(object sender, EventArgs e)
//        {

//        }

//        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
//        {

//        }

//        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
//        {

//        }

//        private void panelCustomCite_Paint(object sender, PaintEventArgs e)
//        {

//        }

//        private void label8_Click(object sender, EventArgs e)
//        {

//        }

//        private void panel5_Paint(object sender, PaintEventArgs e)
//        {

//        }

//        private void label10_Click(object sender, EventArgs e)
//        {

//        }

//        private void label11_Click(object sender, EventArgs e)
//        {

//        }

//        private void panel4_Paint(object sender, PaintEventArgs e)
//        {

//        }

//        private void label9_Click(object sender, EventArgs e)
//        {

//        }

//        private void panel1_Paint(object sender, PaintEventArgs e)
//        {

//        }
//    }
//}
