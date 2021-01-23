using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.RedactionTool;
using Microsoft.Office.Interop.Word;

namespace LitKit1.Controls.RedactionControls
{
    public partial class ctrlConfidentialMarker : UserControl
    {
        public string Marker { get; private set; }
        public WdColorIndex Highlight = WdColorIndex.wdYellow; //TODO: add form for user to choose highlight color, if necessary
        public bool Aborted { get; private set; }
        public ctrlConfidentialMarker(bool ShowColorDialog)
        {
            InitializeComponent();

            if (ShowColorDialog)
            { ShowColorChooser(); }
            else
            { HighlightColorLabel.Visible = false; comboBox1.Visible = false; }

        }
        private void ShowColorChooser()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DisplayMember = "Yellow";
            HighlightColorLabel.Visible = true;
            comboBox1.Visible = true;

        }

        //public string ConfidentialityLabel;
        private string ConfidentialityLabel_other;

        private void Confidential_Click(object sender, EventArgs e)
        {
            Marker = Confidential.Text;
            this.FindForm().Close();
        }

        private void HighlyConfidential_Click(object sender, EventArgs e)
        {
            Marker = HighlyConfidential.Text;
            this.FindForm().Close();
        }

        private void AttorneysEyes_Click(object sender, EventArgs e)
        {
            Marker = AttorneysEyes.Text;
            this.FindForm().Close();
        }

        private void FiledUnderSeal_Click(object sender, EventArgs e)
        {
            Marker = FiledUnderSeal.Text;
            this.FindForm().Close();
        }

        private void InCamera_Click(object sender, EventArgs e)
        {
            Marker = InCamera.Text;
            this.FindForm().Close();
        }

        private void PersonalInfo_Click(object sender, EventArgs e)
        {
            Marker = PersonalInfo.Text;
            this.FindForm().Close();
        }

        private void HealthInfo_Click(object sender, EventArgs e)
        {
            Marker = HealthInfo.Text;
            this.FindForm().Close();
        }

        private void ProtectiveOrder_Click(object sender, EventArgs e)
        {
            Marker = ProtectiveOrder.Text;
            this.FindForm().Close();
        }

        private void ProtectedInformation_Click(object sender, EventArgs e)
        {
            Marker = ProtectedInformation.Text;
            this.FindForm().Close();
        }

        private void OtherLabel_Click(object sender, EventArgs e)
        {
            ConfidentialityLabel_other = OtherLabel_txt.Text;

            Marker = ConfidentialityLabel_other;
            this.FindForm().Close();
        }

        private void OtherLabel_txt_TextChanged(object sender, EventArgs e)
        {
            ConfidentialityLabel_other = OtherLabel_txt.Text;
        }

        private void NoLabel_Click(object sender, EventArgs e)
        {
            Marker = " ";
            this.FindForm().Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Marker = null;
            Aborted = true;
            this.FindForm().Close();
        }

        private void FiledUnderSeal_Click_1(object sender, EventArgs e)
        {
            Marker = FiledUnderSeal.Text;
            this.FindForm().Close();
        }

        private void OtherLabel_txt_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void OtherLabel_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                OtherLabel.PerformClick();
            }
        }

        private void ctrlConfidentialMarker_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "Yellow":
                    Highlight = WdColorIndex.wdYellow;
                    break;
                case "Dark Yellow":
                    Highlight = WdColorIndex.wdDarkYellow;
                    break;
                case "Bright Green":
                    Highlight = WdColorIndex.wdBrightGreen;
                    break;
                case "Green":
                    Highlight = WdColorIndex.wdGreen;
                    break;
                case "Teal":
                    Highlight = WdColorIndex.wdTeal;
                    break;
                case "Turquoise":
                    Highlight = WdColorIndex.wdTurquoise;
                    break;
                case "Blue":
                    Highlight = WdColorIndex.wdBlue;
                    break;
                case "Dark Blue":
                    Highlight = WdColorIndex.wdDarkBlue;
                    break;
                case "Violet":
                    Highlight = WdColorIndex.wdViolet;
                    break;
                case "Pink":
                    Highlight = WdColorIndex.wdPink;
                    break;
                case "Red":
                    Highlight = WdColorIndex.wdRed;
                    break;
                case "Dark Red":
                    Highlight = WdColorIndex.wdDarkRed;
                    break;
                case "Gray 25":
                    Highlight = WdColorIndex.wdGray25;
                    break;
                case "Gray 50":
                    Highlight = WdColorIndex.wdGray50;
                    break;

            }
        }
    }
}
