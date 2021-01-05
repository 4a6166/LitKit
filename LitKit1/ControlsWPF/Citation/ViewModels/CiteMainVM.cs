using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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


        #endregion

        public CiteMainVM()
        {
            _app = Globals.ThisAddIn.Application;

            _repository = new CitationRepository(_app);
            _docLayer = new CiteDocLayer(_app);
            Citations = _repository.Citations;

            FormatList_Long = Repository.CiteFormatting.ExhibitLongFormat;
            FormatList_Short = Repository.CiteFormatting.ExhibitShortFormat;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        #region Data Transformation

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
            System.Windows.Forms.MessageBox.Show("Selected Citation: " + citation.ID);

            //_docLayer.InsertCiteAtSelection(citation);
            //_docLayer.RefreshDocCites();
        }

        public void OpenEditCite(Tools.Citation.Citation citation)
        {
            EditCiteVM = new EditCiteVM(citation, true);


        }
        public void EditCite(Tools.Citation.Citation oldcite, Tools.Citation.Citation newcite)
        {

            Repository.UpdateCitation(oldcite, newcite);
            OnPropertyChanged("Citations");

            //TODO: _docLayer.RefreshDocCites();

        }

        public void DeleteCite(Tools.Citation.Citation citation)
        {
            var mb = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this citation from the document?", "Confirm", System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (mb == System.Windows.Forms.DialogResult.OK)
            {
                Citations.Remove(citation);
                _repository.DeleteCitation(citation);

                //TODO: _docLayer.RefreshDocCites();
            }
        }

        internal void AddNewCite(Tools.Citation.Citation cite)
        {
            Repository.AddCitation(cite); 

        }

        internal void RefreshCites()
        {
            throw new NotImplementedException();
        }

        internal void ResetFormatList()
        {
            FormatList_Long.Clear();
            //AddTestFormatBlock();
            OnPropertyChanged("FormatList");
        }
    }
}
