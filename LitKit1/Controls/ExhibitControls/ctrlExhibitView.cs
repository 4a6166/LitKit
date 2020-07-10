using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Exhibit;
using LitKit1.Controls.ExhibitControls;
using Word = Microsoft.Office.Interop.Word;
using Tools = Microsoft.Office.Tools;
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
            repository = ExhibitRepositoryFactory.GetRepository("XML", _app);

            ErrorLabel.Visible = false;
            ErrorLabel.Text = string.Empty;
            LoadFormatting();
            
        }

        readonly Microsoft.Office.Interop.Word.Application _app;
        readonly IExhibitRepository repository;
        private IEnumerable<Exhibit> exhibits;

        private IntroOptions intro;
        private NumberingOptions numbering;
        private FirstOnlyOptions firstOnly;
        private DescBatesFormatOptions descBatesFormat;
        private string parentheses; //"True" or "False"
        private string idCite; //"True" or "False"
        readonly ExhibitHelper helper = new ExhibitHelper();
        readonly EnumSwitch enumSwitch = new EnumSwitch();


        public void LoadListView()
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
                CreateListViewItems(listView1, ex);
            }
        }

        private void CreateListViewItems(ListView lv, Exhibit exhibit)
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

        private void LoadFormatting()
        {
            intro = enumSwitch.IntroOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.Intro));
            numbering = enumSwitch.NumberingOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.Numbering));
            firstOnly = enumSwitch.FirstOnlyOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.FirstOnly));
            descBatesFormat = enumSwitch.DescBatesFormatOptions_TextSwitchEnum(repository.GetFormatting(FormatNodes.DescBatesFormat));
            parentheses = repository.GetFormatting(FormatNodes.Parentheses);
            idCite = repository.GetFormatting(FormatNodes.IdCite);

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

                int index = helper.GetPosition(cc.Tag, _app);
                helper.FormatFirstCite(exhibit, index, _app);

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
                helper.RefreshInsertedExhibits(_app);

                _app.Selection.SetRange(cc.Range.End+1, cc.Range.End+1);

                _app.UndoRecord.EndCustomRecord();

                Globals.ThisAddIn.ReturnFocus();
            }
            else
            {
                ErrorLabel.Text = "You must select an Exhibit before editing";
                ErrorLabel.Visible = true;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ExhibitCtrl_Load(object sender, EventArgs e)
        {

        }

        private void ExhibitFormatting_Click(object sender, EventArgs e)
        {
            ctrlExhibitFormat exhibitCtrl = new ctrlExhibitFormat();
            Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];

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
            Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];
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

                Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow];

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

                helper.InsertExhibitIndex(_app);
                Globals.ThisAddIn.ReturnFocus();

                _app.UndoRecord.EndCustomRecord();
            }
            catch { MessageBox.Show("Please select an editable range of text.");}
        }

        private void RefreshNumbering_Click(object sender, EventArgs e) 
        {
            _app.UndoRecord.StartCustomRecord("Refesh Exhibits");
            var curSelEnd = _app.Selection.End;

            helper.RefreshInsertedExhibits(_app);

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

                    helper.RefreshInsertedExhibits(_app);
                    LoadListView();
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
                helper.RemoveExhibitsFromDoc(_app.Selection);
                _app.UndoRecord.EndCustomRecord();
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to remove the references to all Exhibits in the document? The text will remain but will no longer update when adjustments to the Exhibit List are made.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    _app.UndoRecord.StartCustomRecord("Remove Exhibits");
                    helper.RemoveExhibitsFromDoc(_app);
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

                    helper.RefreshInsertedExhibits(_app);

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
    }
    
}
