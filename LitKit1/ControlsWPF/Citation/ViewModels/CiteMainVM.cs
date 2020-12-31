using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Citation;

namespace LitKit1.ControlsWPF.Citation.ViewModels
{
    public class CiteMainVM
    {
        #region Private properties
        private CitationRepository _repository;
        private Tools.Citation.Citation _selectedCite;
        private ObservableCollection<Tools.Citation.Citation> _citations;
        
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
            _citations = ListToObservableCollection(_repository.Citations);
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
