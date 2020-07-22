using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Services.Response
{
    public class ResponseStandardRepository
    {
        static XmlNodeList nodesResponse = LoadStandardLanguage().SelectNodes("//Response");

        public string GetTexts()
        {
            string result = string.Empty;
            string respondingParty = "The Associated Defenda Companies";
            string respondingPlural = "Plural";
            string propoundingParty = "Mr. Litigious";

            result += GetTextOptions(respondingParty, respondingPlural, propoundingParty, "Complaint");
            result += (Environment.NewLine + Environment.NewLine + "===========================================================================" +Environment.NewLine);

            result += GetTextOptions(respondingParty, respondingPlural, propoundingParty, "Admission");
            result += (Environment.NewLine + Environment.NewLine + "===========================================================================" + Environment.NewLine);

            result += GetTextOptions(respondingParty, respondingPlural, propoundingParty, "Production");
            result += (Environment.NewLine + Environment.NewLine + "===========================================================================" + Environment.NewLine);

            result += GetTextOptions(respondingParty, respondingPlural, propoundingParty, "Interrogatory");
            return result;
        }

        static string GetTextOptions(string respondingParty, string respondingPlural, string propoundingParty, string docType)
        {
            int n = 0;
            string resultText = string.Empty;
            string texta = string.Empty;
            foreach (XmlNode response in nodesResponse)
            {
                if (response.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(DocType[docType]).InnerText == "True")
                {
                    resultText += Environment.NewLine + "ID: " + response.ChildNodes.Item(ResponseChild["ID"]).InnerText + Environment.NewLine;
                    resultText += "Name: " + response.ChildNodes.Item(ResponseChild["Name"]).InnerText + Environment.NewLine;

                    string verb1 = response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Item(0).ChildNodes.Item(SingPlur[respondingPlural]).InnerText;

                    string verb2 = string.Empty;
                    string verb3 = string.Empty;
                    if (response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Count > 1)
                    {
                        verb2 = response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Item(1).ChildNodes.Item(SingPlur[respondingPlural]).InnerText;
                        verb3 = response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Item(2).ChildNodes.Item(SingPlur[respondingPlural]).InnerText;
                    }

                    int i = 1;
                    foreach (XmlNode text in response.ChildNodes.Item(ResponseChild["Text"]).ChildNodes)
                    {
                        string filledText = text.InnerText;
                        filledText = filledText.Replace("[Responding]", respondingParty);
                        filledText = filledText.Replace("[Propounding]", propoundingParty);
                        filledText = filledText.Replace("[Paragraph/Request]", paraRequest(docType));
                        filledText = filledText.Replace("[Request/Interrogatory]", RequestInterrog(docType));
                        filledText = filledText.Replace("[documents/information]", DocsInfo1(docType));
                        filledText = filledText.Replace("[documents that are/information that is]", DocsInfo(docType));

                        filledText = filledText.Replace("[verb1]", verb1);
                        filledText = filledText.Replace("[verb2]", verb2);
                        filledText = filledText.Replace("[verb3]", verb3);


                        resultText += $"Text Option {i}: " + filledText + Environment.NewLine;
                        i++;
                    }
                    n++;
                }
            }
            texta += ($"Number of Responses linked to {docType}: " + n.ToString());
            return texta + resultText;
        }

        static Dictionary<string, int> ResponseChild = new Dictionary<string, int>()
        {
            {"ID", 0 },
            {"Name", 1 },
            {"DocType", 2 },
            {"Text", 3 },
            {"Verbs", 4 }
        };

        static Dictionary<string, int> DocType = new Dictionary<string, int>()
        {
            { "Complaint", 0 },
            { "Admission", 1 },
            { "Production", 2 },
            { "Interrogatory",3 }
        };

        static Dictionary<string, int> SingPlur = new Dictionary<string, int>()
        {
            {"Singular", 0 },
            {"Plural", 1 }
        };

        static string RequestInterrog(string docType)
        {
            if (docType == "Interrogatory")
            {
                return "Interrogatory";
            }
            else return "Request";
        }

        static string DocsInfo(string docType)
        {
            if (docType == "Interrogatory")
            {
                return "information that is";
            }
            else return "documents that are";
        }

        static string DocsInfo1(string docType)
        {
            if (docType == "Interrogatory")
            {
                return "information";
            }
            else return "documents";
        }


        static string paraRequest(string docType)
        {
            if (docType == "Admitted")
            {
                return "Request";
            }
            else return "Paragraph";
        }


        static XmlDocument LoadStandardLanguage()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            String Root = Directory.GetCurrentDirectory();
            string path = Root + @"\Response\ResponseStandardLanguage.xml";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            return xmlDocument;
        }

    }
}
