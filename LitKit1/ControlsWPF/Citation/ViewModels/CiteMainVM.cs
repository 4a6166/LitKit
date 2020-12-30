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
        private Tools.Citation.Citation _selectedCite;
        private ObservableCollection<Tools.Citation.Citation> _citations;
        /// <summary>
        /// Binding property for the List View, separated from all tp allow for filtering
        /// </summary>

        #endregion

        #region Public properties
        public Microsoft.Office.Interop.Word.Application _app
        {
            get
            {
                return Globals.ThisAddIn.Application;
            }
            private set { }
        }
        public CitationRepository Repository
        {
            get { return _repository; }
            private set
            {
                // Add INotifyChanged code
                _repository = value;
            }
        }

        public Tools.Citation.Citation SelectedCite
        {
            get { return _selectedCite; }
            set
            {
                // Add INotifyChanged code
                _selectedCite = value;
            }
        }

        public ObservableCollection<Tools.Citation.Citation> Citations
        {
            get { return _citations; }
            private set
            {
                // Add INotifyChanged code
                _citations = value;
            }
        }
        #endregion

        public CiteMainVM()
        {
            _repository = new CitationRepository(_app);
            _repository.AddTestCitations();
            _citations = ListToObservableCollection(_repository.Citations);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }



        #region Data Transformation

        public ObservableCollection<Tools.Citation.Citation> ListToObservableCollection(List<Tools.Citation.Citation> cites)
        {
            ObservableCollection<Tools.Citation.Citation> observableCites = new ObservableCollection<Tools.Citation.Citation>();
            foreach (Tools.Citation.Citation cite in cites)
            {
                observableCites.Add(cite);
            }
            return observableCites;
        }

        #endregion



    }
}
