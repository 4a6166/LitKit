using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Tools.Response;

namespace LitKit1.ControlsWPF.Response.ViewModels
{
    public class EditResponseVM : INotifyPropertyChanged
    {
        private Tools.Response.Response _editResponseRsp;
        private DocType _docType;
        private ObservableCollection<string> _standardResponseTexts;
        int isloaded = 0;
        private Visibility _visibility = Visibility.Collapsed;

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


        public ObservableCollection<string> StandardResponseTexts
        {
            get { return _standardResponseTexts; }
            set
            {
                _standardResponseTexts = value;
            }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public EditResponseVM(Tools.Response.Response response, DocType docType)
        {
            EditResponseRsp = response;
            DocType = docType;
            
            StandardResponseTexts = new ObservableCollection<string>();
            var l = ResponseStandardRepository.GetResponseByID(response.ID);
            if (l != null)
            {
                foreach (string s in l.Texts)
                {
                    StandardResponseTexts.Add(s);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public void updateEditResponseRsp(string displayText)
        {
            EditResponseRsp.DisplayText = displayText;
            OnPropertyChanged("EditResponseRsp");
        }
    }

}
