using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Tools.Simple
{
    public class Replace
    {
        // Open the Find and Replace All box

        public Replace(Word.Application _app)
        {
            this._app = _app;
        }

        Word.Application _app;

        public void SendKey()
        {
            SendKeys.Send("^h");

            OpenForm();

        }

        public void OpenForm()
        {
            //var a = _app.ActiveDocument.CommandBars.Count;
            //var b = _app.ActiveDocument.CommandBars.FindControls(Type: Microsoft.Office.Core.MsoControlType.msoControlButton, Visible: true).Count;
            var c = _app.ActiveDocument.CommandBars.FindControls(Type: Microsoft.Office.Core.MsoControlType.msoControlButton, Visible: true, Id: 120);

            var d = _app.ActiveDocument.CommandBars.FindControl(Type: Microsoft.Office.Core.MsoControlType.msoControlButton, Visible: true, Id: 120);


            //var fill = 0;

            //HomeTab > EditReplace
            //Home > Editing > Replace
        }

        /// <summary>
        /// needs work. do not use
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<Range> RegexFind(string regex, Range range)
        {
            List<Range> result = new List<Range>();

            Regex r = new Regex(regex);
            MatchCollection Matches = r.Matches(range.Text);

            for (int i = 0; i <= Matches.Count; i++)
            {
                Range newRange = _app.Selection.Range;

                newRange.SetRange(range.Start + Matches[i].Index, range.Start + Matches[i].Index + Matches[i].Length);
                result.Add(newRange);
            }

            return result;
        }

        
    }
}
