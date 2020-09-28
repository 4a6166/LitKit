using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Exhibit;
using LitKit1.Controls.ExhibitControls;
using Word = Microsoft.Office.Interop.Word;
using MTools = Microsoft.Office.Tools;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;

namespace LitKit1.Controls
{
    public partial class ctrlExhibitView : UserControl
    {
        public ctrlExhibitView()
        {
            InitializeComponent();
            _app = Globals.ThisAddIn.Application;
            helper = new ExhibitHelper(_app);
            repository = new ExhibitRepository(_app);

            ErrorLabel.Visible = false;
            ErrorLabel.Text = string.Empty;
            LoadFormatting();
            LoadExhibitListView();
            LoadLRListView();
            
        }

        readonly Microsoft.Office.Interop.Word.Application _app;
        readonly ExhibitRepository repository;
        private IEnumerable<Exhibit> exhibits;
        private IEnumerable<LegalRecordCite> cites;

        private string FirstCite;
        private string FollowingCites;
        private NumberingOptions IndexStyle;
        private int IndexStart;
        private bool UniformCites;
        private bool idCite;
        private bool FormatCustomized;

        readonly ExhibitHelper helper;
        readonly EnumSwitch enumSwitch = new EnumSwitch();


        public void LoadExhibitListView()
        {
            listView1.Clear();
            listView1.FullRowSelect = true;
            listView1.View = System.Windows.Forms.View.Details;

            listView1.Columns.Add("Description", 200);
            listView1.Columns.Add("Bates", 75);
            listView1.Columns.Add("ID", 0);

            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;


            exhibits = repository.GetExhibits();

            foreach (Exhibit ex in exhibits)
            {
                CreateExhibitListViewItems(listView1, ex);
            }

            try
            {
                listView1.Items[0].Selected = true;
            }
            catch { }
        }

        public void LoadLRListView()
        {
            listView2.Clear();
            listView2.FullRowSelect = true;
            listView2.View = System.Windows.Forms.View.Details;

            listView2.Columns.Add("Citation", 300);
            listView2.Columns.Add("ShortCite", 0);
            listView2.Columns.Add("ID", 0);

            listView2.HeaderStyle = ColumnHeaderStyle.Nonclickable;


            cites = repository.GetLRCites();

            foreach (LegalRecordCite ex in cites)
            {
                CreateLRListViewItems(listView2, ex);
            }

            try
            {
                listView2.Items[0].Selected = true;
            }
            catch { }
        }


        private void CreateExhibitListViewItems(ListView lv, Exhibit exhibit)
        {
            string[] arr = new string[3];
            arr[0] = exhibit.Description;
            arr[1] = exhibit.BatesNumber;
            arr[2] = exhibit.ID;

            ListViewItem item = new ListViewItem(arr)
            {
                Tag = exhibit
            };
            lv.Items.Add(item);
        }

        private void CreateLRListViewItems(ListView lv, LegalRecordCite LRCite)
        {
            string[] arr = new string[3];
            arr[0] = LRCite.LongCite;
            arr[1] = LRCite.ShortCite;
            arr[2] = LRCite.ID;

            ListViewItem item = new ListViewItem(arr)
            {
                Tag = LRCite
            };
            lv.Items.Add(item);
        }

