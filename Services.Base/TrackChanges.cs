using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms = System.Windows.Forms;

namespace Services.Base
{
    public class TrackChanges
    {
        Dictionary<ContentControl, bool> LockState = new Dictionary<ContentControl, bool>();

        public bool AcceptTrackChanges(Document doc, string message = "This action requires that track changes be off. Do you want to accept any currently tracked changes now?")
        {
            Forms.DialogResult mb = Forms.DialogResult.Yes;
            if (/*_app.ActiveDocument.TrackRevisions == true &&*/ doc.Revisions.Count > 0)
            {
                mb = Forms.MessageBox.Show(message, "Accept Tracked Changes", Forms.MessageBoxButtons.YesNo);
            }
            if (mb == Forms.DialogResult.Yes)
            {

                doc.Select();
                try
                {
                    foreach (ContentControl cc in doc.ContentControls)
                    {
                        LockState.Add(cc, cc.LockContents);
                        cc.LockContents = false;
                    }

                    doc.AcceptAllRevisions();
                }
                catch (Exception e)
                {
                }
                doc.TrackRevisions = false;


                return true;
            }
            else return false;

        }

        public void RelockCCs()
        {
            foreach (var pair in LockState)
            {
                pair.Key.LockContents = pair.Value;
            }

        }
    }
}
