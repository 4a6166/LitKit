using Citation.TESTResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citation.ViewModels
{
    public class MainViewVM
    {
        #region Private properties
        private CiteRepository _repository;
        private Cite _selectedCite;
        private ObservableCollection<Cite> _citations;
        
        #endregion

        #region Public properties
        public CiteRepository Repository
        {
            get { return _repository; }
            private set
            {
                // Add INotifyChanged code
                _repository = value;
            }
        }

        public Cite SelectedCite
        {
            get { return _selectedCite; }
            set
            {
                // Add INotifyChanged code
                _selectedCite = value;
            }
        }

        public ObservableCollection<Cite> Citations
        {
            get { return _citations; }
            private set
            {
                // Add INotifyChanged code
                _citations = value;
            }
        }
        #endregion

        public MainViewVM()
        {
            _repository = new CiteRepository();
            _citations = ListToObservableCollection(_repository.GetCites());
        }

        #region Data Transformation

        public ObservableCollection<Cite> ListToObservableCollection(List<Cite> cites)
        {
            ObservableCollection<Cite> observableCites = new ObservableCollection<Cite>();
            foreach (Cite cite in cites)
            {
                observableCites.Add(cite);
            }
            return observableCites;
        }

        #endregion
    }
}
