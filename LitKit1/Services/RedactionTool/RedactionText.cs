using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Services.RedactionTool
{
    class RedactionText : IRedaction
    {
        public RedactionState State { get; set; }
        public RedactionType Type { get; set; }
        public Application _app { get; set; }
        public ContentControl ContentControl { get; set; }

        public RedactionText(Application _app)
        {
            this._app = _app;
            Type = RedactionType.Text;
            State = RedactionState.Marked;
        }
    }
}

