using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public static class FormatTextInDoc
    {
        public static void FormatFont(Range Range)
        {

            var find = Range.Find;
            {
                //Bold **&**
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\*\*(*)\*\*";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Bold = -1;
                find.MatchWildcards = true;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            {
                //Italics //&//
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\/\/(*)\/\/";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Italic = -1;
                find.MatchWildcards = true;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            {
                //Underline __&__
                find.ClearFormatting();
                find.Replacement.ClearFormatting();

                find.Text = @"\_\_(*)\_\_";
                find.Replacement.Text = @"\1";
                find.Replacement.Font.Underline = WdUnderline.wdUnderlineSingle;
                find.MatchWildcards = true;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }

        }

    }
}
