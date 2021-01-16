using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tools.Response;

namespace LitKit1.ControlsWPF.Response.ViewModels
{
    public class ResponseMainVM : INotifyPropertyChanged
    {
        #region properties
        private DocType _docType;
        private ObservableCollection<string> _responses;

        public DocType DocType
        {
            get { return _docType; }
            set
            {
                _docType = value;
                OnPropertyChanged("DocType");
            }
        }
        public ObservableCollection<string> Responses
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
            Responses = new ObservableCollection<string> 
            { 
                "Answer a Complaint",
                "Respond to Requests for Admission",
                "Respond to Requests for Production of Documents",
                "Respond to Interrogatories"
            };

            _docType = DocType.Admission;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        #region Import/Export
        internal void BatchImportResponses()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "XML | *.xml";
            openFileDialog.Title = "Import Citations and Cite Formatting";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.ShowDialog();

            //try
            //{
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(openFileDialog.FileName);

            //    int formatSuccess;
            //    int formatFail;
            //    int citeSuccess;
            //    int citeFail;
            //    int citeRepeated;


            //    batchFormatting(doc, out formatSuccess, out formatFail);
            //    batchCites(doc, out citeSuccess, out citeFail, out citeRepeated);


            //    System.Windows.Forms.MessageBox.Show(
            //    "Format Nodes Loaded: " + formatSuccess.ToString() + Environment.NewLine +
            //    "Format Nodes Failed: " + formatFail.ToString() + Environment.NewLine +
            //    Environment.NewLine +
            //    "Citations Added: " + citeSuccess.ToString() + Environment.NewLine +
            //    "Citations Failed: " + citeFail.ToString() + Environment.NewLine +
            //    "Redundant Citations Skipped: " + citeRepeated.ToString()
            //    );
            //}
            //catch { throw new FileNotFoundException(); }
        }

        private void batchFormatting(XmlDocument doc/*, out int formatSuccess, out int formatFail*/)
        {

            //formatSuccess = 0;
            //formatFail = 0;

            //var formatNode = doc.SelectSingleNode("//Format");
            //if (formatNode != null)
            //{
            //    try
            //    {
            //        string introLong = formatNode.SelectSingleNode("//IntroLong").InnerText;
            //        string introShort = formatNode.SelectSingleNode("//IntroShort").InnerText;

            //        ExhibitIndexStyle indexStyle = ExhibitIndexStyle.Numbers;
            //        Enum.TryParse(formatNode.SelectSingleNode("//IndexStyle").InnerText, out indexStyle);
            //        int indexStart = Int32.Parse(formatNode.SelectSingleNode("//IndexStart").InnerText);
            //        bool idCite = bool.Parse(formatNode.SelectSingleNode("//IdCite").InnerText);

            //        ObservableCollection<CiteFormatPiece> longFormat = new ObservableCollection<CiteFormatPiece>();
            //        var longnodes = formatNode.SelectSingleNode("//Long").ChildNodes;
            //        for (int i = 0; i < longnodes.Count; i++)
            //        {
            //            CiteFormatPieceType type = CiteFormatPieceType.FREETEXT;
            //            Enum.TryParse(longnodes[i].Name, out type);

            //            longFormat.Add(new CiteFormatPiece(type, longnodes[i].InnerText));
            //        }

            //        ObservableCollection<CiteFormatPiece> shortFormat = new ObservableCollection<CiteFormatPiece>();
            //        var shortnodes = formatNode.SelectSingleNode("//Short").ChildNodes;
            //        for (int i = 0; i < shortnodes.Count; i++)
            //        {
            //            CiteFormatPieceType type = CiteFormatPieceType.FREETEXT;
            //            Enum.TryParse(shortnodes[i].Name, out type);

            //            shortFormat.Add(new CiteFormatPiece(type, shortnodes[i].InnerText));
            //        }

            //        //Update the Cite Formatting and save 
            //        CiteFormatting formatting = new CiteFormatting(introLong, introShort, longFormat, shortFormat, indexStyle, indexStart, idCite);
            //        FormatList_Long = longFormat;
            //        OnPropertyChanged("FormatList_Long");

            //        FormatList_Short = shortFormat;
            //        OnPropertyChanged("FormatList_Short");

            //        Repository.CiteFormatting = formatting;
            //        Repository.UpdateCiteFormattingInDB(formatting);

            //        formatSuccess++;
            //    }
            //    catch
            //    {
            //        formatFail++;
            //    }
            //}

        }

        private void batchCites(XmlDocument doc/*, out int citeSuccess, out int citeFail, out int citeRepeated*/)
        {
            //citeSuccess = 0;
            //citeFail = 0;
            //citeRepeated = 0;

            //var citesNode = doc.SelectNodes("//Citation");
            //for (int i = 0; i < citesNode.Count; i++)
            //{
            //    var children = citesNode[i].ChildNodes;
            //    try
            //    {
            //        string ID = children[0].InnerText;
            //        string RefName = children[1].InnerText;

            //        CiteType Type = CiteType.Exhibit;
            //        Enum.TryParse(children[2].InnerText, out Type);

            //        string Long = children[3].InnerText;
            //        string Short = children[4].InnerText;
            //        string OtherID = children[5].InnerText;

            //        Tools.Citation.Citation cite = new Tools.Citation.Citation(ID, Type, Long, Short, OtherID, RefName);

            //        if (Citations.Where(n => n.ID == cite.ID).Count() == 0)
            //        {
            //            AddNewCite(cite);
            //            citeSuccess++;
            //        }
            //        else citeRepeated++;
            //    }
            //    catch { citeFail++; }
            //};

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

            //if (saveFileDialog.FileName != "")
            //{
            //    Path = saveFileDialog.FileName;
            //}
            //else
            //{ Path = null; }

            ////Check if file is available
            //FileInfo file = new FileInfo(Path);
            //if (!file.Exists)
            //{ FileAvailable = true; }
            //else
            //{
            //    try
            //    {
            //        using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            //        {
            //            stream.Close();
            //            FileAvailable = true;
            //        }
            //    }
            //    catch (IOException)
            //    {
            //        //the file is unavailable because it is:
            //        //still being written to
            //        //or being processed by another thread
            //        //or does not exist (has already been processed)
            //        FileAvailable = false;
            //        System.Windows.Forms.MessageBox.Show("File is open in another window or program. Please close the file and try again.");

            //    }
            //}

            //if (FileAvailable)
            //{
            //    Repository.ExportCites(Path);

            //}
        }
        #endregion
    }
}
