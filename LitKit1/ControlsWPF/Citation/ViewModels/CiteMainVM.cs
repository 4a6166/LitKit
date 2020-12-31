using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion

        #region Public properties
        public Microsoft.Office.Interop.Word.Application _app;

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
        #endregion

        public CiteMainVM()
        {
            _app = Globals.ThisAddIn.Application;

            _repository = new CitationRepository(_app);
            _docLayer = new CiteDocLayer(_app);
            _repository.AddTestCitations();
            LoadCitationsFromRepo();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }



        #region Data Transformation

        public void LoadCitationsFromRepo()
        {
            ObservableCollection<Tools.Citation.Citation> observableCites = new ObservableCollection<Tools.Citation.Citation>();
            foreach (Tools.Citation.Citation cite in _repository.Citations)
            {
                observableCites.Add(cite);
            }
            Citations = observableCites;
        }

        #endregion


        public void InsertCite(Tools.Citation.Citation citation)
        {
            System.Windows.Forms.MessageBox.Show("Selected Citation: " + citation.ID);


            //_docLayer.InsertCiteAtSelection(citation);
            //LoadCitationsFromRepo();
            //_docLayer.RefreshDocCites();
        }

        public void EditCite(Tools.Citation.Citation citation)
        {
            System.Windows.Forms.MessageBox.Show("Selected Citation: " + citation.ID);

            //_repository.UpdateCitation(citation);
            //LoadCitationsFromRepo();
            //_docLayer.RefreshDocCites();

        }

        public void DeleteCite(Tools.Citation.Citation citation)
        {
            var mb = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this citation from the document?", "Confirm", System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (mb == System.Windows.Forms.DialogResult.OK)
            {

                System.Windows.Forms.MessageBox.Show("Selected Citation: " + citation.ID);

                //_repository.DeleteCitation(citation);
                //LoadCitationsFromRepo();
                //_docLayer.RefreshDocCites();
            }
        }
    }
}
