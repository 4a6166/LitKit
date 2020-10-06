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

namespace LitKit1.Controls.RedactionControls
{
    public partial class ctrlConfidentialMarker : UserControl
    {
        public ctrlConfidentialMarker()
        {
            InitializeComponent();
        }


        //public string ConfidentialityLabel;
        private string ConfidentialityLabel_other;

        private void Confidential_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = Confidential.Text;
            this.FindForm().Close();
        }

        private void HighlyConfidential_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = HighlyConfidential.Text;
            this.FindForm().Close();
        }

        private void AttorneysEyes_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = AttorneysEyes.Text;
            this.FindForm().Close();
        }

        private void FiledUnderSeal_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = FiledUnderSeal.Text;
            this.FindForm().Close();
        }

        private void InCamera_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = InCamera.Text;
            this.FindForm().Close();
        }

        private void PersonalInfo_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = PersonalInfo.Text;
            this.FindForm().Close();
        }

        private void HealthInfo_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = HealthInfo.Text;
            this.FindForm().Close();
        }

        private void ProtectiveOrder_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = ProtectiveOrder.Text;
            this.FindForm().Close();
        }

        private void ProtectedInformation_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = ProtectedInformation.Text;
            this.FindForm().Close();
        }

        private void OtherLabel_Click(object sender, EventArgs e)
        {
            ConfidentialityLabel_other = OtherLabel_txt.Text;

            Redactions.ConfidentialityLabel = ConfidentialityLabel_other;
            this.FindForm().Close();
        }

        private void OtherLabel_txt_TextChanged(object sender, EventArgs e)
        {
            ConfidentialityLabel_other = OtherLabel_txt.Text;
        }

        private void NoLabel_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = " ";
            this.FindForm().Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = null;
            Redactions.cancel = true;
            this.FindForm().Close();
        }

        private void FiledUnderSeal_Click_1(object sender, EventArgs e)
        {
            Redactions.ConfidentialityLabel = FiledUnderSeal.Text;
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
    }
}
