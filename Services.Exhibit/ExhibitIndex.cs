using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Exhibit
{
    public class ExhibitIndex
    {
        public ExhibitIndex(Application _app)
        {
            repository = new ExhibitRepository(_app);
            this._app = _app;
        }

        public ExhibitRepository repository;
        public Application _app;


        public void InsertExhibitIndex()
        {
            ExhibitHelper helper = new ExhibitHelper(_app);

            List<ContentControl> exhibits = helper.GetAllExhibitsFromDoc();
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
            var Numbering = repository.GetFormatting(FormatNodes.IndexStyle);
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
                    Index = tags.Count - 1;

                    _app.Selection.TypeText(ExhibitFormatter.ApplyNumFormat(Index, Numbering));
                    _app.Selection.MoveRight(WdUnits.wdCell);
                    _app.Selection.TypeText(Description);

                    if (exhibitCount > 0)
                        _app.Selection.MoveRight(WdUnits.wdCell);

                }
            }

        }

    }
}
