using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LitKit1.ControlsWPF.Citation.ViewModels
{
    public class EditCiteVM : INotifyPropertyChanged
    {

        private Tools.Citation.Citation _editCiteCitation;
        private bool _editCiteOpen;
        int isloaded = 0;

        public Tools.Citation.Citation EditCiteCitation
        {
            get { return _editCiteCitation; }
            set
            {
                _editCiteCitation = value;
                if (isloaded >1)
                {
                    OnPropertyChanged("EditCiteCitation");
                }
                else isloaded++;
            }
        }

        public bool EditCiteOpen
        {
            get { return _editCiteOpen; }
            set
            {
                _editCiteOpen = value;
                if (isloaded >1)
                {
                    OnPropertyChanged("EditCiteOpen");
                }
                else isloaded++;
            }
        }

        public EditCiteVM(Tools.Citation.Citation cite, bool visible)
        {
            EditCiteCitation = cite;
            EditCiteOpen = visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
