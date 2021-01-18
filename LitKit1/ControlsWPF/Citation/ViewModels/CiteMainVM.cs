using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using Tools.Citation;

namespace LitKit1.ControlsWPF.Citation.ViewModels
{
    public class CiteMainVM : INotifyPropertyChanged
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Private properties
        private CitationRepository _repository;
        private CiteDocLayer _docLayer;

        private Tools.Citation.Citation _selectedCite;
        private ObservableCollection<Tools.Citation.Citation> _citations;

        private ObservableCollection<CiteFormatPiece> _formatList_Long;
        private ObservableCollection<CiteFormatPiece> _formatList_Short;

        private EditCiteVM _editCiteVM = new EditCiteVM(new Tools.Citation.Citation(CiteType.Exhibit, "FillCite"), false);

        private bool _freeTextBeingEdited_Long = false;
        private bool _freeTextBeingEdited_Short = false;

        private CiteFormatPiece _freeTextFormatPiece_Long;
        private CiteFormatPiece _freeTextFormatPiece_Short;

        private bool _citesReloadAutomatically = true;

        private Visibility _citeAddVisibility = Visibility.Collapsed;

        #endregion

        #region Public properties
        public Microsoft.Office.Interop.Word.Application _app;

        public CitationRepository Repository
        {
            get { return _repository; }
            set 
            { 
                _repository = value;
                OnPropertyChanged("Repository");
            }
        }

        public Tools.Citation.Citation SelectedCite
        {
            get { return _selectedCite; }
            set
            {
                _selectedCite = value;
                OnPropertyChanged("SelectedCite");
            }
        }

        public ObservableCollection<Tools.Citation.Citation> Citations
        {
            get { return _citations; }
            private set
            {
                _citations = value;
            }
        }

        public ObservableCollection<CiteFormatPiece> FormatList_Long
        {
            get { return _formatList_Long; }
            set
            {
                _formatList_Long = value;
            }
        }
        public ObservableCollection<CiteFormatPiece> FormatList_Short
        {
            get { return _formatList_Short; }
            set
            {
                _formatList_Short = value;
            }
        }

        public EditCiteVM EditCiteVM
        {
            get { return _editCiteVM; }
            set
            {
                _editCiteVM = value;
                OnPropertyChanged("EditCiteVM");
            }
        }

        public bool FreeTextBeingEdited_Long
        {
            get { return _freeTextBeingEdited_Long; }
            set
            {
                _freeTextBeingEdited_Long = value;
                OnPropertyChanged("FreeTextBeingEdited_Long");
            }
        }
        public bool FreeTextBeingEdited_Short
        {
            get { return _freeTextBeingEdited_Short; }
            set
            {
                _freeTextBeingEdited_Short = value;
                OnPropertyChanged("FreeTextBeingEdited_Short");
            }
        }

        public CiteFormatPiece FreeTextFormatPiece_Long
        {
            get { return _freeTextFormatPiece_Long; }
            set
            {
                _freeTextFormatPiece_Long = value;
                OnPropertyChanged("FreeTextFormatPiece_Long");
            }
        }
        public CiteFormatPiece FreeTextFormatPiece_Short
        {
            get { return _freeTextFormatPiece_Short; }
            set
            {
                _freeTextFormatPiece_Short = value;
                OnPropertyChanged("FreeTextFormatPiece_Short");
            }
        }

        public bool CitesReloadAutomatically
        {
            get { return _citesReloadAutomatically; }
            set
            {
                _citesReloadAutomatically = value;
                OnPropertyChanged("CitesReloadAutomatically");
            }
        }

        public Visibility CiteAddVisibility
        {
            get { return _citeAddVisibility; }
            set
            {
                _citeAddVisibility = value;
                OnPropertyChanged("CiteAddVisibility");
            }
        }
        #endregion

