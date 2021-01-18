using System.ComponentModel;
using Tools.Response;

namespace LitKit1.ControlsWPF.Response.ViewModels
{
    public class EditResponseVM : INotifyPropertyChanged
    {
        private Tools.Response.Response _editResponseRsp;
        private DocType _docType;
        private bool _editResponseOpen;
        int isloaded = 0;

        public Tools.Response.Response EditResponseRsp
        {
            get { return _editResponseRsp; }
            set
            {
                _editResponseRsp = value;
                if (isloaded > 1)
                {
                    OnPropertyChanged("EditResponseRsp");
                }
                else isloaded++;
            }
        }

        public DocType DocType
        {
            get { return _docType; }
            set
            {
                _docType = value;
                if (isloaded > 1)
                {
                    OnPropertyChanged("DocType");
                }
                else isloaded++;

            }
        }

        public bool EditResponseOpen
        {
            get { return _editResponseOpen; }
            set
            {
                _editResponseOpen = value;
                if (isloaded > 1)
                {
                    OnPropertyChanged("EditResponseOpen");
                }
                else isloaded++;
            }
        }

        public EditResponseVM(Tools.Response.Response response, DocType docType, bool visible)
        {
            EditResponseRsp = response;
            EditResponseOpen = visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}
