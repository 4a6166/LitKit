using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Services.Exhibit
{


    public class ExhibitHelper
    {
        public ExhibitHelper()
        {

        }
        public IExhibitRepository repository;
        public string ToAlphabet(int number)
        {
            string strAlpha = "";
            switch (number)
            {
                case int n when (0 <= n && n <= 26):
                    strAlpha += ((char)(n + 64)).ToString();
                    break;
                case int n when (26 < n && n <= 26 * 2):
                    strAlpha += "A" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 2 < n && n <= 26 * 3):
                    strAlpha += "B" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 3 < n && n <= 26 * 4):
                    strAlpha += "C" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 4 < n && n <= 26 * 5):
                    strAlpha += "D" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 5 < n && n <= 26 * 6):
                    strAlpha += "E" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 6 < n && n <= 26 * 7):
                    strAlpha += "F" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 7 < n && n <= 26 * 8):
                    strAlpha += "G" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 8 < n && n <= 26 * 9):
                    strAlpha += "H" + ((char)(n % 26 + 64)).ToString();
                    break;
                case int n when (26 * 9 < n && n <= 26 * 10):
                    strAlpha += "I" + ((char)(n % 26 + 64)).ToString();
                    break;

                default:
                    strAlpha = "";
                    break;
            }
            return strAlpha;
        }
        public string ToRoman(int number)
        {
            // https://stackoverflow.com/questions/7040289/converting-integers-to-roman-numerals
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            else return number.ToString();
        }
        public string ApplyNumFormat(int index, NumberingOptions NumberFormat)
        {
            string numbering = string.Empty;
            switch (NumberFormat)
            {
                case NumberingOptions.Numbers:
                    numbering = index.ToString();
                    break;
                case NumberingOptions.Letters:
                    numbering = ToAlphabet(index);
                    break;
                case NumberingOptions.RomanNumerals:
                    numbering = ToRoman(index);
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return numbering;
        }
        public string ApplyNumFormat(int index, string NumberFormat)
        {
            NumberingOptions numberFormat = new EnumSwitch().NumberingOptions_TextSwitchEnum(NumberFormat);
            return ApplyNumFormat(index, numberFormat);
        }

        public string ApplyDescBatesFormat(string Description, string BatesNumber, DescBatesFormatOptions DescBatesFormat)
        {
            string DescBates = string.Empty;
            if (string.IsNullOrWhiteSpace(BatesNumber))
            {
                switch (DescBatesFormat)
                {
                    case DescBatesFormatOptions.Description:
                        DescBates = ", " + Description;
                        break;
                    case DescBatesFormatOptions.Description_Bates:
                        DescBates = ", " + Description;
                        break;
                    case DescBatesFormatOptions.Description_P_Bates_P_:
                        DescBates = ", " + Description;
                        break;
                    case DescBatesFormatOptions._P_Description_Bates_P_:
                        DescBates = " (" + Description + ")";
                        break;
                    case DescBatesFormatOptions._P_Description_P_:
                        DescBates = " (" + Description + ")";
                        break;
                    default:
                        throw new Exception("Correct text not sent to method");
                }
            }
            else
            {
                switch (DescBatesFormat)
                {
                    case DescBatesFormatOptions.Description:
                        DescBates = ", " + Description;
                        break;
                    case DescBatesFormatOptions.Description_Bates:
                        DescBates = ", " + Description + ", " + BatesNumber;
                        break;
                    case DescBatesFormatOptions.Description_P_Bates_P_:
                        DescBates = ", " + Description + " (" + BatesNumber + ")";
                        break;
                    case DescBatesFormatOptions._P_Description_Bates_P_:
                        DescBates = " (" + Description + ", " + BatesNumber + ")";
                        break;
                    case DescBatesFormatOptions._P_Description_P_:
                        DescBates = " (" + Description + ")";
                        break;
                    default:
                        throw new Exception("Correct text not sent to method");
                }
            }
            return DescBates;
        }


        public string ApplyDescBatesFormat(string Description, string BatesNumber, string DescBatesFormat)
        {
            DescBatesFormatOptions descBatesFormat = new EnumSwitch().DescBatesFormatOptions_TextSwitchEnum(DescBatesFormat);
            return ApplyDescBatesFormat(Description, BatesNumber, descBatesFormat);
        }



        public string FormatFirstCite(Exhibit exhibit, int Index, Word.Application _app)
        {
            string result = string.Empty;

            repository = ExhibitRepositoryFactory.GetRepository("XML", _app);

            string intro = repository.GetFormatting(FormatNodes.Intro);
            string numbering = repository.GetFormatting(FormatNodes.Numbering);

            string firstOnly = repository.GetFormatting(FormatNodes.FirstOnly);
            string descBates = string.Empty;
            if (firstOnly != "In no citations") 
            {
                DescBatesFormatOptions descBatesFormat = new EnumSwitch().DescBatesFormatOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.DescBatesFormat));
                descBates = ApplyDescBatesFormat(exhibit.Description, exhibit.BatesNumber, descBatesFormat); 
            }

            string parentheses = repository.GetFormatting(FormatNodes.Parentheses);
            string firstParen = string.Empty;
            string secondParen = string.Empty;
            if (parentheses == "True") { firstParen = "("; secondParen = ")"; }

            result = firstParen + intro + "\u00A0" + ApplyNumFormat(Index, numbering) + descBates  + secondParen;

            return result;
        }

        public string FormatFollowingCite(Exhibit exhibit, int Index, Word.Application _app)
        {
            string result = string.Empty;

            repository = ExhibitRepositoryFactory.GetRepository("XML", _app);

            string intro = repository.GetFormatting(FormatNodes.Intro);
            string numbering = repository.GetFormatting(FormatNodes.Numbering);

            string firstOnly = repository.GetFormatting(FormatNodes.FirstOnly);
            string descBates = string.Empty;
            if (firstOnly == "In all citations") 
            {
                DescBatesFormatOptions descBatesFormat = new EnumSwitch().DescBatesFormatOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.DescBatesFormat));
                descBates = ApplyDescBatesFormat(exhibit.Description, exhibit.BatesNumber, descBatesFormat); 
            }

            string parentheses = repository.GetFormatting(FormatNodes.Parentheses);
            string firstParen = string.Empty;
            string secondParen = string.Empty;
            if (parentheses == "True") { firstParen = "("; secondParen = ")"; }

            result = firstParen + intro + "\u00A0" + ApplyNumFormat(Index, numbering) +  descBates  + secondParen;

            return result;
        }
        public string FormatIdExhibit(Word.Range range, int Index, Word.Application _app)
        {
            try
            {
                _app.Selection.SetRange(range.Start - 6, range.Start-1);

                if (_app.Selection.Range.Text.Contains(",") || _app.Selection.Range.Text.Contains("See") || _app.Selection.Range.Text.Contains("see") || _app.Selection.Range.Text.Contains("e.g.") || _app.Selection.Range.Text.Contains("cf.") || _app.Selection.Range.Text.Contains("Cf.") || _app.Selection.Range.Text.Contains("CF."))
                {
                    return "id.";
                }
                else if (_app.Selection.Range.Text.Contains(".") || _app.Selection.Range.Text.Contains("\r\n") || _app.Selection.Range.Text.Contains("\r") || _app.Selection.Range.Text.Contains("\n")) 
                { 
                    return "Id."; 
                }
                else
                { 
                    return "id."; 
                }                
            }
            catch { return "id."; }
        }

        public int GetPosition(string tag, Word.Application _app)
        {
            List<ExhibitsReference> references = OrderExhibits(_app);
            int index = 0;

            string ID = tag.Substring(8);
            ExhibitsReference refer = references.Where(n => n.ExhibtId == ID).FirstOrDefault();

            index = references.IndexOf(refer);

            return index+1;
            
        }

        private List<ExhibitsReference> OrderExhibits(Word.Application _app)
        {

            List<ExhibitsReference> references = new List<ExhibitsReference>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                {
                    ExhibitsReference r = new ExhibitsReference(cc.Tag.Substring(8),cc.Range.Start,0,cc.ID);
                    references.Add(r);
                }
            }


            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ExhibitsReference r = new ExhibitsReference(cc.Tag.Substring(8), fn.Reference.Start, fn.Range.Start, cc.ID);
                        references.Add(r);
                        // fn.Reference.Start should give the start position of the footnote holding the exhibit. Need to figure out what to do if there are multiple exhibits in the same footnote. Also need to make any changes her to the Endnote search as well.
                    }
                }
            }

            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ExhibitsReference r = new ExhibitsReference(cc.Tag.Substring(8), en.Reference.Start, en.Range.Start, cc.ID);
                        references.Add(r);
                    }
                }
            }

            return references = references.OrderBy(reference => reference.RangeStart).ThenBy(note => note.NoteRangeStart).ToList();

        }

        public List<ContentControl> GetExhibitsInDocument(Word.Application _app)
        {

            List<ContentControl> ccs = new List<ContentControl>();

            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                {
                    ccs.Add(cc);
                }
            }


            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ccs.Add(cc);
                    }
                }
            }

            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        ccs.Add(cc);
                    }
                }
            }

            return ccs;

        }

        public void RefreshInsertedExhibits(Word.Application _app)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<ContentControl> ccs = GetExhibitsInDocument(_app);
            List<ExhibitsReference> ccRefs = OrderExhibits(_app);
            List<string> CcIDsInOrder = new List<string>();
            foreach (ExhibitsReference eRef in ccRefs)
            {
                CcIDsInOrder.Add(eRef.CcId);
            }

            ccs = ccs.OrderBy(cc => CcIDsInOrder.IndexOf(cc.ID)).ToList();

            repository = ExhibitRepositoryFactory.GetRepository("XML", _app);
            string idCite = repository.GetFormatting(FormatNodes.IdCite);

            List<string> insertedCiteTags = new List<string> { "FillItem" };
            List<string> CiteTagsIndex = new List<string> { "FillItem" };

            List<string> allBodyIDs = new List<string>();
            foreach (ContentControl cc in _app.ActiveDocument.ContentControls)
            {
                if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                {
                    allBodyIDs.Add(cc.ID);
                }
            }
            List<string> insertedBodyTags = new List<string> { "FillItem" };

            List<string> allFootNoteIDs = new List<string>(); 
            foreach (Word.Footnote fn in _app.ActiveDocument.Footnotes)
            {
                foreach (ContentControl cc in fn.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        allFootNoteIDs.Add(cc.ID);
                    }
                }
            }
            List<string> insertedFootNoteTags = new List<string> { "FillItem" };

            List<string> allEndNoteIDs = new List<string>();
            foreach (Word.Endnote en in _app.ActiveDocument.Endnotes)
            {
                foreach (ContentControl cc in en.Range.ContentControls)
                {
                    if (cc.Tag != null && cc.Tag.StartsWith("Exhibit:"))
                    {
                        allEndNoteIDs.Add(cc.ID);
                    }
                }
            }
            List<string> insertedEndNoteTags = new List<string> { "FillItem" };

            string text = string.Empty;
            string ID = string.Empty;

            foreach (ContentControl cc in ccs)
            {
                string PinCiteText = string.Empty;
                if(cc.Title.Contains("|PIN"))
                {
                    foreach (ContentControl ccChild in cc.Range.ContentControls)
                    {
                        if (ccChild.Tag.Contains("PINCITE:"))
                        {
                            if (ccChild.Range.Text == "{type PinCite text}")
                            {
                                PinCiteText = string.Empty;
                            }
                            else PinCiteText = ccChild.Range.Text;
                        }
                    }
                }

                #region exhibit is in footnotes
                if (allFootNoteIDs.Contains(cc.ID))
                {
                    // Start formatting Exhibit text, PINCITE is re-added after
                    if (idCite == "True" && cc.Tag == insertedFootNoteTags.Last())  //Id cites
                    {
                        int index = CiteTagsIndex.IndexOf(cc.Tag);
                        Word.Selection sel = _app.Selection;
                        sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
                        text = FormatIdExhibit(sel.Range, index, _app);
                        insertedCiteTags.Add(cc.Tag);
                        insertedFootNoteTags.Add(cc.Tag);
                    }
                    else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
                    {
                        int index = CiteTagsIndex.IndexOf(cc.Tag);
                        insertedFootNoteTags.Add(cc.Tag);

                        ID = cc.Tag.Substring(8);
                        Exhibit exhibit = repository.GetExhibit(ID);

                        text = FormatFollowingCite(exhibit, index, _app);
                    }
                    else // first cites
                    {
                        int index = CiteTagsIndex.Count;
                        CiteTagsIndex.Add(cc.Tag);
                        insertedFootNoteTags.Add(cc.Tag);

                        ID = cc.Tag.Substring(8);
                        Exhibit exhibit = repository.GetExhibit(ID);

                        text = FormatFirstCite(exhibit, index, _app);
                    }

                    cc.LockContents = false;
                    cc.Range.Text = text;

                    if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
                    {
                        cc.Range.Italic = -1;
                    }
                    else cc.Range.Italic = 0;

                    cc.LockContents = true;

                    //Re-adds PINCITE following full formatting of text in Exhibit
                    if (cc.Title.Contains("|PIN"))
                    {
                        cc.Range.Select();
                        Word.Selection sel = _app.Selection;
                        sel.SetRange(cc.Range.Start, cc.Range.End);
                        ReAddPincite(sel, PinCiteText);
                    }
                }

                #endregion
                #region exhibit is in endnotes
                if (allEndNoteIDs.Contains(cc.ID))
                {
                    // Start formatting Exhibit text, PINCITE is re-added after
                    if (idCite == "True" && cc.Tag == insertedEndNoteTags.Last())  //Id cites
                    {
                        int index = CiteTagsIndex.IndexOf(cc.Tag);
                        Word.Selection sel = _app.Selection;
                        sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
                        text = FormatIdExhibit(sel.Range, index, _app);
                        insertedCiteTags.Add(cc.Tag);
                        insertedEndNoteTags.Add(cc.Tag);
                    }
                    else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
                    {
                        int index = CiteTagsIndex.IndexOf(cc.Tag);
                        insertedEndNoteTags.Add(cc.Tag);

                        ID = cc.Tag.Substring(8);
                        Exhibit exhibit = repository.GetExhibit(ID);

                        text = FormatFollowingCite(exhibit, index, _app);
                    }
                    else // first cites
                    {
                        int index = CiteTagsIndex.Count;
                        CiteTagsIndex.Add(cc.Tag);
                        insertedEndNoteTags.Add(cc.Tag);

                        ID = cc.Tag.Substring(8);
                        Exhibit exhibit = repository.GetExhibit(ID);

                        text = FormatFirstCite(exhibit, index, _app);
                    }

                    cc.LockContents = false;
                    cc.Range.Text = text;

                    if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
                    {
                        cc.Range.Italic = -1;
                    }
                    else cc.Range.Italic = 0;

                    cc.LockContents = true;

                    //Re-adds PINCITE following full formatting of text in Exhibit
                    if (cc.Title.Contains("|PIN"))
                    {
                        cc.Range.Select();
                        Word.Selection sel = _app.Selection;
                        sel.SetRange(cc.Range.Start, cc.Range.End);
                        ReAddPincite(sel, PinCiteText);
                    }
                }

                #endregion

                #region exhibit is in body text
                if (allBodyIDs.Contains(cc.ID))
                {
                    // Start formatting Exhibit text, PINCITE is re-added after
                    if (idCite == "True" && cc.Tag == insertedBodyTags.Last())  //Id cites
                    {
                        int index = CiteTagsIndex.IndexOf(cc.Tag);
                        Word.Selection sel = _app.Selection;
                        sel.SetRange(cc.Range.Start, cc.Range.End); // needed to bring the selection in/out of the footnotes/endnotes, which have their own range start
                        text = FormatIdExhibit(sel.Range, index, _app);
                        insertedCiteTags.Add(cc.Tag);
                        insertedBodyTags.Add(cc.Tag);
                    }
                    else if (CiteTagsIndex.Contains(cc.Tag)) //Following cites
                    {
                        int index = CiteTagsIndex.IndexOf(cc.Tag);
                        insertedBodyTags.Add(cc.Tag);

                        ID = cc.Tag.Substring(8);
                        Exhibit exhibit = repository.GetExhibit(ID);

                        text = FormatFollowingCite(exhibit, index, _app);
                    }
                    else // first cites
                    {
                        int index = CiteTagsIndex.Count;
                        CiteTagsIndex.Add(cc.Tag);
                        insertedBodyTags.Add(cc.Tag);

                        ID = cc.Tag.Substring(8);
                        Exhibit exhibit = repository.GetExhibit(ID);

                        text = FormatFirstCite(exhibit, index, _app);
                    }

                    cc.LockContents = false;
                    cc.Range.Text = text;

                    if (cc.Range.Text != null && (cc.Range.Text.Contains("Id.") || cc.Range.Text.Contains("id.")))
                    {
                        cc.Range.Italic = -1;
                    }
                    else cc.Range.Italic = 0;

                    cc.LockContents = true;

                    //Re-adds PINCITE following full formatting of text in Exhibit
                    if (cc.Title.Contains("|PIN"))
                    {
                        cc.Range.Select();
                        Word.Selection sel = _app.Selection;
                        sel.SetRange(cc.Range.Start, cc.Range.End);
                        ReAddPincite(sel, PinCiteText);
                    }
                }
                #endregion
            }

            Cursor.Current = Cursors.Default;
        }

        public void RemoveExhibitsFromDoc(Word.Application _app)
        {
            List<ContentControl> ccs = GetExhibitsInDocument(_app);
            foreach (ContentControl cc in ccs)
            {
                cc.Delete(false);
            }
        }
        public void RemoveExhibitsFromDoc(Word.Selection Selection)
        {
            ContentControls ccs = Selection.ContentControls;
            foreach (ContentControl cc in ccs)
            {
                if (cc.Tag.Contains("Exhibit:"))
                {
                    cc.Delete(false);
                }
            }
        }

        public void InsertExhibitIndex(Word.Application _app)
        {
            List<ContentControl> exhibits = GetExhibitsInDocument(_app);
            List<string> tags = new List<string> { "FillItem" };

            _app.ActiveDocument.Tables.Add(_app.Selection.Range, 2, 2, WdDefaultTableBehavior.wdWord9TableBehavior, WdAutoFitBehavior.wdAutoFitFixed);
            _app.Selection.Font.Bold = (int)WdConstants.wdToggle;
            _app.Selection.TypeText("Exhibit No.");
            _app.Selection.MoveRight(WdUnits.wdCell);
            _app.Selection.Font.Bold = (int)WdConstants.wdToggle;
            _app.Selection.TypeText("Exhibit Description");
            _app.Selection.MoveRight(WdUnits.wdCell);

            var exhibitCount = exhibits.Count();
            var Description = string.Empty;
            repository = ExhibitRepositoryFactory.GetRepository("XML", _app);
            var Numbering = repository.GetFormatting(FormatNodes.Numbering);
            int Index = 0;

            foreach (var exhibit in exhibits)
            {
                var repoExhibit = repository.GetExhibit(exhibit.Tag.Substring(8));

                Description = repoExhibit.Description;
                if (tags.Contains(exhibit.Tag))
                { 
                }
                else
                {
                    exhibitCount--;

                    tags.Add(exhibit.Tag);
                    Index = tags.Count-1;

                    _app.Selection.TypeText(ApplyNumFormat(Index, Numbering));
                    _app.Selection.MoveRight(WdUnits.wdCell);
                    _app.Selection.TypeText(Description);

                    if (exhibitCount > 0)
                        _app.Selection.MoveRight(WdUnits.wdCell);

                }
            }

        }

        public void AddPincite(Word.Selection sel)
        {
            repository = ExhibitRepositoryFactory.GetRepository("XML", sel.Application);
            var cc = GetCCForPINCITE(sel);
            if (cc.Title.Contains("|PIN"))
            {
                MessageBox.Show("This Exhibit already has an inserted PINCITE");
            }
            else
            {
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
                {
                    if (FirstOnly != "In no citations" & DescBatesFormat.StartsWith("(") & cc.Range.Text.Contains('('))
                    {
                        var ccTextSplit = cc.Range.Text.Split('(');
                        pinCiteRange.SetRange(cc.Range.Start + ccTextSplit[0].Length+ccTextSplit[1].Length, cc.Range.Start + ccTextSplit[0].Length+ccTextSplit[1].Length);
                    }
                    else
                    {
                        cc.Range.Text = cc.Range.Text + ".";

                        pinCiteRange.SetRange(cc.Range.End - 1, cc.Range.End);
                    }
                }

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
        public void ReAddPincite(Word.Selection sel, string PinCiteText)
        {
            string pinCiteText = PinCiteText;
            repository = ExhibitRepositoryFactory.GetRepository("XML", sel.Application);
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
                    pinCiteRange.SetRange(cc.Range.Start + ccTextSplit[0].Length+ccTextSplit[1].Length+1, cc.Range.Start + ccTextSplit[0].Length+ccTextSplit[1].Length+1);
                }
                else
                {
                    //cc.Range.Text = cc.Range.Text + ".";

                    pinCiteRange.SetRange(cc.Range.End - 1, cc.Range.End-1);
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
        public ContentControl GetCCForPINCITE(Word.Selection sel)
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
        public void RemovePinCite(Word.Selection selection)
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
