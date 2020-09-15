using Microsoft.Office.Core;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Tools.Exhibit
{
    public class ExhibitRepository : IExhibitRepository
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
                new XElement("Intro", "Exhibit"),
                new XElement("Numbering", "1, 2, 3..."),
                new XElement("FirstOnly", "In first citation only"),
                new XElement("DescBatesFormat", "Description, Bates"),
                new XElement("Parentheses", "False"),
                new XElement("IdCite", "True")
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

        public void UpdateFormatting(string Intro, string Numbering, string FirstOnly, string DescBatesFormat, string Parentheses, string IdCite)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode("//Format");
            FormattingNode.SelectSingleNode("//Intro").Text= Intro;
            FormattingNode.SelectSingleNode("//Numbering").Text = Numbering;
            FormattingNode.SelectSingleNode("//FirstOnly").Text = FirstOnly;
            FormattingNode.SelectSingleNode("//DescBatesFormat").Text = DescBatesFormat;
            FormattingNode.SelectSingleNode("//Parentheses").Text = Parentheses;
            FormattingNode.SelectSingleNode("//IdCite").Text = IdCite;


        }
        
        

        public string GetFormatting(FormatNodes node)  //TODO: check why this loops so many times when Updating Formatting on Exhibit Format
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode FormattingNode = customXmlDoc.SelectSingleNode("//Format");
            CustomXMLNode FormatNode = FormattingNode.SelectSingleNode("//"+node.ToString());
            return FormatNode.Text;
        }

        public void AddExhibit(string Description, string BatesNumber)
        {
            Exhibit newExhibit = new Exhibit(Description, BatesNumber);
            
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ExhibitsNode = customXmlDoc.SelectSingleNode("//Format").ParentNode;   
            customXmlDoc.AddNode(ExhibitsNode, "Exhibit","",null,MsoCustomXMLNodeType.msoCustomXMLNodeElement,"");

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
                if(exh.SelectSingleNode("ID").Text == id)
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
    }
}
