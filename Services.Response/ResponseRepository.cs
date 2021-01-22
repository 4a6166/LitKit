using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //***********NO FACTORY OR INTERFACE YET -> plan is to refactor to improve performance following user testing.

        private readonly Application _app;
        static string NameSpace = "Prelimine Litkit Response Tool";
        static XNamespace name = NameSpace;
        static XName rootName = name + "Responses";

        public ResponseRepository(Application _app)
        {
            //log4net.Config.XmlConfigurator.Configure();

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

            //try //For user testing
            //{
            //    string Parent = Directory.GetCurrentDirectory() + @"\..\";
            //    var Dirs = Directory.EnumerateDirectories(Parent);

            //    string Rootdll = Dirs.Where(n => n.Contains("litkit.dll")).SingleOrDefault();

            //    path = Rootdll + @"\Services\Response\ResponseFrame.xml";
            //}
            //catch
            //{ }
                //path for debugging
                path = Root + @"\Services\Response\ResponseFrame.xml";
            //}

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            _app.ActiveDocument.CustomXMLParts.Add(xmlDocument.OuterXml);
        }

        #region Doc Properties

        public string GetDocProps(Application _app, DocPropsNode node)
        {
            int i = (int)node + 1;

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;

            CustomXMLNode PropsNode = ResponsesNode.ChildNodes[1];
            return PropsNode.ChildNodes[i].Text;

        }

        public void UpdateDocProps(Application _app, string responding, bool respondingPlural, string propounding, DocType docType)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;
            CustomXMLNode PropsNode = ResponsesNode.ChildNodes[1];

            PropsNode.ChildNodes[1].Text = responding;
            PropsNode.ChildNodes[2].Text = respondingPlural.ToString();
            PropsNode.ChildNodes[3].Text = propounding;
            PropsNode.ChildNodes[4].Text = docType.ToString();

        }

        #endregion

        #region Responses

        public void AddCustomResponse(Response response)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode ResponsesNode = customXmlDoc.DocumentElement;
            customXmlDoc.AddNode(ResponsesNode, "Response", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            CustomXMLNode ResponseNode = ResponseNodes[ResponseNodes.Count];
            customXmlDoc.AddNode(ResponseNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, response.ID);
            customXmlDoc.AddNode(ResponseNode, "Name", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, response.Name);
            customXmlDoc.AddNode(ResponseNode, "DocType", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNode DocTypesNode = ResponseNode.SelectSingleNode("DocType");
            if(response.DocTypes.Contains(DocType.Complaint)) { customXmlDoc.AddNode(DocTypesNode, "Complaint"); }
            if (response.DocTypes.Contains(DocType.Admission)) { customXmlDoc.AddNode(DocTypesNode, "Admission"); }
            if (response.DocTypes.Contains(DocType.Production)) { customXmlDoc.AddNode(DocTypesNode, "Production"); }
            if (response.DocTypes.Contains(DocType.Interrogatory)) { customXmlDoc.AddNode(DocTypesNode, "Interrogatory"); }
            
            customXmlDoc.AddNode(ResponseNode, "DisplayText", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, response.DisplayText);
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

                    List<DocType> docTypes = new List<DocType>();
                    var nodes = resp.SelectSingleNode("DocType").ChildNodes;
                    for (int i = 1; i <= nodes.Count; i++)
                    {
                        DocType d = DocType.Admission;
                        Enum.TryParse(nodes[i].BaseName, out d);
                        docTypes.Add(d);
                    }
                    string DisplayText = resp.SelectSingleNode("DisplayText").Text;

                    response = new Response(id, Name, docTypes, DisplayText);
                }
            }
            return response;
        }
        public ObservableCollection<Response> GetResponses()
        {
            ObservableCollection<Response> responses = new ObservableCollection<Response>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");

            foreach (CustomXMLNode element in ResponseNodes)
            {
                string ID = element.SelectSingleNode("ID").Text;
                Response response = GetResponse(ID);

                responses.Add(response);
            }

            return responses;
        }

        public void UpdateResponse(Response response)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes ResponseNodes = customXmlDoc.SelectNodes("//Response");
            foreach (CustomXMLNode node in ResponseNodes)
            {
                if (node.SelectSingleNode("ID").Text == response.ID)
                {
                    node.SelectSingleNode("Name").Text = response.Name;
                    node.SelectSingleNode("DisplayText").Text = response.DisplayText;
                }
            }
        }

        public void ExportResponses(string path)
        {
            string xml = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(ResponseRepository.NameSpace)[1].XML;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlTextWriter writer = new XmlTextWriter(path, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);

        }

        #endregion
    }
}
