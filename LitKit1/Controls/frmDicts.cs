using Services.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Simple;

namespace LitKit1.Controls
{
    public partial class frmDicts : Form
    {
        string UpdateText;
        string DictType;
        string filename;
        string filepath;
        bool pulledStandardDict;

        public frmDicts(string DictType)
        {
            InitializeComponent();
            this.DictType = DictType;
            
            tbEntries.Text = LoadText();

        }

        private string LoadText()
        {
            if(DictType == "Latin")
            {
                this.Text = "Update Latin Words";
                filename = @"LatinDict.dic";
            }
            else 
            {
                this.Text = "Update Sentence Spacing Abbreviations";
                filename = @"SentenceSpacingDict.dic"; 
            }

            filepath = Dicts.GetExpressionFilePath(filename, out pulledStandardDict);
            StreamReader reader = new StreamReader(filepath);

            UpdateText = reader.ReadToEnd();
            reader.Close();
            return UpdateText;
        }

        private void frmDicts_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DictType == "Latin")
            {
                LatinExpressions latin = new LatinExpressions();
                latin.UpdateExpressionFile(tbEntries.Text);
            }
            else 
            {
                SpaceBetweenSentences spaces = new SpaceBetweenSentences();
                spaces.UpdateAbbreviationsFile(tbEntries.Text);
            }

            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var standardDicPath = Dicts.getStandardDict(filename);

            StreamReader reader = new StreamReader(standardDicPath);
            tbEntries.Text = reader.ReadToEnd();

            reader.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
