using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Tools.Exhibit
{
    public class ExhibitRepository
    {
        public ExhibitRepository(Application _app)
        {
            this._app = _app;

            if (_app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace).Count == 0)
            {
                FrameCustomXMLDoc();
            }

        }
        #region builds initial custom XML doc if it doens't exist
        void FrameCustomXMLDoc()
        {

            XDocument xDocument =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(rootName, "")
                );

            xDocument.Element(rootName).Add(new XElement("Format",
                new XElement("FirstCite", "Exhibit {INDEX}, {PINCITE}{DESC} ({BATES})"),
                new XElement("FollowingCites", "Exhibit {INDEX}, {PINCITE}{DESC} ({BATES})"),
                new XElement("IndexStyle", "Numbers"),
                new XElement("IndexStart", "1"),
                new XElement("IdCite", "True"),
                new XElement("FormatCustomized", "False"),

                // used for standard formatting form
                new XElement("Intro", "Exhibit"),
                new XElement("DescBatesFormat", "Description (Bates)"),
                new XElement("UniformCites", "True"), //First and following cites are in the same format
                new XElement("Parentheses", "False")
                ));

            string docString = /*@"<?xml version="+quotes+"1.0" + quotes + " encoding=" + quotes + "UTF - 8" + quotes + " standalone =" + quotes + "yes" + quotes + " ?>" +*/
                xDocument.ToString();

            _app.ActiveDocument.CustomXMLParts.Add(docString);

        }
        #endregion

        private readonly Application _app;

        static string NameSpace = "Prelimine Litkit Exhibits";
        static XNamespace name = NameSpace;
        static XName rootName = name + "Exhibits";

        public void UpdateFormatting(string FirstCite, string FollowingCites, string IndexStyle, string IndexStart, bool UniformCites, bool IdCite, bool FormatCustomized)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode("//Format");
            FormattingNode.SelectSingleNode("//FirstCite").Text = FirstCite;
            FormattingNode.SelectSingleNode("//FollowingCites").Text = FollowingCites;
            FormattingNode.SelectSingleNode("//IndexStyle").Text = IndexStyle;
            FormattingNode.SelectSingleNode("//IndexStart").Text = IndexStart;
            FormattingNode.SelectSingleNode("//UniformCites").Text = UniformCites.ToString();
            FormattingNode.SelectSingleNode("//IdCite").Text = IdCite.ToString();
            FormattingNode.SelectSingleNode("//FormatCustomized").Text = FormatCustomized.ToString();
        }

        public void UpdateStandardFormatting(string FirstCite, string FollowingCites, string IndexStyle, string IndexStart, bool UniformCites, bool IdCite, bool FormatCustomized, string intro, string descBatesFormat, bool parentheses)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode("//Format");
            FormattingNode.SelectSingleNode("//FirstCite").Text = FirstCite;
            FormattingNode.SelectSingleNode("//FollowingCites").Text = FollowingCites;
            FormattingNode.SelectSingleNode("//IndexStyle").Text = IndexStyle;
            FormattingNode.SelectSingleNode("//IndexStart").Text = IndexStart;
            FormattingNode.SelectSingleNode("//UniformCites").Text = UniformCites.ToString();
            FormattingNode.SelectSingleNode("//IdCite").Text = IdCite.ToString();
            FormattingNode.SelectSingleNode("//FormatCustomized").Text = FormatCustomized.ToString();

            FormattingNode.SelectSingleNode("//Intro").Text = intro;
            FormattingNode.SelectSingleNode("//DescBatesFormat").Text = descBatesFormat;
            FormattingNode.SelectSingleNode("//Parentheses").Text = parentheses.ToString();

        }

        public string GetFormatting(FormatNodes node)  //TODO: check why this loops so many times when Updating Formatting on Exhibit Format
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode("//Format");
            CustomXMLNode FormatNode = FormattingNode.SelectSingleNode("//" + node.ToString());
            string result = FormatNode.Text.Replace("\\u00A0", "\u00A0");

            return result;
        }

        #region Exhibits
        public void AddExhibit(string Description, string BatesNumber)
        {
            Exhibit newExhibit = new Exhibit(Description, BatesNumber);

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ExhibitsNode = customXmlDoc.SelectSingleNode("//Format").ParentNode;
            customXmlDoc.AddNode(ExhibitsNode, "Exhibit", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes ExhibitNodes = customXmlDoc.SelectNodes("//Exhibit");
            CustomXMLNode ExhibitNode = ExhibitNodes[ExhibitNodes.Count];
            customXmlDoc.AddNode(ExhibitNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newExhibit.ID);
            customXmlDoc.AddNode(ExhibitNode, "Description", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newExhibit.Description);
            customXmlDoc.AddNode(ExhibitNode, "Bates", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newExhibit.BatesNumber);
        }

        public void DeleteExhibit(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];

            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//Exhibit");
            foreach (CustomXMLNode exh in exhibitNodes)
            {
                if (exh.SelectSingleNode("ID").Text == id)
                {
                    exh.Delete();
                }
            }

        }

        public Exhibit GetExhibit(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            id = id.Split('|')[0];

            List<CustomXMLNode> nodesList = new List<CustomXMLNode>();
            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//Exhibit");
            foreach (CustomXMLNode exh in exhibitNodes)
            {
                if (exh.SelectSingleNode("ID").Text == id)
                {
                    nodesList.Add(exh);
                }
            }
            Exhibit exhibit = new Exhibit(nodesList.First().SelectSingleNode("ID").Text);
            exhibit.Description = nodesList.First().SelectSingleNode("Description").Text;
            exhibit.BatesNumber = nodesList.First().SelectSingleNode("Bates").Text;

            return exhibit;
        }

        public IEnumerable<Exhibit> GetExhibits()
        {

            List<Exhibit> exhibits = new List<Exhibit>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//Exhibit");

            foreach (CustomXMLNode element in exhibitNodes)
            {
                string ID = element.SelectSingleNode("ID").Text;
                Exhibit exhibit = new Exhibit(ID);
                exhibit.Description = element.SelectSingleNode("Description").Text;
                exhibit.BatesNumber = element.SelectSingleNode("Bates").Text;

                exhibits.Add(exhibit);
            }

            return exhibits.AsEnumerable();
        }

        public void UpdateExhibit(string id, string Description, string BatesNumber)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//Exhibit");
            foreach (CustomXMLNode exh in exhibitNodes)
            {
                if (exh.SelectSingleNode("ID").Text == id)
                {
                    exh.SelectSingleNode("Description").Text = Description;
                    exh.SelectSingleNode("Bates").Text = BatesNumber;
                }
            }
        }
        #endregion

        #region Legal and Record Cites
        public void AddLRCite(string LongCite, string ShortCite)
        {
            LegalRecordCite newExhibit = new LegalRecordCite(LongCite, ShortCite);

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ExhibitsNode = customXmlDoc.SelectSingleNode("//Format").ParentNode;
            customXmlDoc.AddNode(ExhibitsNode, "LRCite", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes ExhibitNodes = customXmlDoc.SelectNodes("//LRCite");
            CustomXMLNode ExhibitNode = ExhibitNodes[ExhibitNodes.Count];
            customXmlDoc.AddNode(ExhibitNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newExhibit.ID);
            customXmlDoc.AddNode(ExhibitNode, "LongCite", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newExhibit.LongCite);
            customXmlDoc.AddNode(ExhibitNode, "ShortCite", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newExhibit.ShortCite);
        }

        public void DeleteLRCite(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];

            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//LRCite");
            foreach (CustomXMLNode exh in exhibitNodes)
            {
                if (exh.SelectSingleNode("ID").Text == id)
                {
                    exh.Delete();
                }
            }

        }

        public LegalRecordCite GetLRCite(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            id = id.Split('|')[0];

            List<CustomXMLNode> nodesList = new List<CustomXMLNode>();
            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//LRCite");
            foreach (CustomXMLNode exh in exhibitNodes)
            {
                if (exh.SelectSingleNode("ID").Text == id)
                {
                    nodesList.Add(exh);
                }
            }
            LegalRecordCite LRCite = new LegalRecordCite(nodesList.First().SelectSingleNode("ID").Text);
            LRCite.LongCite = nodesList.First().SelectSingleNode("LongCite").Text;
            LRCite.ShortCite = nodesList.First().SelectSingleNode("ShortCite").Text;

            return LRCite;
        }

        public IEnumerable<LegalRecordCite> GetLRCites()
        {

            List<LegalRecordCite> cites = new List<LegalRecordCite>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//LRCite");

            foreach (CustomXMLNode element in exhibitNodes)
            {
                string ID = element.SelectSingleNode("ID").Text;
                LegalRecordCite cite = new LegalRecordCite(ID);
                cite.LongCite = element.SelectSingleNode("LongCite").Text;
                cite.ShortCite = element.SelectSingleNode("ShortCite").Text;

                cites.Add(cite);
            }

            return cites.AsEnumerable();
        }

        public void UpdateLRCite(string id, string LongCite, string ShortCite)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes exhibitNodes = customXmlDoc.SelectNodes("//LRCite");
            foreach (CustomXMLNode exh in exhibitNodes)
            {
                if (exh.SelectSingleNode("ID").Text == id)
                {
                    exh.SelectSingleNode("LongCite").Text = LongCite;
                    exh.SelectSingleNode("ShortCite").Text = ShortCite;
                }
            }
        }


        #endregion
    }
}
