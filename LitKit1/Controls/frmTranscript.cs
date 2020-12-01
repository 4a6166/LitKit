using Tools;
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
using Tools.Simple;

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

            this.txtTranscriptText.MouseDown += new MouseEventHandler(this.txtTranscriptText_MouseDown);
            
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

        private void txtTranscriptText_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            string copiedText = Clipboard.GetText(TextDataFormat.UnicodeText);
            txtTranscriptText.SelectedText = copiedText;
        }

        private void frmTranscript_Load(object sender, EventArgs e)
        {

        }

        private void txtTranscriptText_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));
                        break;
                    }
            }
        }
    }

}
