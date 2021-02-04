using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Tools.Simple
{
    public class Shortcuts
    {
        // Change selection to Small Caps
        public Shortcuts(Application _app)
        {
            this._app = _app;
        }

        Application _app;

        public bool ChangeSmallCaps(Selection selection, Microsoft.Office.Tools.Ribbon.RibbonToggleButton button)
        {
            //System.Windows.Forms.MessageBox.Show(selection.Font.SmallCaps.ToString());
            if (selection.Font.SmallCaps == 0)
            {
                button.Checked = true;
                selection.Font.SmallCaps = -1;
            }
            else
            {
                button.Checked = false;
                selection.Font.SmallCaps = 0;
            }

            return true;
        }

        public bool Exactly24(Selection selection)
        {
            selection.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
            selection.ParagraphFormat.LineSpacing = 24;

            return true;
        }

        public bool OrphanControl(Selection selection)
        {
            selection.ParagraphFormat.WidowControl = -1;

            return true;
        }


    }
}
