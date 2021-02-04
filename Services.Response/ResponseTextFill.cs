using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.Response
{
    public class ResponseTextFill
    {
        private List<string> ParaNumberLanguages = new List<string>()
        {
            "RESPONSE TO REQUEST FOR ADMISSION",
            "RESPONSE TO PARAGRAPH",
            "RESPONSE TO INTERROGATORY",
            "RESPONSE TO REQUEST",
            "RESPONSE TO REQUEST FOR PRODUCTION OF DOCUMENTS",
            "ANSWER TO PARAGRAPH",
            "RESPONSE TO RFA",
            "RESPONSE TO RFP",
            "RESPONSE TO REQUEST FOR PRODUCTION",
            "RESPONSE TO DOCUMENT REQUEST"
        };

        public string FillParaNumberForX(Selection selection)
        {
            try
            {
                string result = string.Empty;

                // Current Paragraph
                result = GetParaNumbers(selection.Paragraphs.First.Range.Text.ToUpper(), selection.Paragraphs.First);

                // Previous paragraph
                if (result == string.Empty || result == "")
                {
                        if (selection.Paragraphs.First.Previous(1) != null)
                        {

                            result = GetParaNumbers(selection.Paragraphs.First.Previous(1).Range.Text.ToUpper(), selection.Paragraphs.First.Previous(1));
                        }
                }

                // If above do not work
                if (result == string.Empty || result == "")
                { result = "[X]"; }

                return result;
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #307"); return null; }

        }

        private string GetParaNumbers(string text, Paragraph paragraph)
        {
            try
            {
                string result = string.Empty;
                int languageEndLength;
                foreach (string language in ParaNumberLanguages)
                {

                    if (text.Length < language.Length + 15)
                    {
                        languageEndLength = text.Length - 1;
                    }
                    else
                    {
                        languageEndLength = language.Length + 15;
                    }
                    try
                    {
                        if (language == text.Substring(0, language.Length))
                        {
                            for (int i = language.Length; i <= languageEndLength; i++)
                            {

                                try
                                {
                                    if (char.IsDigit(text[i]))
                                    {
                                        result += text[i];
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }

                if (result == string.Empty || result == "")
                {
                    if (paragraph.Range.ListParagraphs.Count > 0)
                    {
                        for (int i = 0; i <= paragraph.Range.ListFormat.ListString.Length - 1; i++)
                        {
                            if (char.IsDigit(paragraph.Range.ListFormat.ListString[i]))
                            {
                                result += paragraph.Range.ListFormat.ListString[i];
                            }
                        }
                    }
                    else
                    {
                        int ctLen;
                        if (text.Length > 5)
                        { ctLen = 5; }
                        else { ctLen = text.Length; }
                        for (int i = 0; i <= ctLen; i++)
                        {
                            try
                            {
                                if (char.IsDigit(text[i]))
                                {
                                    result += text[i];
                                }
                            }
                            catch { }
                        }
                    }
                }

                return result;
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #306"); return null; }

        }

    }
}
