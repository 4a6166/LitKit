using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;

namespace Services.Answers
{
    public class AnsRespository
    {
        //***********NO FACTORY OR INTERFACE YET -> plan is to refactor to improve performance following user testing.
        public AnsRespository(Application _app)
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

            string docString = /*@"<?xml version="+quotes+"1.0" + quotes + " encoding=" + quotes + "UTF - 8" + quotes + " standalone =" + quotes + "yes" + quotes + " ?>" +*/
                AnsCustomXMLFrame.FrameCustomXMLDoc(rootName);

            _app.ActiveDocument.CustomXMLParts.Add(docString);

        }
        #endregion


        private readonly Application _app;

        static string NameSpace = "Prelimine Litkit Answers";
        static XNamespace name = NameSpace;
        static XName rootName = name + "Answers";




        public void AddAnswer(string Name, string Text, bool Singular)
        {
            Answer newAnswer = new Answer(Name, Text, Singular);

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNode AnswersNode = customXmlDoc.SelectSingleNode("//Answer").ParentNode;
            customXmlDoc.AddNode(AnswersNode, "Answer", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, "");

            CustomXMLNodes AnsNodes = customXmlDoc.SelectNodes("//Answer");
            CustomXMLNode AnsNode = AnsNodes[AnsNodes.Count];
            customXmlDoc.AddNode(AnsNode, "ID", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newAnswer.ID);
            customXmlDoc.AddNode(AnsNode, "Name", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newAnswer.Name);
            customXmlDoc.AddNode(AnsNode, "Text", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newAnswer.Text);
            customXmlDoc.AddNode(AnsNode, "Singular", "", null, MsoCustomXMLNodeType.msoCustomXMLNodeElement, newAnswer.Singular.ToString());
        }

        public void DeleteAnswer(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];

            CustomXMLNodes AnsNodes = customXmlDoc.SelectNodes("//Answer");
            foreach (CustomXMLNode ans in AnsNodes)
            {
                if (ans.SelectSingleNode("ID").Text == id)
                {
                    ans.Delete();
                }
            }

        }

        public Answer GetAnswer(string id)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            id = id.Split('|')[0];

            List<CustomXMLNode> nodesList = new List<CustomXMLNode>();
            CustomXMLNodes ansNodes = customXmlDoc.SelectNodes("//Answer");
            foreach (CustomXMLNode ans in ansNodes)
            {
                if (ans.SelectSingleNode("ID").Text == id)
                {
                    nodesList.Add(ans);
                }
            }
            Answer answer = new Answer(nodesList.First().SelectSingleNode("ID").Text);
            answer.Name = nodesList.First().SelectSingleNode("Name").Text;
            answer.Text = nodesList.First().SelectSingleNode("Text").Text;
            if (nodesList.First().SelectSingleNode("Singular").Text == "True")
            {
                answer.Singular = true;
                //TODO: double check that bool.ToString() results in "True" or "False"
            }
            else answer.Singular = false;

            return answer;
        }

        public IEnumerable<Answer> GetAnswers()
        {

            List<Answer> answers = new List<Answer>();

            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes ansNodes = customXmlDoc.SelectNodes("//Answer");

            foreach (CustomXMLNode element in ansNodes)
            {
                string ID = element.SelectSingleNode("ID").Text;
                Answer answer = new Answer(ID);
                answer.Name = element.SelectSingleNode("Name").Text;
                answer.Text = element.SelectSingleNode("Text").Text;
                if (element.SelectSingleNode("Singular").Text == "True")
                {
                    answer.Singular = true;
                    //TODO: double check that bool.ToString() results in "True" or "False"
                }
                else answer.Singular = false;

                answers.Add(answer);
            }

            return answers.AsEnumerable();
        }

        public void UpdateAnswer(string id, string Name, string Text, string Singular)
        {
            var customXmlDoc = _app.ActiveDocument.CustomXMLParts.SelectByNamespace(NameSpace)[1];
            CustomXMLNodes AnsNodes = customXmlDoc.SelectNodes("//Answer");
            foreach (CustomXMLNode ans in AnsNodes)
            {
                if (ans.SelectSingleNode("ID").Text == id)
                {
                    ans.SelectSingleNode("Name").Text = Name;
                    ans.SelectSingleNode("Text").Text = Text;
                    ans.SelectSingleNode("Singular").Text = Singular;
                }
            }
        }
    }
}
