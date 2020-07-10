using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace LitKit1.Controls
{
    public partial class frmTranscript : Form
    {
        public frmTranscript(InLineOrBlock QuoteType)
        {
            InitializeComponent();
            this.QuoteType = QuoteType;
            if (QuoteType == InLineOrBlock.InLine)
            {
                this.Text = "Insert transcript text as an In-Line quote.";
            }
            else this.Text = "Insert transcript text as a Block quote.";

            this._app = Globals.ThisAddIn.Application;
        }

        Word.Application _app;
        public InLineOrBlock QuoteType;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Transcript transcript = new Transcript(_app);
            if (QuoteType == InLineOrBlock.InLine)
            {
                transcript.PasteAsInText(txtTranscriptText.Text);
            }
            else if (QuoteType == InLineOrBlock.Block)
            {
                transcript.PasteAsBlockQuote(txtTranscriptText.Text);
            }
            this.Close();
        }
    }

}
