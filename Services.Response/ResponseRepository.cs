using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Services.Base;

namespace Tools.Response
{
    public class ResponseRepository
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //***********NO FACTORY OR INTERFACE YET -> plan is to refactor to improve performance following user testing.
        public ResponseRepository(Application _app)
        {
            log4net.Config.XmlConfigurator.Configure();

            this._app = _app;

            if (_app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace).Count == 0)
            {
                FrameCustomXMLDoc();
            }

        }

        
        void FrameCustomXMLDoc()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            string path = string.Empty;

            try // For debugging
            {
                path = Root + @"\Services\Response\ResponseFrame.xml";
            }
            catch { }
            //try //For user testing
            //{
            //    string Parent = Directory.GetCurrentDirectory() + @"\..\";
            //    var Dirs = Directory.EnumerateDirectories(Parent);

            //    string Rootdll = Dirs.Where(n => n.Contains("litkit.dll")).SingleOrDefault();

            //    path = Rootdll + @"\Services\Response\ResponseFrame.xml";
            //}
            //catch { }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            _app.ActiveDocument.CustomXMLParts.Add(xmlDocument.OuterXml);
        }


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

            Response newResponse = new Response(Guid.NewGuid().ToString(), Name, DocTypes, DisplayText);

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;
            customXmlDoc.AddNode(ResponsesNode, "Response", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            CustomXMLNode ResponseNode = ResponseNodes[ResponseNodes.Count];
            customXmlDoc.AddNode(ResponseNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newResponse.ID);
            customXmlDoc.AddNode(ResponseNode, "Name", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newResponse.Name);
            customXmlDoc.AddNode(ResponseNode, "DocType", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNode DocTypesNode = ResponseNode.SelectSingleNode("DocType");
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
            Response response = null;

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];

            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");

            foreach (CustomXMLNode resp in ResponseNodes)
            {
                if (resp.SelectSingleNode("ID").Text == id)
                {
                    string Name = resp.SelectSingleNode("Name").Text;

                    bool c = bool.Parse(resp.SelectSingleNode("DocType").SelectSingleNode("Complaint").Text);
                    bool a = bool.Parse(resp.SelectSingleNode("DocType").SelectSingleNode("Admission").Text);
                    bool p = bool.Parse(resp.SelectSingleNode("DocType").SelectSingleNode("Production").Text);
                    bool i = bool.Parse(resp.SelectSingleNode("DocType").SelectSingleNode("Interrogatory").Text);

                    List<bool> docTypes = new List<bool>
                        {
                            c, a, p, i
                        };

                    string DisplayText = resp.SelectSingleNode("DisplayText").Text;

                    response = new Response(id, Name, docTypes, DisplayText);
                }
            }
            return response;
        }

        public string GetDocProps(Application _app, DocPropsNode node)
        {
            int i = (int)node+1;

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;

            CustomXMLNode PropsNode = ResponsesNode.ChildNodes[1];
            return PropsNode.ChildNodes[i].Text;

        }

        public void UpdateDocProps(Application _app, string responding, string respondingPlural, string propounding, string docType)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;
            CustomXMLNode PropsNode = ResponsesNode.ChildNodes[1];

            PropsNode.ChildNodes[1].Text = responding;
            PropsNode.ChildNodes[2].Text = respondingPlural;
            PropsNode.ChildNodes[3].Text = propounding;
            PropsNode.ChildNodes[4].Text = docType;

        }

        
        public IEnumerable<Response> GetResponses()
        {
            List<Response> responses = new List<Response>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");

            foreach (CustomXMLNode element in ResponseNodes)
            {
                string ID = element.SelectSingleNode("ID").Text;
                Response response = GetResponse(ID);
               
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
