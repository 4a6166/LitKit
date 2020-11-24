using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //SendKeys.Send("^h");

            OpenForm();
        }

        public void OpenForm()
        {
            Stack<object> stack = new Stack<object>();

            var a = _app.ActiveDocument.CommandBars.Count;
            var b = _app.ActiveDocument.CommandBars.FindControls(Type: Microsoft.Office.Core.MsoControlType.msoControlButton, Visible: true).Count;
            var c = _app.ActiveDocument.CommandBars.FindControls(Type: Microsoft.Office.Core.MsoControlType.msoControlButton, Visible: true, Id: 120);



            var fill = 0;

            //HomeTab > EditReplace
            //Home > Editing > Replace
        }

        
    }
}
