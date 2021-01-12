//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Citation.TESTResources
//{
//    public class Cite : INotifyPropertyChanged
//    {
//        private string _ID;
//        private string _ReferenceName;
//        private string _CiteType;
//        private string _LongDescription;
//        private string _ShortDescription;
//        private string _OtherIdentifier;

//        public string ID
//        {
//            get { return _ID; }
//            set
//            {
//                _ID = value;
//                RaisePropertyChanged("ID");
//            }
//        }

//        public string ReferenceName { get; set; }

//        public string CiteType { get; set; }

        
//        public string LongDescription
//        {
//            get { return _LongDescription; }
//            set
//            {
//                _LongDescription = value;
//                RaisePropertyChanged("LongDescription");
//            }
//        }

//        public string ShortDescription { get; set; }

//        public string OtherIdentifier { get; set; }

//        public event PropertyChangedEventHandler PropertyChanged;

//        public void RaisePropertyChanged(string propertyName)
//        {
//            if(PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//            }
//        }
//    }
//}
