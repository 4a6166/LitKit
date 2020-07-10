using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using Services.Exhibit;

namespace LitKit1
{
    public class TestClass
    {
        public TestClass()
        {

        }

        static readonly Word.Application _app = Globals.ThisAddIn.Application;
        public IExhibitRepository repository = ExhibitRepositoryFactory.GetRepository("XML", _app);
        public ExhibitHelper helper = new ExhibitHelper();

        public void FootNoteFinder(Selection selection)
        {
            /// Gets the location of the FootNote reference
            string firstStart = selection.Start.ToString();
            selection.Find.Execute("^f");        ////////////////////////// ^f is footnotes and ^e is endnotes 
            string findStart = selection.Start.ToString();
            string appSelection =_app.Selection.Start.ToString();
            MessageBox.Show("First: "+firstStart +"\n" + "FindStart: "+findStart +"\n" +"_app.Selection: "+appSelection);

            string mbstring = "Selection Footnotes";
            foreach (Footnote footnote in selection.Footnotes)
            {
                mbstring = mbstring +"\n"+footnote.Reference.Start + "-" + footnote.Reference.End +" | StartingNumber: ";
            }
            MessageBox.Show(mbstring);

            ///////Shows all the FootNotes regardless of selection
            //selection = _app.Selection;
            //string mbstring = string.Empty;
            //foreach (Footnote footnote in selection.Footnotes)
            //{
            //    mbstring = mbstring + "\n"+footnote.Range.Start.ToString();
            //}
            //MessageBox.Show(mbstring);
        }

    }
}
