using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;

namespace Services.Response
{
    public class ResponseRespository
    {
        //***********NO FACTORY OR INTERFACE YET -> plan is to refactor to improve performance following user testing.
        public ResponseRespository(Application _app)
        {
            this._app = _app;

            ResponseStandardLanguage = LoadStandardLanguage();

            if (_app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace).Count == 0)
            {
                FrameCustomXMLDoc();
            }

        }

        #region builds initial custom XML doc if it doens't exist and loads standard Response language
        XmlDocument ResponseStandardLanguage;
        XmlDocument LoadStandardLanguage()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            string path = Root + @"\Response\ResponseStandardLanguage.xml";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            return xmlDocument;
        }

        
        void FrameCustomXMLDoc()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            string path = Root + @"\Response\ResponseFrame.xml";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            _app.ActiveDocument.CustomXMLParts.Add(xmlDocument.OuterXml);
        }
        #endregion

        private readonly Application _app;
        static string NameSpace = "Prelimine Litkit Response Tool";
        static XNamespace name = NameSpace;
        static XName rootName = name + "Responses";

        public void AddCustomResponse(string Name, bool Complaint, bool Admission, bool Production, bool Interrogatory, string DisplayText)
        {
            List<bool> DocTypes = new List<bool>
            {
                Complaint, Admission, Production, Interrogatory
            };

            Response newResponse = new Response(Name, DocTypes, DisplayText);

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;
            customXmlDoc.AddNode(ResponsesNode, "Response", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            CustomXMLNode ResponseNode = ResponseNodes[ResponseNodes.Count];
            customXmlDoc.AddNode(ResponseNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newResponse.ID);
            customXmlDoc.AddNode(ResponseNode, "Name", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newResponse.Name);
            customXmlDoc.AddNode(ResponseNode, "DocTypes", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNode DocTypesNode = ResponseNode.SelectSingleNode("//DocTypes");
            customXmlDoc.AddNode(DocTypesNode, "Complaint", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, Complaint.ToString());
            customXmlDoc.AddNode(DocTypesNode, "Admission", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, Admission.ToString());
            customXmlDoc.AddNode(DocTypesNode, "Production", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, Production.ToString());
            customXmlDoc.AddNode(DocTypesNode, "Interrogatory", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, Interrogatory.ToString());
            
            customXmlDoc.AddNode(ResponseNode, "DisplayText", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newResponse.DisplayText);
        }

        
        public OperationResult DeleteResponse(string id)
        {
            bool success = false;
            string deleteText = string.Empty; 

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];

            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            foreach (CustomXMLNode Response in ResponseNodes)
            {
                if (Response.SelectSingleNode("ID").Text == id)
                {
                    Response.Delete();
                    success = true;
                    deleteText = "Response at node " + id + " deleted.";
                }
                else deleteText = "Response at node " + id + " not found in CustomXML DB.";
            }

            return new OperationResult(success, deleteText);

        }


        public Response GetResponse(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];

            List<CustomXMLNode> nodesList = new List<CustomXMLNode>();
            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            foreach (CustomXMLNode Response in ResponseNodes)
            {
                if (Response.SelectSingleNode("ID").Text == id)
                {
                    nodesList.Add(Response);
                }
            }
            Response response = new Response(nodesList.First().SelectSingleNode("ID").Text, _app);
            response.Name = nodesList.First().SelectSingleNode("Name").Text;
            response.DisplayText = nodesList.First().SelectSingleNode("DisplayText").Text;

            List<bool> docTypes = new List<bool>
            {
                bool.Parse(nodesList.First().SelectSingleNode("DocTypes").SelectSingleNode("Complaint").Text),
                bool.Parse(nodesList.First().SelectSingleNode("DocTypes").SelectSingleNode("Admission").Text),
                bool.Parse(nodesList.First().SelectSingleNode("DocTypes").SelectSingleNode("Production").Text),
                bool.Parse(nodesList.First().SelectSingleNode("DocTypes").SelectSingleNode("Interrogatory").Text),

            };
            response.DocTypes = docTypes;

            return response;
        }
        
        public string GetDocProps(Application _app, DocPropsNode node)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode PropsNode = customXmlDoc.SelectSingleNode("//Document");
            
            return PropsNode.SelectSingleNode("//" + node.ToString()).Text;
        }

        
        public IEnumerable<Response> GetAnswers()
        {
            List<Response> responses = new List<Response>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");

            foreach (CustomXMLNode element in ResponseNodes)
            {
                string ID = element.SelectSingleNode("ID").Text;
                Response response = new Response(ID, _app);
               
                responses.Add(response);
            }

            return responses.AsEnumerable();
        }
        
        public void UpdateResponse(string id, string Name, string DisplayText)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            foreach (CustomXMLNode response in ResponseNodes)
            {
                if (response.SelectSingleNode("ID").Text == id)
                {
                    response.SelectSingleNode("Name").Text = Name;
                    response.SelectSingleNode("DisplayText").Text = DisplayText;
                }
            }
        }
    }
}
