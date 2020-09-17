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
            SendKeys.Send("^h");


        }

        
    }
}