        public CiteMainVM()
        {
            log4net.Config.XmlConfigurator.Configure();

            _app = Globals.ThisAddIn.Application;

            _repository = new CitationRepository(_app);
            _docLayer = new CiteDocLayer(_app);
            Citations = _repository.Citations;

            LoadFormatLists();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        #region Data Transformation

        private void LoadFormatLists()
        {
            FormatList_Long = Repository.CiteFormatting.ExhibitLongFormat;
            FormatList_Short = Repository.CiteFormatting.ExhibitShortFormat;

            var introLong = FormatList_Long.FirstOrDefault(n => n.Type == CiteFormatPieceType.INTROLONG);
            if (introLong != null)
            {
                introLong._displayText = Repository.CiteFormatting.ExhibitIntroLong;
            }
            var introShort = FormatList_Short.FirstOrDefault(n => n.Type == CiteFormatPieceType.INTROSHORT);
            if (introShort != null)
            {
                introShort._displayText = Repository.CiteFormatting.ExhibitIntroShort;
            }

            var indexLong = FormatList_Long.FirstOrDefault(n => n.Type == CiteFormatPieceType.INDEX);
            var indexShort = FormatList_Short.FirstOrDefault(n => n.Type == CiteFormatPieceType.INDEX);

            switch (Repository.CiteFormatting.ExhibitIndexStyle)
            {
                case ExhibitIndexStyle.Numbers:
                    if (indexLong != null) { indexLong._displayText = "#"; }
                    if (indexShort != null) { indexShort._displayText = "#"; }
                    break;
                case ExhibitIndexStyle.Letters:
                    if (indexLong != null) { indexLong._displayText = "A"; }
                    if (indexShort != null) { indexShort._displayText = "A"; }
                    break;
                case ExhibitIndexStyle.Roman:
                    if (indexLong != null) { indexLong._displayText = "IV"; }
                    if (indexShort != null) { indexShort._displayText = "IV"; }
                    break;

            }

        }
        //public void LoadCitationsFromRepo()
        //{
        //    ObservableCollection<Tools.Citation.Citation> observableCites = new ObservableCollection<Tools.Citation.Citation>();
        //    foreach (Tools.Citation.Citation cite in _repository.Citations)
        //    {
        //        observableCites.Add(cite);
        //    }
        //    Citations = observableCites;
        //}

        public void LoadFormattingFromRepo()
        {


            var indexStart = Repository.CiteFormatting.ExhibitIndexStart;
            var hasId = Repository.CiteFormatting.hasIdCite;

        }

        public void SetFormattinginRepo()
        {

        }

        #endregion


        public void InsertCite(Tools.Citation.Citation citation)
        {
            _app.UndoRecord.StartCustomRecord("Insert Citation");

            _docLayer.InsertCiteAtSelection(citation, Repository, CitesReloadAutomatically);

            if (CitesReloadAutomatically)
            {
                _docLayer.UpdateCitesInDoc(Repository);
                _docLayer.UpdateCiteInsertCountandExample(Repository);
            }

            var addin = (ThisAddIn)_app.Parent;
            addin.ReturnFocus();

            _app.UndoRecord.EndCustomRecord();
        }

        public void OpenEditCite(Tools.Citation.Citation citation)
        {
            EditCiteVM = new EditCiteVM(citation, true);


        }
        public void EditCite(Tools.Citation.Citation oldcite, Tools.Citation.Citation newcite)
        {

            Repository.UpdateCitation(oldcite, newcite);
            OnPropertyChanged("Citations");

            if (CitesReloadAutomatically)
            {
                _docLayer.UpdateCitesInDoc(Repository);
            }
        }

        public void DeleteCite(Tools.Citation.Citation citation)
        {
            var mb = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this citation from the document?", "Confirm", System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (mb == System.Windows.Forms.DialogResult.OK)
            {
                Citations.Remove(citation);
                _repository.DeleteCitation(citation);
                _docLayer.RemoveCiteCCs(citation, false);
                if (CitesReloadAutomatically)
                {
                    _docLayer.UpdateCitesInDoc(Repository);
                }
            }
        }

        internal void AddNewCite(Tools.Citation.Citation cite)
        {
            Repository.AddCitation(cite); 

        }

        internal void RefreshCites()
        {
            Cursor.Current = Cursors.WaitCursor;
            _app.UndoRecord.StartCustomRecord("Reload Citations");

            // do not check if CitesReloadedAutomatically is checked, force a refresh
                _docLayer.UpdateCitesInDoc(Repository);
                _docLayer.UpdateCiteInsertCountandExample(Repository);
            
            // // Throws an error when Cite Format is updated
            //var addin = (ThisAddIn)_app.Parent;
            //addin.ReturnFocus();

            _app.UndoRecord.EndCustomRecord();
            Cursor.Current = Cursors.Default;

        }

        internal void ResetFormatList()
        {
            FormatList_Long.Clear();
            FormatList_Short.Clear();

            FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.INTROLONG));
            FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.INDEX));
            FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.COMMA));
            FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.PIN));
            FormatList_Long.Add(new CiteFormatPiece(CiteFormatPieceType.DESC));

            FormatList_Short.Add(new CiteFormatPiece(CiteFormatPieceType.INTROSHORT));
            FormatList_Short.Add(new CiteFormatPiece(CiteFormatPieceType.INDEX));
            FormatList_Short.Add(new CiteFormatPiece(CiteFormatPieceType.PIN));

            OnPropertyChanged("FormatList_Long");
            OnPropertyChanged("FormatList_Short");
        }

        public void ChooseFreeTextEditBlock(CiteFormatPiece formatPiece)
        {

            if (FormatList_Long.Contains(formatPiece))
            {
                FreeTextFormatPiece_Long = formatPiece;
                FreeTextBeingEdited_Long = true;
            } else if (FormatList_Short.Contains(formatPiece))
            {
                FreeTextFormatPiece_Short = formatPiece;
                FreeTextBeingEdited_Short = true;
            }

        }

        internal void AddExhibitIndex()
        {
            _app.UndoRecord.StartCustomRecord("Insert Exhibit Index");
            _docLayer.InsertExhibitIndex();
            _app.UndoRecord.EndCustomRecord();
        }

        internal void BatchAddCites()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "XML | *.xml";
            openFileDialog.Title = "Import Citations and Cite Formatting";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog.FileName);

                int formatSuccess;
                int formatFail;
                int citeSuccess;
                int citeFail;
                int citeRepeated;


                batchFormatting(doc, out formatSuccess, out formatFail);
                batchCites(doc, out citeSuccess, out citeFail, out citeRepeated);


                System.Windows.Forms.MessageBox.Show(
                "Format Nodes Loaded: " + formatSuccess.ToString() + Environment.NewLine +
                "Format Nodes Failed: " + formatFail.ToString() + Environment.NewLine +
                Environment.NewLine +
                "Citations Added: " + citeSuccess.ToString() + Environment.NewLine +
                "Citations Failed: " + citeFail.ToString() + Environment.NewLine +
                "Redundant Citations Skipped: " + citeRepeated.ToString()
                );
            }
            catch { throw new FileNotFoundException(); }
        }

        private void batchFormatting(XmlDocument doc, out int formatSuccess, out int formatFail)
        {

            formatSuccess = 0;
            formatFail = 0;

            var formatNode = doc.SelectSingleNode("//Format");
            if (formatNode != null)
            {
                try
                {
                    string introLong = formatNode.SelectSingleNode("//IntroLong").InnerText;
                    string introShort = formatNode.SelectSingleNode("//IntroShort").InnerText;

                    ExhibitIndexStyle indexStyle = ExhibitIndexStyle.Numbers;
                    Enum.TryParse(formatNode.SelectSingleNode("//IndexStyle").InnerText, out indexStyle);
                    int indexStart = Int32.Parse(formatNode.SelectSingleNode("//IndexStart").InnerText);
                    bool idCite = bool.Parse(formatNode.SelectSingleNode("//IdCite").InnerText);

                    ObservableCollection<CiteFormatPiece> longFormat = new ObservableCollection<CiteFormatPiece>();
                    var longnodes = formatNode.SelectSingleNode("//Long").ChildNodes;
                    for (int i = 0; i < longnodes.Count; i++)
                    {
                        CiteFormatPieceType type = CiteFormatPieceType.FREETEXT;
                        Enum.TryParse(longnodes[i].Name, out type);

                        longFormat.Add(new CiteFormatPiece(type, longnodes[i].InnerText));
                    }

                    ObservableCollection<CiteFormatPiece> shortFormat = new ObservableCollection<CiteFormatPiece>();
                    var shortnodes = formatNode.SelectSingleNode("//Short").ChildNodes;
                    for (int i = 0; i < shortnodes.Count; i++)
                    {
                        CiteFormatPieceType type = CiteFormatPieceType.FREETEXT;
                        Enum.TryParse(shortnodes[i].Name, out type);

                        shortFormat.Add(new CiteFormatPiece(type, shortnodes[i].InnerText));
                    }

                    //Update the Cite Formatting and save 
                    CiteFormatting formatting = new CiteFormatting(introLong, introShort, longFormat, shortFormat, indexStyle, indexStart, idCite);
                    FormatList_Long = longFormat;
                    OnPropertyChanged("FormatList_Long");

                    FormatList_Short = shortFormat;
                    OnPropertyChanged("FormatList_Short");

                    Repository.CiteFormatting = formatting;
                    Repository.UpdateCiteFormattingInDB(formatting);

                    formatSuccess++;
                }
                catch
                {
                    formatFail++;
                }
            }
            
        }

        private void batchCites(XmlDocument doc, out int citeSuccess, out int citeFail, out int citeRepeated)
        {
            citeSuccess = 0;
            citeFail = 0;
            citeRepeated = 0;

            var citesNode = doc.SelectNodes("//Citation");
            for (int i = 0; i<citesNode.Count; i++)
            {
                var children = citesNode[i].ChildNodes;
                try 
                {
                    string ID = children[0].InnerText;
                    string RefName = children[1].InnerText;

                    CiteType Type = CiteType.Exhibit;
                    Enum.TryParse(children[2].InnerText, out Type);

                    string Long = children[3].InnerText;
                    string Short = children[4].InnerText;
                    string OtherID = children[5].InnerText;

                    Tools.Citation.Citation cite = new Tools.Citation.Citation(ID, Type, Long, Short, OtherID, RefName);

                    if (Citations.Where(n => n.ID == cite.ID).Count() == 0)
                    {
                        AddNewCite(cite); 
                        citeSuccess++;
                    }
                    else citeRepeated++;
                }
                catch { citeFail++; }
            };

        }

        internal void ExportCites()
        {
            string Path = "";
            bool FileAvailable = false;

            //Save File Dialog
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "XML|*.xml";
            saveFileDialog.Title = "Export Citations and Cite Formatting";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Path = saveFileDialog.FileName;
            }
            else
            { Path = null; }

            //Check if file is available
            FileInfo file = new FileInfo(Path);
            if (!file.Exists)
            { FileAvailable = true; }
            else
            {
                try
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                        FileAvailable = true;
                    }
                }
                catch (IOException)
                {
                    //the file is unavailable because it is:
                    //still being written to
                    //or being processed by another thread
                    //or does not exist (has already been processed)
                    FileAvailable = false;
                    System.Windows.Forms.MessageBox.Show("File is open in another window or program. Please close the file and try again.");

                }
            }

            if (FileAvailable)
            {
                Repository.ExportCites(Path);

            }
        }

        internal void UpdateFormatting(int indexStart)
        {
            var introLong = FormatList_Long.FirstOrDefault(n => n.Type == CiteFormatPieceType.INTROLONG);
            if (introLong != null)
            { Repository.CiteFormatting.ExhibitIntroLong = introLong.DisplayText; }

            var introShort = FormatList_Short.FirstOrDefault(n => n.Type == CiteFormatPieceType.INTROSHORT);
            if (introShort != null)
            { Repository.CiteFormatting.ExhibitIntroShort = introShort.DisplayText; }


            var index = FormatList_Long.FirstOrDefault(n => n.Type == CiteFormatPieceType.INDEX);

            if (index == null)
            { index = FormatList_Short.FirstOrDefault(n => n.Type == CiteFormatPieceType.INDEX); }

            if (index != null)
            {
                switch (index.DisplayText)
                {
                    case "#":
                        Repository.CiteFormatting.ExhibitIndexStyle = ExhibitIndexStyle.Numbers;
                        break;
                    case "A":
                        Repository.CiteFormatting.ExhibitIndexStyle = ExhibitIndexStyle.Letters;
                        break;
                    case "IV":
                        Repository.CiteFormatting.ExhibitIndexStyle = ExhibitIndexStyle.Roman;
                        break;
                }
            }

            Repository.CiteFormatting.ExhibitIndexStart = indexStart;
            Repository.UpdateCiteFormattingInDB(Repository.CiteFormatting);

            if (CitesReloadAutomatically)
            {
                RefreshCites();
            }
        }
    }
}
