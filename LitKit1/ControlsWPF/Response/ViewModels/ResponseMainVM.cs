using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using Tools.Response;

namespace LitKit1.ControlsWPF.Response.ViewModels
{
    public class ResponseMainVM : INotifyPropertyChanged
    {
        #region properties
        Application _app;
        private string _responding;
        private bool _respondingIsPlural;
        private string _propounding;
        private DocType _docType;
        private ObservableCollection<DocType> _docTypes;
        private ResponseRepository _repository;
        private ObservableCollection<Tools.Response.Response> _responses;


        public string Responding
        {
            get { return _responding; }
            set
            {
                _responding = value;
                OnPropertyChanged("Responding");
            }
        }
        public bool RespondingIsPlural
        {
            get { return _respondingIsPlural; }
            set
            {
                _respondingIsPlural = value;
                OnPropertyChanged("RespondingIsPlural");
            }
        }
        public string Propounding
        {
            get { return _propounding; }
            set
            {
                _propounding = value;
                OnPropertyChanged("Propounding");
            }
        }

        public DocType DocType
        {
            get { return _docType; }
            set
            {
                _docType = value;
                OnPropertyChanged("DocType");
            }
        }
        public ObservableCollection<DocType> docTypes
        {
            get
            {
                return _docTypes;
            }
            set
            {
                _docTypes = value;
            }
        }

        public ResponseRepository Repository
        {
            get { return _repository; }
            set
            {
                _repository = value;
                OnPropertyChanged("Repository");
            }
        }
        public ObservableCollection<Tools.Response.Response> Responses
        {
            get { return _responses; }
            private set
            {
                _responses = value;
            }
        }
        #endregion

        public ResponseMainVM()
        {
            _app = Globals.ThisAddIn.Application;

            _docTypes = new ObservableCollection<DocType>() { DocType.Complaint, DocType.Admission, DocType.Production, DocType.Interrogatory };

            _repository = new ResponseRepository(_app);
            _responses = Repository.GetResponses();

            _responding = Repository.GetDocProps(_app, DocPropsNode.Responding);
            bool.TryParse(Repository.GetDocProps(_app, DocPropsNode.RespondingPlural), out _respondingIsPlural);
            _propounding = Repository.GetDocProps(_app, DocPropsNode.Propounding);
            Enum.TryParse(Repository.GetDocProps(_app, DocPropsNode.DocType), out _docType);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region Repsonses
        public void AddNewResponse(Tools.Response.Response response)
        {
            Repository.AddCustomResponse(response);
            Responses.Add(response);
        }

        public void EditResponse(Tools.Response.Response response)
        {
            Repository.UpdateResponse(response);
            Responses = Repository.GetResponses();
        }

        public void DeleteResponse(Tools.Response.Response response)
        {
            Repository.DeleteResponse(response.ID);
            Responses.Remove(response);
        }
        #endregion

        #region Doc Properties

        public void updateDocProperties()
        {
            Repository.UpdateDocProps(_app, Responding, RespondingIsPlural, Propounding, DocType);
        }

        #endregion

        #region Import/Export
        internal void BatchImportResponses()
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
                int respSuccess;
                int respFail;
                int respRepeated;


                batchFormatting(doc, out formatSuccess, out formatFail);
                batchResponses(doc, out respSuccess, out respFail, out respRepeated);


                System.Windows.Forms.MessageBox.Show(
                "Format Nodes Loaded: " + formatSuccess.ToString() + Environment.NewLine +
                "Format Nodes Failed: " + formatFail.ToString() + Environment.NewLine +
                Environment.NewLine +
                "Citations Added: " + respSuccess.ToString() + Environment.NewLine +
                "Citations Failed: " + respFail.ToString() + Environment.NewLine +
                "Redundant Citations Skipped: " + respRepeated.ToString()
                );
            }
            catch { throw new FileNotFoundException(); }
        }

        private void batchFormatting(XmlDocument doc, out int formatSuccess, out int formatFail)
        {

            formatSuccess = 0;
            formatFail = 0;

            var formatNode = /*doc.SelectSingleNode("//Document");*/ doc.ChildNodes[1].FirstChild;
            if (formatNode != null && formatNode.Name=="Document")
            {
                try
                {
                    Responding = /*formatNode.SelectSingleNode("//Responding").InnerText;*/ formatNode.ChildNodes[0].InnerText;
                    RespondingIsPlural = bool.Parse(/*formatNode.SelectSingleNode("//RespondingPlural").InnerText*/ formatNode.ChildNodes[1].InnerText );
                    Propounding = /*formatNode.SelectSingleNode("//Propounding").InnerText;*/ formatNode.ChildNodes[2].InnerText;

                    DocType type = DocType.Admission;
                    Enum.TryParse(
                        /*formatNode.SelectSingleNode("//DocType").InnerText*/ formatNode.ChildNodes[3].InnerText,
                        out type);
                    DocType = type;

                    //Update the Cite Formatting and save ;

                    Repository.UpdateDocProps(_app, Responding, RespondingIsPlural, Propounding, DocType);

                    formatSuccess++;
                }
                catch
                {
                    formatFail++;
                }
            }

        }

        private void batchResponses(XmlDocument doc, out int respSuccess, out int respFail, out int respRepeated)
        {
            respSuccess = 0;
            respFail = 0;
            respRepeated = 0;

            var ResponseNode = doc.SelectNodes("//Response");
            for (int i = 0; i < ResponseNode.Count; i++)
            {
                var children = ResponseNode[i].ChildNodes;
                try
                {
                    string ID = children[0].InnerText;
                    string Name = children[1].InnerText;

                    List<DocType> list = new List<DocType>();
                    var DocTypeNodes = children[2].ChildNodes;

                    for (int j = 0; j < DocTypeNodes.Count; j++)
                    {
                        DocType Type = DocType.Admission;
                        Enum.TryParse(DocTypeNodes[j].Name, out Type);
                        list.Add(Type);
                    }

                    string DisplayText = children[3].InnerText;

                    Tools.Response.Response response = new Tools.Response.Response(ID, Name, list, DisplayText);

                    if (Responses.Where(n => n.DisplayText == response.DisplayText).Count() > 0)
                    {
                        respRepeated++;
                    }
                    else if (Responses.Where(n => n.ID == response.ID).Count() != 0)
                    {
                        EditResponse(response);
                        respSuccess++;
                    }
                    else
                    { 
                        AddNewResponse(response);
                        respSuccess++;
                    }

                }
                catch { respFail++; }
            };

        }

        internal void ExportResponses()
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
                Repository.ExportResponses(Path);

            }
        }
        #endregion
    }
}