        private void LoadFormatting()
        {
            FirstCite = repository.GetFormatting(FormatNodes.FirstCite);
            FollowingCites = repository.GetFormatting(FormatNodes.FollowingCites);
            IndexStyle = enumSwitch.NumberingOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.IndexStyle));
            IndexStart = Int32.Parse(repository.GetFormatting(FormatNodes.IndexStart));
            UniformCites = bool.Parse(repository.GetFormatting(FormatNodes.UniformCites));
            idCite = bool.Parse(repository.GetFormatting(FormatNodes.IdCite));
            FormatCustomized = bool.Parse(repository.GetFormatting(FormatNodes.FormatCustomized));


        }

        public void AddPINCITE()
        {

            // Adds an additional cc covering PINCITE in orde to allow user to enter a pincite that is not kept with the exhibit information elsewhere
            /*string pinciteText = "{{PINCITE}}";

            _app.Selection.SetRange(exhibitStart.Range.End + 2, exhibitStart.Range.End + 2 + pinciteText.Length);
            var pincite = _app.Selection.ContentControls.Add(Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText);
            pincite.Tag = "PINCITE:" + exhibit.ID;
            pincite.SetPlaceholderText(null);
            pincite.Appearance = Microsoft.Office.Interop.Word.WdContentControlAppearance.wdContentControlBoundingBox;
            */

            //TODO: Pull PINCITE out and allow user to add it with a right click or other button
        }
        public void AddMention()
        {
            //TODO: add Mention Exhibit (only uses format "Exhibi #" and does not get included in exhibit count

        }

        public ContentControl CiteToExhibit(Exhibit exhibit)
        {
            try
            {
                ContentControl cc = _app.Selection.ContentControls.Add(Word.WdContentControlType.wdContentControlRichText);
                cc.Tag = "Exhibit:" + exhibit.ID;
                cc.Title = "Exhibit: " + exhibit.Description;

                int index = helper.GetPosition(cc.Tag);

                string CiteFormat = FirstCite; // TODO: update so it pulls FollowingCites if the exhibit has been inserted already

                cc.Range.Text = ExhibitFormatter.FormatCite(exhibit, CiteFormat, IndexStyle, IndexStart, index);

                Globals.ThisAddIn.ReturnFocus();
                return cc;
            }
            catch
            {
                frmToast toast = new frmToast(_app.ActiveWindow);
                toast.OpenToast("Something went wrong!", "Please clear your selection and try again.");
                return null;
            }
        }

        public ContentControl InsertLRCite(LegalRecordCite cite)
        {
            try
            {
                ContentControl cc = _app.Selection.ContentControls.Add(Word.WdContentControlType.wdContentControlRichText);
                cc.Tag = "Cite:" + cite.ID;
                cc.Title = "Cite: " + cite.LongCite;

                int index = helper.GetPosition(cc.Tag);

                // TODO: update so it pulls ShortCite (if selected) if the exhibit has been inserted already
                bool initialCite = true;

                if (initialCite == true)
                {
                    cc.Range.Text = cite.LongCite;
                }
                else cc.Range.Text = cite.ShortCite;

                Globals.ThisAddIn.ReturnFocus();
                return cc;
            }
            catch
            {
                frmToast toast = new frmToast(_app.ActiveWindow);
                toast.OpenToast("Something went wrong!", "Please clear your selection and try again.");
                return null;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                _app.UndoRecord.StartCustomRecord("Cite to Exhibit");

                ErrorLabel.Visible = false;
                Exhibit exhibit = (Exhibit)listView1.SelectedItems[0].Tag;
                ContentControl cc = CiteToExhibit(exhibit);
                helper.UpdateInsertedCites();

                _app.Selection.SetRange(cc.Range.End+1, cc.Range.End+1);

                _app.UndoRecord.EndCustomRecord();

                Globals.ThisAddIn.ReturnFocus();
            }
            else
            {
                ErrorLabel.Text = "You must select an Exhibit before inserting";
                ErrorLabel.Visible = true;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            //For determining the index of the controls in the parent control

            //string result = string.Empty;
            //foreach(Control control in this.Controls)
            //{
            //    result += control.Name + Environment.NewLine;
            //}
            //MessageBox.Show(result);
        }

        private void ExhibitCtrl_Load(object sender, EventArgs e)
        {

        }

        private void ExhibitFormatting_Click(object sender, EventArgs e)
        {
            UserControl exhibitCtrl;
            switch (FormatCustomized)
            {
                case true:
                    exhibitCtrl = new ctrlExhibitFormatCustom();
                    break;
                case false:
                    exhibitCtrl = new ctrlExhibitFormat();
                    break;
                default:
                    throw new Exception("Bool FormattingCustomized not reading as true or false");
            }

            MTools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];

            ActivePane.Control.Controls.Clear();
            //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

            ActivePane.Control.Controls.Add(exhibitCtrl);
            //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);

            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;

            //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
            ActivePane.Visible = true;
        }

        private void NewExhibit_Click(object sender, EventArgs e)
        {
            ctrlExhibitUpdateAdd exhibitCtrl = new ctrlExhibitUpdateAdd();
            MTools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            ActivePane.Control.Controls.Add(exhibitCtrl);



            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            exhibitCtrl.button1.Text = "Add to Exhibit List";
            exhibitCtrl.label3.Text = "New Exhibit Reference";

            exhibitCtrl.GrayExampleText();

            ActivePane.Visible = true;
        }

        private void EditExhibit_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) 
            {

                ctrlExhibitUpdateAdd exhibitCtrl = new ctrlExhibitUpdateAdd();

                MTools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];

                ActivePane.Control.Controls.Clear();
                //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

                ActivePane.Control.Controls.Add(exhibitCtrl);
                //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);



                exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
                exhibitCtrl.label3.Text = "Current Description: " +
                    listView1.SelectedItems[0].SubItems[0].Text;

                exhibitCtrl.ID = listView1.SelectedItems[0].SubItems[2].Text;
                exhibitCtrl.textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;

                if (string.IsNullOrWhiteSpace(listView1.SelectedItems[0].SubItems[1].Text))
                {
                    exhibitCtrl.textBox3.Text = "ABC0001234";
                }
                else
                {
                    exhibitCtrl.textBox3.Text = listView1.SelectedItems[0].SubItems[1].Text;
                }

                exhibitCtrl.GrayExampleText();

                ActivePane.Visible = true;
                //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
            }
            else
            {
                ErrorLabel.Text = "You must select an Exhibit before editing";
                ErrorLabel.Visible = true;
            }
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                _app.UndoRecord.StartCustomRecord("Exhibit Index");

                new ExhibitIndex(_app).InsertExhibitIndex();
                Globals.ThisAddIn.ReturnFocus();

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("Please select an editable range of text.");}
        }

        private void RefreshNumbering_Click(object sender, EventArgs e) 
        {
            _app.UndoRecord.StartCustomRecord("Refesh Exhibits");
            var curSelEnd = _app.Selection.End;

            helper.UpdateInsertedCites();
            //TODO: Refresh inserted LRCites as well

            _app.Selection.Start = curSelEnd + 1;
            Globals.ThisAddIn.ReturnFocus();

            _app.UndoRecord.EndCustomRecord();

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ReorderExhibitsList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Functionality Coming Soon");

            
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            
            
            if (listView1.SelectedItems.Count > 0)
            {
                ErrorLabel.Visible = false;
                DialogResult result = MessageBox.Show("Are you sure you want to remove this Exhibit from the Exhibit List? This will also remove the references to this Exhibit from the document and cannot be undone.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string id = listView1.SelectedItems[0].SubItems[2].Text;
                    repository.DeleteExhibit(id);

                    foreach (Word.ContentControl cc in _app.ActiveDocument.ContentControls)
                    {
                        if (cc.Tag == "Exhibit:"+id)
                        {
                            cc.Delete(true);
                        }
                    }

                    helper.UpdateInsertedCites();
                    LoadExhibitListView();
                }
                _app.ActiveDocument.UndoClear(); // prevents user from re-inserting a cc that no longer is able to reference an exhibit
            }
            else
            {
                ErrorLabel.Text = "You must select an Exhibit before deleting";
                ErrorLabel.Visible = true;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void toolTip2_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void toolTip3_Popup(object sender, PopupEventArgs e)
        {
            //toolTip3.SetToolTip(DeleteExhibit, "Delete the selected Exhibit from the Exhibit List.");
        }

        private void DeleteExhibit_MouseHover(object sender, EventArgs e)
        {
            toolTipDelete.Show("Delete the selected Exhibit from the Exhibit List.", DeleteExhibit);
        }

        private void EditExhibit_MouseHover(object sender, EventArgs e)
        {
            toolTipEdit.Show("Edit the selected Exhibit description and Bates number.", EditExhibit);
        }

        private void NewExhibit_MouseHover(object sender, EventArgs e)
        {
            toolTipAdd.Show("Add a new Exhibit to the list.", NewExhibit);
        }

        private void RefreshNumbering_MouseHover(object sender, EventArgs e)
        {
            toolTipRefresh.Show("Refreshes the formatting, numbering, and content of all the exhibits within the document text.", RefreshNumbering);
        }

        private void ExhibitFormatting_MouseHover(object sender, EventArgs e)
        {
            toolTipFormat.Show("Adjust the format in which the exhibits are presented in the document.", ExhibitFormatting);
        }

        private void toolTip1_Popup_1(object sender, PopupEventArgs e)
        {

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTipRemoveLocks.Show("Removes the controls binding all Exhibits in the current selection, leaving them as plain text. To remove the Exhibit controls from the entire document, select no text.", btnRemoveExhibitLocks);
        }

        private void btnRemoveExhibitLocks_Click(object sender, EventArgs e)
        {
            if (_app.Selection.Range.Characters.Count > 2)
            {
                _app.UndoRecord.StartCustomRecord("Remove Exhibits");
                helper.RemoveSelectedCitesFromDoc(_app.Selection);
                _app.UndoRecord.EndCustomRecord();
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to remove the references to all Exhibits in the document? The text will remain but will no longer update when adjustments to the Exhibit List are made.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    _app.UndoRecord.StartCustomRecord("Remove Exhibits");
                    helper.RemoveAllCitesFromDoc();
                    _app.UndoRecord.EndCustomRecord();
                }
        }
    }

        private void ClearReferencesToExhibit_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ErrorLabel.Visible = false;
                DialogResult result = MessageBox.Show("Are you sure you want to remove all references to this Exhibit from the document? The Exhibit will remain in the Exhibit List.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _app.UndoRecord.StartCustomRecord("Remove Exhibit References");

                    string id = listView1.SelectedItems[0].SubItems[2].Text;

                    foreach (Word.ContentControl cc in _app.ActiveDocument.ContentControls)
                    {
                        if (cc.Tag == "Exhibit:" + id)
                        {
                            cc.Delete(true);
                        }
                    }

                    helper.UpdateInsertedCites();

                    _app.UndoRecord.EndCustomRecord();
                }
            }
            else
            {
                ErrorLabel.Text = "You must select an Exhibit before deleting";
                ErrorLabel.Visible = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ErrorLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTipIndex.Show("Inserts a table index of Exhibits in the document at your current selection. This table will not be updated when Exhibits are moved, edited, or deleted.", btnCreateExhibitIndex);
        }

        private void ClearReferencesToExhibit_MouseHover(object sender, EventArgs e)
        {
            toolTipClearFromDoc.Show("Removes all references to the selected Exhibit from the document.", ClearReferencesToExhibit);
        }

        private void btnCiteToExhibit_MouseHover(object sender, EventArgs e)
        {
            toolTipCiteExhibit.Show("Cites to the selected Exhibit at the selection location in the document.", btnCiteToExhibit);
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    btnCiteToExhibit.PerformClick();
                }
            }
            catch { }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            ctrlLegalRecordUpdateAdd exhibitCtrl = new ctrlLegalRecordUpdateAdd();
            MTools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
            ActivePane.Control.Controls.Clear();
            ActivePane.Control.Controls.Add(exhibitCtrl);



            exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            exhibitCtrl.button1.Text = "Add to Citation List";
            exhibitCtrl.label3.Text = "New Citation Reference";

            exhibitCtrl.GrayExampleText();

            ActivePane.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                label1.Visible = false;
                DialogResult result = MessageBox.Show("Are you sure you want to remove all references to this citation from the document? The citation will remain in the citation List.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _app.UndoRecord.StartCustomRecord("Remove Citation References");

                    string id = listView2.SelectedItems[0].SubItems[2].Text;
                    MessageBox.Show("TODO");
                    //foreach (Word.ContentControl cc in _app.ActiveDocument.ContentControls)
                    //{
                    //    if (cc.Tag == "Exhibit:" + id)
                    //    {
                    //        cc.Delete(true);
                    //    }
                    //}

                    //helper.RefreshInsertedExhibits();

                    _app.UndoRecord.EndCustomRecord();
                }
            }
            else
            {
                ErrorLabel.Text = "You must select an Exhibit before deleting";
                ErrorLabel.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {

                ctrlLegalRecordUpdateAdd exhibitCtrl = new ctrlLegalRecordUpdateAdd();

                MTools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];

                ActivePane.Control.Controls.Clear();
                //Globals.ThisAddIn.ExhibitMain.Controls.Clear();

                ActivePane.Control.Controls.Add(exhibitCtrl);
                //Globals.ThisAddIn.ExhibitMain.Controls.Add(exhibitCtrl);



                exhibitCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
                exhibitCtrl.label3.Text = "Citation: "+
                    listView2.SelectedItems[0].SubItems[0].Text;

                exhibitCtrl.ID = listView2.SelectedItems[0].SubItems[2].Text;
                exhibitCtrl.textBox1.Text = listView2.SelectedItems[0].SubItems[0].Text;

                if (string.IsNullOrWhiteSpace(listView2.SelectedItems[0].SubItems[1].Text))
                {
                    exhibitCtrl.textBox3.Text = "New Citation";
                }
                else
                {
                    exhibitCtrl.textBox3.Text = listView2.SelectedItems[0].SubItems[1].Text;
                }

                exhibitCtrl.GrayExampleText();

                ActivePane.Visible = true;
                //Globals.ThisAddIn.ExhibitTaskPane.Visible = true;
            }
            else
            {
                label1.Text = "You must select an Exhibit before editing";
                label1.Visible = true;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                label1.Visible = false;
                DialogResult result = MessageBox.Show("Are you sure you want to remove this citation from the citation List? This will also remove the references to this citation from the document and cannot be undone.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string id = listView2.SelectedItems[0].SubItems[2].Text;
                    repository.DeleteLRCite(id);

                    MessageBox.Show("TODO");
                    //foreach (Word.ContentControl cc in _app.ActiveDocument.ContentControls)
                    //{
                    //    if (cc.Tag == "Exhibit:" + id)
                    //    {
                    //        cc.Delete(true);
                    //    }
                    //}

                    //helper.RefreshInsertedExhibits();
                    //LoadExhibitListView();
                }
                _app.ActiveDocument.UndoClear(); // prevents user from re-inserting a cc that no longer is able to reference an exhibit
            }
            else
            {
                label1.Text = "You must select a citation before deleting";
                label1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                _app.UndoRecord.StartCustomRecord("Insert Legal or Record Citation");

                label1.Visible = false;
                LegalRecordCite cite = (LegalRecordCite)listView2.SelectedItems[0].Tag;
                ContentControl cc = InsertLRCite(cite);
                helper.UpdateInsertedCites();

                _app.Selection.SetRange(cc.Range.End + 1, cc.Range.End + 1);

                _app.UndoRecord.EndCustomRecord();

                Globals.ThisAddIn.ReturnFocus();
            }
            else
            {
                label1.Text = "You must select an citation before inserting";
                label1.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefreshNumbering_Click(sender, e);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        private void NewCite_MouseHover(object sender, EventArgs e)
        {
            toolTipAdd.Show("Add a new Citation to the list.", NewCite);
        }

        private void EditCite_MouseHover(object sender, EventArgs e)
        {
            toolTipEdit.Show("Edit the long and short formats for the selected Citation.", EditCite);
        }

        private void ClearReferencesToCite_MouseHover(object sender, EventArgs e)
        {
            toolTipClearFromDoc.Show("Removes all references to the selected Exhibit from the document.", ClearReferencesToCite);
        }

        private void DeleteCite_MouseHover(object sender, EventArgs e)
        {
            toolTipDelete.Show("Delete the selected Citation from the Citation List.", DeleteCite);

        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolTipRefresh.Show("Refreshes the formatting, numbering, and content of all the exhibits within the document text.", button6);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //mirror of pincite button on ribbon

            try
            {
                _app.UndoRecord.StartCustomRecord("Add Pincite");

                new Pincite(_app).AddPincite(_app.Selection);
                Globals.ThisAddIn.ReturnFocus();

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #204"); }


        }

        private void btnRemovePincite_Click(object sender, EventArgs e)
        {
            //mirror of remove pincite button on ribbon
            try
            {
                _app.UndoRecord.StartCustomRecord("Remove Pincite");

                new Pincite(_app).RemovePinCite(_app.Selection);

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("An Error Occurred. Please contact Prelimine with this error code: #205"); }

        }
    }
    
}
