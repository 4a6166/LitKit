using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Tools.Citation
{
    public class CitationRepository
    {
        public Application _app { get; private set; }
        public List<Citation> Citations { get; private set; }
        public CiteFormatting CiteFormatting { get; private set; }

        static string _Namespace = "Prelimine Litkit Citation Tool";
        static XNamespace Namespace = _Namespace;
        static string CitationRoot = "//Citation";
        static string FormattingRoot = "//Format";
        static string XML_ID = "ID";
        static string XML_RefName = "RefName";
        static string XML_Type = "Type";
        static string XML_Long = "Long";
        static string XML_Short = "Short";
        static string XML_OtherID = "OtherID";

        public CitationRepository(Application _app)
        {
            this._app = _app;

            if (_app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace).Count == 0)
            {
                FrameCustomXMLDoc();
            }
            SetCiteFormatting();
            Citations = GetCitationsFromDB(CiteType.All);
        }

        private void FrameCustomXMLDoc()
        {

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            string path = string.Empty;

            try // For debugging
            {
                path = Root + @"\" +@"CitationsCustomXMLFrame.xml";
            }
            catch { }
            //try //For user testing
            //{
            //    string Parent = Directory.GetCurrentDirectory() + @"\..\";
            //    var Dirs = Directory.EnumerateDirectories(Parent);

            //    string Rootdll = Dirs.Where(n => n.Contains("litkit.dll")).SingleOrDefault();

            //    path = Rootdll + @"\CitationsCustomXMLFrame.xml";
            //}
            //catch { }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            _app.ActiveDocument.CustomXMLParts.Add(xmlDocument.OuterXml);


        }

        #region Formatting
        private string GetFormattingFromDB(FormatNode Node)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode(FormattingRoot);
            CustomXMLNode FormatNode = FormattingNode.SelectSingleNode("//" + Node.ToString());
            string result = FormatNode.Text.Replace("\\u00A0", "\u00A0");

            return result;
        }
        private void SetCiteFormatting()
        {
            string ExhibitIntro = GetFormattingFromDB(FormatNode.Intro);
            string ExhibitLongFormat = GetFormattingFromDB(FormatNode.Long);
            string ExhibitShortFormat = GetFormattingFromDB(FormatNode.Short);

            ExhibitIndexStyle ExhibitIndexStyle = ExhibitIndexStyle.Numbers;
            Enum.TryParse(GetFormattingFromDB(FormatNode.IndexStyle), out ExhibitIndexStyle);
            int ExhibitIndexStart = Int32.Parse(GetFormattingFromDB(FormatNode.IndexStart));
            bool HasSurroundingParentheses = bool.Parse(GetFormattingFromDB(FormatNode.Parentheses));
            bool HasIdCite = bool.Parse(GetFormattingFromDB(FormatNode.IdCite));

            CiteFormatting = new CiteFormatting(ExhibitIntro, ExhibitLongFormat, ExhibitShortFormat, ExhibitIndexStyle, ExhibitIndexStart, HasSurroundingParentheses, HasIdCite);
        }

        public void UpdateDBFormatting(CiteFormatting formatting)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode(FormattingRoot);

            FormattingNode.SelectSingleNode("//Intro").Text = formatting.ExhibitIntro;
            FormattingNode.SelectSingleNode("//Long").Text = formatting.ExhibitLongFormat;
            FormattingNode.SelectSingleNode("//Short").Text = formatting.ExhibitShortFormat;
            FormattingNode.SelectSingleNode("//IndexStyle").Text = formatting.ExhibitIndexStyle.ToString();
            FormattingNode.SelectSingleNode("//IndexStart").Text = formatting.ExhibitIndexStart.ToString();
            FormattingNode.SelectSingleNode("//Parentheses").Text = formatting.hasSurroundingParentheses.ToString();
            FormattingNode.SelectSingleNode("//IdCite").Text = formatting.hasIdCite.ToString();
        }
        #endregion

        #region Citations
        private void AddCitationToDB(Citation citation)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];
            CustomXMLNode ExhibitsNode = customXmlDoc.SelectSingleNode(FormattingRoot).ParentNode;
            customXmlDoc.AddNode(ExhibitsNode, "Citation", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes CiteNodes = customXmlDoc.SelectNodes(CitationRoot);
            CustomXMLNode CiteNode = CiteNodes[CiteNodes.Count];
            customXmlDoc.AddNode(CiteNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, citation.ID);
            customXmlDoc.AddNode(CiteNode, "RefName", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, citation.ReferenceName);
            customXmlDoc.AddNode(CiteNode, "Type", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, citation.CiteType.ToString());
            customXmlDoc.AddNode(CiteNode, "Long", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, citation.LongDescription);
            customXmlDoc.AddNode(CiteNode, "Short", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, citation.ShortDescription);
            customXmlDoc.AddNode(CiteNode, "OtherID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, citation.OtherIdentifier);
        }
        private void DeleteCitationFromDB(Citation citation)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];

            CustomXMLNodes citeNodes = customXmlDoc.SelectNodes(CitationRoot);
            foreach (CustomXMLNode cite in citeNodes)
            {
                if (cite.SelectSingleNode("ID").Text == citation.ID)
                {
                    cite.Delete();
                }
            }

        }
        private Citation GetCitationFromDB(string ID)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];

            CustomXMLNodes CiteNodes = customXmlDoc.SelectNodes(CitationRoot);
            foreach (CustomXMLNode cite in CiteNodes)
            {
                if (cite.SelectSingleNode("ID").Text == ID)
                {
                    string RefName = cite.SelectSingleNode("RefName").Text;

                    CiteType citeType = CiteType.None;
                    Enum.TryParse(cite.SelectSingleNode("Type").Text, out citeType);

                    string longDescription = cite.SelectSingleNode("Long").Text;
                    string shortDescription = cite.SelectSingleNode("Short").Text;
                    string otherID = cite.SelectSingleNode("OtherID").Text;

                    return new Citation(ID, citeType, longDescription, shortDescription, otherID, RefName);
                }
            }
            return null;  // Should only fire if ID is not found

        }
        private List<Citation> GetCitationsFromDB(CiteType Type)
        {
            List<Citation> citations = new List<Citation>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];

            CustomXMLNodes CiteNodes = customXmlDoc.SelectNodes(CitationRoot);
            if (Type == CiteType.All)
            {
                foreach (CustomXMLNode cite in CiteNodes)
                {

                    string ID = cite.SelectSingleNode("ID").Text;
                    string RefName = cite.SelectSingleNode("RefName").Text;

                    CiteType citeType = CiteType.None;
                    Enum.TryParse(cite.SelectSingleNode("Type").Text, out citeType);

                    string longDescription = cite.SelectSingleNode("Long").Text;
                    string shortDescription = cite.SelectSingleNode("Short").Text;
                    string otherID = cite.SelectSingleNode("OtherID").Text;

                    citations.Add(new Citation(ID, citeType, longDescription, shortDescription, otherID, RefName));
                }
            }
            else
            {
                foreach (CustomXMLNode cite in CiteNodes)
                {
                    if (cite.SelectSingleNode("Type").Text == Type.ToString())
                    {
                        string ID = cite.SelectSingleNode("ID").Text;
                        string RefName = cite.SelectSingleNode("RefName").Text;

                        CiteType citeType = CiteType.None;
                        Enum.TryParse(cite.SelectSingleNode("Type").Text, out citeType);

                        string longDescription = cite.SelectSingleNode("Long").Text;
                        string shortDescription = cite.SelectSingleNode("Short").Text;
                        string otherID = cite.SelectSingleNode("OtherID").Text;

                        citations.Add(new Citation(ID, citeType, longDescription, shortDescription, otherID, RefName));
                    }
                }
            }
            return citations;
        }
        private void UpdateCitationinDB(Citation citation)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(_Namespace)[1];
            CustomXMLNodes CiteNodes = customXmlDoc.SelectNodes(CitationRoot);
            foreach (CustomXMLNode cite in CiteNodes)
            {
                if (cite.SelectSingleNode(XML_ID).Text == citation.ID)
                {
                    cite.SelectSingleNode(XML_RefName).Text = citation.ReferenceName;
                    cite.SelectSingleNode(XML_Type).Text = citation.CiteType.ToString();
                    cite.SelectSingleNode(XML_Long).Text = citation.LongDescription;
                    cite.SelectSingleNode(XML_Short).Text = citation.ShortDescription;
                    cite.SelectSingleNode(XML_OtherID).Text = citation.OtherIdentifier;
                }
            }

        }

        public void AddCitation(Citation citation)
        {
            AddCitationToDB(citation);
            Citations.Add(citation);
        }

        public void DeleteCitation(Citation citation)
        {
            DeleteCitationFromDB(citation);
            Citations.Remove(citation);
        }

        public void UpdateCitation(Citation citation)
        {
            UpdateCitationinDB(citation);
            Citations[Citations.IndexOf(citation)] = citation;
        }

        #endregion

        public void AddTestCitations()
        {
            for (int i = 1; i <= 5; i++)
            {
                AddCitation(new Citation(i.ToString(), CiteType.Exhibit, "Long Description " + i, "Short " + i));
            }

            for (int i = 1; i <= 5; i++)
            {
                AddCitation(new Citation(i.ToString(), CiteType.Legal, "Long Description " + i, "Short " + i));
            }

            for (int i = 1; i <= 5; i++)
            {
                AddCitation(new Citation(i.ToString(), CiteType.Record, "Long Description " + i, "Short " + i));
            }

            for (int i = 1; i <= 5; i++)
            {
                AddCitation(new Citation(i.ToString(), CiteType.Other, "Long Description " + i, "Short " + i));
            }
        }
    }
}
