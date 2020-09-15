using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Word = Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Tools.RedactionTool
{
    public enum RedactionState
    {
        Marked,
        UnMarked,
        Applied,
        Visibile
    };

    public enum RedactionType
    {
        Text,

        ImageInLine,
        ChartInLine,
        DiagramInLine,

        ImageFloating,
        ChartFloating,
        DiagramFloating,
    }

    public interface IRedaction
    {
       
        RedactionState State { get; set; }
        RedactionType Type { get; set; }

        Application _app { get; set; }
        Word.ContentControl ContentControl { get; set; }
    }
}
