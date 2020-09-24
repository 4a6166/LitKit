using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace Tools.Exhibit
{
    public class Pincite
    {
        public Pincite(Microsoft.Office.Interop.Word.Application _app)
        {
            this._app = _app;
            repository = new ExhibitRepository(_app);
        }

        Microsoft.Office.Interop.Word.Application _app;
        ExhibitRepository repository;

        public void AddPincite(Selection sel)
        {
            var cc = GetCCForPINCITE(sel);
            if (cc.Title.Contains("|PIN"))
            {
                MessageBox.Show("This Exhibit already has an inserted PINCITE");
            }
            else
            {
                cc.LockContents = false;

                // Update Exhibit, passing in PINCITE as filled text.
                Exhibit exhibit = new Exhibit(cc.ID);
                int Index = Int32.Parse(cc.Tag.Split('|')[1]); //TODO: update tag: EX|INDEX|ID

                string CiteText = FormatFirstCite(exhibit, Index);

                // Create range over filled text

                var SplitArray = new string[] { "[PINCITE]" };
                int PinInCiteText = CiteText.Split(SplitArray, StringSplitOptions.None)[0].Length;

                Range pinCiteRange = cc.Range;
                pinCiteRange.Start = cc.Range.Start + PinInCiteText;
                pinCiteRange.End = pinCiteRange.Start + SplitArray[0].Length;


                // use below to create PINCITE cc


                var pinCiteCC = sel.ContentControls.Add(WdContentControlType.wdContentControlRichText, pinCiteRange);
                pinCiteCC.SetPlaceholderText(null, null, "{type Pincite text}");
                pinCiteCC.Range.Text = string.Empty;
                pinCiteCC.Range.Italic = 0;
                pinCiteCC.Tag = "PINCITE:" + cc.Tag;

                sel.SetRange(pinCiteCC.Range.Start, pinCiteCC.Range.End); // so user can begin typing into the Pincite right after it is inserted.
                cc.Title += "|PIN";
                cc.LockContents = true;
            }
        }
        public void ReAddPincite(Selection sel, string PinCiteText)
        {
            string pinCiteText = PinCiteText;
            var cc = GetCCForPINCITE(sel);

            cc.LockContents = false;

            Range pinCiteRange = cc.Range;

            var DescBatesFormat = repository.GetFormatting(FormatNodes.DescBatesFormat);
            var FirstOnly = repository.GetFormatting(FormatNodes.FirstOnly);
            var Parens = repository.GetFormatting(FormatNodes.Parentheses);
            if (Parens == "False")
            {
                if (FirstOnly != "In no citations" & DescBatesFormat.StartsWith("(") & cc.Range.Text.Contains('('))
                {
                    var ccTextSplit = cc.Range.Text.Split('(');
                    pinCiteRange.SetRange(cc.Range.Start + ccTextSplit[0].Length - 1, cc.Range.Start + ccTextSplit[0].Length - 1);

                }

                else
                {
                    cc.Range.Text = cc.Range.Text + ".";

                    pinCiteRange.SetRange(cc.Range.End - 1, cc.Range.End);
                }
            }
            else
            {  /// if updates here, also update logic in public void AddPincite(Word.Selection sel)
                if (FirstOnly != "In no citations" & DescBatesFormat.StartsWith("(") & cc.Range.Text.Contains('('))
                {
                    var ccTextSplit = cc.Range.Text.Split('(');
                    pinCiteRange.SetRange(cc.Range.Start + ccTextSplit[0].Length + ccTextSplit[1].Length + 1, cc.Range.Start + ccTextSplit[0].Length + ccTextSplit[1].Length + 1);
                }
                else
                {
                    //cc.Range.Text = cc.Range.Text + ".";

                    pinCiteRange.SetRange(cc.Range.End - 1, cc.Range.End - 1);
                }

                pinCiteText = pinCiteText.Trim();
            }

            var pinCiteCC = sel.ContentControls.Add(WdContentControlType.wdContentControlRichText, pinCiteRange);
            pinCiteCC.SetPlaceholderText(null, null, "{type Pincite text}");
            if (PinCiteText == "{type Pincite text}")
            {
                pinCiteCC.Range.Text = string.Empty;
            }
            else pinCiteCC.Range.Text = pinCiteText;
            pinCiteCC.Tag = "PINCITE:" + cc.Tag;
            pinCiteCC.Range.Italic = 0;

            cc.LockContents = true;

        }
        public ContentControl GetCCForPINCITE(Selection sel)
        {
            int ccCount = sel.ContentControls.Count;
            ContentControl cc = null;

            switch (ccCount)
            {
                case int n when n < 1:
                    cc = sel.ParentContentControl;
                    if (cc == null)
                    {
                        MessageBox.Show("Please select an Exhibit requiring a PINCITE.", "Warning");
                    }
                    break;
                case 1:
                    if (sel.ContentControls[1].Tag.Contains("Exhibit:"))
                    {
                        cc = sel.ContentControls[1];
                    }
                    else
                    {
                        MessageBox.Show("Please select an Exhibit requiring a PINCITE.", "Warning");
                    }
                    break;
                case int n when n > 1:
                    MessageBox.Show("Please select one Exhibit per PINCITE.", "Warning");
                    break;
                default:
                    throw new Exception("error in selecting Exhibits");
            }

            if (cc != null)
            {
                return cc;
            }
            else return null;
        }
        public string GeExhibitIDFromTag(ContentControl cc) //replaced cc.Tag where referenced
        {
            string ID = string.Empty;
            ID = cc.Tag.Substring(8);
            ID = ID.Split('|')[0];

            return ID;
        }
        public void RemovePinCite(Selection selection)
        {
            ContentControl cc = GetCCForPINCITE(selection);
            if (selection.ContentControls.Count > 0 && selection.ContentControls[1].Tag.Contains("PINCITE"))
            {
                cc = selection.ContentControls[1].ParentContentControl;
                cc.LockContents = false;
                selection.ContentControls[1].Delete(true);
                cc.Title = cc.Title.Split('|')[0];
                cc.LockContents = true;
            }
            else if (cc.Tag.Contains("PINCITE"))
            {
                ContentControl ChildCC = cc;
                cc = cc.ParentContentControl;
                cc.LockContents = false;
                ChildCC.Delete(true);
                cc.Title = cc.Title.Split('|')[0];
                cc.LockContents = true;
            }
            else if (cc.Title.Contains("|PIN"))
            {
                var ChildCCs = cc.Range.ContentControls;
                foreach (ContentControl ChildCC in ChildCCs)
                {
                    if (ChildCC.Tag.Contains("PINCITE"))
                    {
                        cc.LockContents = false;
                        ChildCC.Delete(true);
                        cc.Title = cc.Title.Split('|')[0];
                        cc.LockContents = true;
                    }
                }
            }

        }
    }
}
