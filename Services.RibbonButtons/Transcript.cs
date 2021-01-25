using System;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace Tools.Simple
{
    public class Transcript
    {
        public Transcript(Word.Application _app)
        {
            this._app = _app;
        }

        Word.Application _app;
        /// <summary>
        ///Remove numbering, Remove all line breaks, Add Quotation marks before and after text, Keep spacing as per the current document format
        /// </summary>
        /// <param name="Quote"></param>
        public void PasteAsInText(string Quote)
        {
            //TODO: when a quotation mark is at the end of the inserted text, the result is two quotation marks. When it is at the beginning, the result is one.
            string quote = RemoveNumberingAndLineBreaks(Quote, InLineOrBlock.InLine);

            var rngStart = _app.Selection.Start;

            quote = quote.Trim();
            quote = "\"" + quote + "\"";
            _app.Selection.TypeText(quote);

            var rngEnd = _app.Selection.End;

            _app.Selection.SetRange(rngStart, rngEnd);
            var rng = _app.Selection.Range;
            rng.Find.Execute(FindText: "\"", ReplaceWith: "\"", Replace: Word.WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "\'", ReplaceWith: "\'", Replace: Word.WdReplace.wdReplaceAll);

            _app.Selection.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
        }

        /// <summary>
        /// //Remove all line breaks except if a line starts with “Q.” or “A.” or “Mr.” or “Mrs.” or “Ms.” or “Dr.” or “Court Reporter”, do not remove preceding line break and add a space before that line, Single space, Add  1 inch indent on each side
        /// </summary>
        /// <param name="Quote"></param>
        public void PasteAsBlockQuote(string Quote, float indentInches = 1)
        {
            //TODO: when the first char is a quotation mark, it disappears from text to be inserted

            string quote = RemoveNumberingAndLineBreaks(Quote, InLineOrBlock.Block);

            int RangeStart = _app.Selection.Start ;
            _app.Selection.TypeText( quote + Environment.NewLine);

            _app.Selection.SetRange(RangeStart, _app.Selection.Start + quote.Length-1);

            var rng = _app.Selection.Range;
            rng.Find.Execute(FindText: "\"", ReplaceWith: "\"", Replace: Word.WdReplace.wdReplaceAll);
            rng.Find.Execute(FindText: "\'", ReplaceWith: "\'", Replace: Word.WdReplace.wdReplaceAll);

            _app.Selection.Paragraphs.Format.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            
            _app.Selection.Paragraphs.Format.LeftIndent = _app.InchesToPoints(indentInches);
            _app.Selection.Paragraphs.Format.RightIndent = _app.InchesToPoints(indentInches);

            _app.Selection.SetRange(_app.Selection.End + 1, _app.Selection.End + 1);
            _app.Selection.Paragraphs.Format.LeftIndent = 0;
            _app.Selection.Paragraphs.Format.RightIndent = 0;

            
        }



        /// <summary>
        /// If InLine removes all line breaks, if Block removes certain line breaks. Also removes any numbers at the beginning of the line and spaces after it.
        /// </summary>
        /// <param name="Quote"></param>
        /// <param name="inLineOrBlock"></param>
        /// <returns></returns>
        public string RemoveNumberingAndLineBreaks(string Quote, InLineOrBlock inLineOrBlock)
        {
            char[] delims = new[] { '\r', '\n' };
            string[] lines = Quote.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            string quote = string.Empty;
            switch (inLineOrBlock)
            {
                case InLineOrBlock.InLine:
                    //Remove all line breaks
                    foreach (string line in lines)
                    {
                        string strng1 = RemoveNumbers(line);
                        quote += strng1+" ";
                    }
                    break;
                case InLineOrBlock.Block:
                    //Remove all line breaks except if a line starts with “Q.” or “A.” or “Mr.” or “Mrs.” or “Ms.” or “Dr.” or “Court Reporter”
                    foreach (string line in lines)
                    {
                        string strng1 = RemoveNumbers(line);

                        if (strng1.StartsWith("Q ") ||
                            strng1.StartsWith("A ") ||

                            strng1.StartsWith("Q.") ||
                            strng1.StartsWith("A.") ||
                            strng1.StartsWith("Mr.") ||
                            strng1.StartsWith("Mrs.") ||
                            strng1.StartsWith("Ms.") ||
                            strng1.StartsWith("Dr.") ||
                            strng1.StartsWith("Court Reporter.") ||

                            strng1.StartsWith("MR.") ||
                            strng1.StartsWith("MRS.") ||
                            strng1.StartsWith("MS.") ||
                            strng1.StartsWith("DR.") ||
                            strng1.StartsWith("COURT REPORTER.") ||

                            strng1.StartsWith("Q:") ||
                            strng1.StartsWith("A:") ||
                            strng1.StartsWith("Mr:") ||
                            strng1.StartsWith("Mrs:") ||
                            strng1.StartsWith("Ms:") ||
                            strng1.StartsWith("Dr:") ||
                            strng1.StartsWith("Court Reporter:") ||

                            strng1.StartsWith("MR:") ||
                            strng1.StartsWith("MRS:") ||
                            strng1.StartsWith("MS:") ||
                            strng1.StartsWith("DR:") ||
                            strng1.StartsWith("COURT REPORTER:")

                            )
                        {
                            quote += (Environment.NewLine + strng1 + " ");
                        }
                        else quote += (strng1 + " ");
                    }
                    break;
                default:
                    throw new Exception("Issue with passed InLineOrBlock");
            }

            return quote;
        }

        public string RemoveNumbers(string Quote)
        {
            try
            {
                var chars = Quote.ToCharArray().ToList();
                var firstLetter = chars.Where(c => Char.IsLetter(c)).FirstOrDefault();

                int indexFirstLetter = chars.IndexOf(firstLetter);

                string quote = string.Empty;
                for (int i = indexFirstLetter; i <= chars.Count - 1; i++)
                {
                    quote += chars[i];
                }
                return quote;
            }
            catch
            {
                var chars = Quote.ToCharArray().ToList();
                var firstLetter = chars.Where(c => Char.IsWhiteSpace(c) || Char.IsLetter(c)).FirstOrDefault();

                int indexFirstLetter = chars.IndexOf(firstLetter);

                string quote = string.Empty;
                for (int i = indexFirstLetter; i <= chars.Count - 1; i++)
                {
                    quote += chars[i];
                }
                return quote;
            }
        }
    }
    
    public enum InLineOrBlock
    {
        InLine,
        Block,
    }

}
