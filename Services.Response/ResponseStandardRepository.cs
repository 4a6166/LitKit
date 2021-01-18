using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools.Response
{
    public class ResponseStandardRepository
    {


        static XmlNodeList nodesResponse = LoadStandardLanguage().SelectNodes("//Response");

        #region test code
        //public string GetAllTexts()
        //{
        //    string result = string.Empty;
        //    string respondingParty = "The Associated Defenda Companies";
        //    string respondingPlural = "Plural";
        //    string propoundingParty = "Mr. Litigious";

        //    result += "Complaint" + Environment.NewLine;
        //    foreach (var t in GetTextByDocType("Complaint", respondingParty, respondingPlural, propoundingParty))
        //    { result += t + Environment.NewLine; }
        //    result += (Environment.NewLine + Environment.NewLine + "===========================================================================" +Environment.NewLine);

        //    result += "Admission" + Environment.NewLine;
        //    foreach (var t in GetTextByDocType("Admission", respondingParty, respondingPlural, propoundingParty))
        //    { result += t + Environment.NewLine; }
        //    result += (Environment.NewLine + Environment.NewLine + "===========================================================================" + Environment.NewLine);

        //    result += "Production" + Environment.NewLine;
        //    foreach (var t in GetTextByDocType("Production", respondingParty, respondingPlural, propoundingParty))
        //    { result += t + Environment.NewLine; }
        //    result += (Environment.NewLine + Environment.NewLine + "===========================================================================" + Environment.NewLine);

        //    result += "Interrogatory" + Environment.NewLine;
        //    foreach (var t in GetTextByDocType("Interrogatory", respondingParty, respondingPlural, propoundingParty))
        //    { result += t + Environment.NewLine; }
        //    result += (Environment.NewLine + Environment.NewLine + "===========================================================================" + Environment.NewLine);

        //    result += GetTextByID("2", 0);
        //    result += GetTextByID("2", 0, respondingParty, respondingPlural, propoundingParty, "Complaint");


        //    return result;
        //}
        #endregion



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

        static Dictionary<string, int> Plural = new Dictionary<string, int>()
        {
            {"False", 0 },
            {"True", 1 }
        };

        static string RequestInterrog(string docType)
        {
            if (docType == "Respond to Interrogatories")
            {
                return "Interrogatory";
            }
            else return "Request";
        }

        static string DocsInfo(string docType)
        {
            if (docType == "Interrogatory" || docType == "Admission")
            {
                return "information that is";
            }
            else return "documents that are";
        }

        static string DocsInfo1(string docType)
        {
            if (docType == "Interrogatory" || docType == "Admission")
            {
                return "information";
            }
            else return "documents";
        }

        static string paraRequest(string docType)
        {
            if (docType == "Respond to Requests for Admission")
            {
                return "Request";
            }
            else return "Paragraph";
        }

        static XmlDocument LoadStandardLanguage()
        {
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                String Root = Directory.GetCurrentDirectory();
                string path = string.Empty;

                try //For use during debug
                {
                    path = Root + @"\Services\Response\ResponseStandardLanguage.xml";

                }
                catch { }

                //try //For use during user testing
                //{
                //    string Parent = Directory.GetCurrentDirectory() + @"\..\";
                //    var Dirs = Directory.EnumerateDirectories(Parent);

                //    string Rootdll = Dirs.Where(n => n.Contains("litkit.dll")).SingleOrDefault();
                //    path = Rootdll + @"\Services\Response\ResponseStandardLanguage.xml";
                    
                //}
                //catch { }

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                return xmlDocument;
            }

            catch 
            {
                
                return null;

            }
        }

        public static ObservableCollection<ResponseStandard> GetResponses(string docType)
        {
            ObservableCollection<ResponseStandard> result = new ObservableCollection<ResponseStandard>();

            foreach (XmlNode respNode in nodesResponse)
            {
                if (respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(DocType[docType]).InnerText == "True")
                {
                    string ID = respNode.ChildNodes.Item(ResponseChild["ID"]).InnerText;
                    string Name = respNode.ChildNodes.Item(ResponseChild["Name"]).InnerText;
                    List<bool> dt = new List<bool>
                    {
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(0).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(1).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(2).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(3).InnerText),
                    };
                    List<string> Texts = new List<string>();
                    for(var i = 0; i<= respNode.ChildNodes.Item(ResponseChild["Text"]).ChildNodes.Count-1; i++)
                    {
                        Texts.Add(respNode.ChildNodes.Item(ResponseChild["Text"]).ChildNodes[i].InnerText);
                    }

                    string[,] verbs = new string[,]
                    {
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[0]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[0]?.ChildNodes[1].InnerText },
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[1]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[1]?.ChildNodes[1].InnerText },
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[2]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[2]?.ChildNodes[1].InnerText },
                    };

                    result.Add(new ResponseStandard(ID, Name, dt, Texts, verbs));
                }
            }
            return result;
        }
        

        public static ResponseStandard GetResponseByName(string Name = "Generic Response")
        {
            ResponseStandard response = null;

            foreach (XmlNode respNode in nodesResponse)
            {
                if (respNode.ChildNodes.Item(ResponseChild["Name"]).InnerText == Name)
                {
                    string ID = respNode.ChildNodes.Item(ResponseChild["ID"]).InnerText;
                    List<bool> dt = new List<bool>
                    {
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(0).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(1).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(2).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(3).InnerText),
                    };
                    List<string> Texts = new List<string>();
                    for (var i = 0; i <= respNode.ChildNodes.Item(ResponseChild["Text"]).ChildNodes.Count - 1; i++)
                    {
                        Texts.Add(respNode.ChildNodes.Item(ResponseChild["Text"]).ChildNodes[i].InnerText);
                    }

                    string[,] verbs = new string[,]
                    {
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[0]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[0]?.ChildNodes[1].InnerText },
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[1]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[1]?.ChildNodes[1].InnerText },
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[2]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[2]?.ChildNodes[1].InnerText },
                    };

                    response = new ResponseStandard(ID, Name, dt, Texts, verbs);
                }
            }

            return response;
        }

        public static ResponseStandard GetResponseByID(string ID = "20")
        {
            ResponseStandard response = null;

            foreach (XmlNode respNode in nodesResponse)
            {
                if (respNode.ChildNodes.Item(ResponseChild["ID"]).InnerText == ID)
                {
                    string Name = respNode.ChildNodes.Item(ResponseChild["Name"]).InnerText;
                    List<bool> dt = new List<bool>
                    {
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(0).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(1).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(2).InnerText),
                        bool.Parse(respNode.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(3).InnerText),
                    };
                    List<string> Texts = new List<string>();
                    for (var i = 0; i <= respNode.ChildNodes.Item(ResponseChild["Text"]).ChildNodes.Count - 1; i++)
                    {
                        Texts.Add(respNode.ChildNodes.Item(ResponseChild["Text"]).ChildNodes[i].InnerText);
                    }

                    string[,] verbs = new string[,]
                    {
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[0]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[0]?.ChildNodes[1].InnerText },
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[1]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[1]?.ChildNodes[1].InnerText },
                        { respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[2]?.ChildNodes[0].InnerText , respNode.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes[2]?.ChildNodes[1].InnerText },
                    };

                    response = new ResponseStandard(ID, Name, dt, Texts, verbs);
                }
            }

            return response;
        }


        public static ResponseStandard FillStrings(ResponseStandard response, string respondingParty, string respondingPlural, string propoundingParty, string docType)
        {
            for (var i = 0; i <= response.Texts.Count; i++)
            {
                response.Texts[i] = response.Texts[i].Replace("[Responding]", respondingParty);
                response.Texts[i] = response.Texts[i].Replace("[Propounding]", propoundingParty);
                response.Texts[i] = response.Texts[i].Replace("[Paragraph/Request]", paraRequest(docType));
                response.Texts[i] = response.Texts[i].Replace("[Request/Interrogatory]", RequestInterrog(docType));
                response.Texts[i] = response.Texts[i].Replace("[documents/information]", DocsInfo1(docType));
                response.Texts[i] = response.Texts[i].Replace("[documents that are/information that is]", DocsInfo(docType));

                response.Texts[i] = response.Texts[i].Replace("[verb1]", response.Verbs[0, Plural[respondingPlural]]);
                response.Texts[i] = response.Texts[i].Replace("[verb2]", response.Verbs[1, Plural[respondingPlural]]);
                response.Texts[i] = response.Texts[i].Replace("[verb3]", response.Verbs[2, Plural[respondingPlural]]);

            }

            return response;
        }

        public static string FillString(string id, string text, string respondingParty, string respondingPlural, string propoundingParty, string docType)
        {
            ResponseStandard response = GetResponseByID(id);

            string result = text;
            if(response != null)
            {
                result = text.Replace("[Responding]", respondingParty);
                result = result.Replace("[Propounding]", propoundingParty);
                result = result.Replace("[Paragraph/Request]", paraRequest(docType));
                result = result.Replace("[Request/Interrogatory]", RequestInterrog(docType));
                result = result.Replace("[documents/information]", DocsInfo1(docType));
                result = result.Replace("[documents that are/information that is]", DocsInfo(docType));

                result = result.Replace("[verb1]", response.Verbs[0, Plural[respondingPlural]]);
                result = result.Replace("[verb2]", response.Verbs[1, Plural[respondingPlural]]);
                result = result.Replace("[verb3]", response.Verbs[2, Plural[respondingPlural]]);
            }
            else //custom responses will create a null response
            {
                result = text.Replace("[Responding]", respondingParty);
                result = result.Replace("[Propounding]", propoundingParty);
            }

            return result;
        }



        //public string GetTextByID(string ID, int TextOption)
        //{
        //    string result = string.Empty;
        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["ID"]).InnerText == ID)
        //        {
        //            result = response.ChildNodes.Item(ResponseChild["Text"]).ChildNodes.Item(TextOption).InnerText;
        //        }
        //    }
        //    return result;
        //}

        //public string GetTextByID(string ID, int TextOption, string respondingParty, string respondingPlural, string propoundingParty, string docType)
        //{
        //    string result = string.Empty;

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["ID"]).InnerText == ID)
        //        {
        //            result = response.ChildNodes.Item(ResponseChild["Text"]).ChildNodes.Item(TextOption).InnerText;

        //            List<string> verbs = new List<string>();
        //            for(var i =0; i<= response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Count-1; i++ )
        //            {
        //                verbs.Add(response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Item(i).ChildNodes.Item(SingPlur[respondingPlural]).InnerText);
        //            }

        //            result = result.Replace("[Responding]", respondingParty);
        //            result = result.Replace("[Propounding]", propoundingParty);
        //            result = result.Replace("[Paragraph/Request]", paraRequest(docType));
        //            result = result.Replace("[Request/Interrogatory]", RequestInterrog(docType));
        //            result = result.Replace("[documents/information]", DocsInfo1(docType));
        //            result = result.Replace("[documents that are/information that is]", DocsInfo(docType));

        //            result = result.Replace("[verb1]", verbs[0]);
        //            if (verbs.Count > 1)
        //            {
        //                result = result.Replace("[verb2]", verbs[1]);
        //            }
        //            if (verbs.Count > 2)
        //            {
        //                result = result.Replace("[verb3]", verbs[2]);
        //            }


        //        }
        //    }
        //    return result;
        //}

        //public static List<string> GetTextsByID(string docType, string ID, string respondingParty = "[Responding]", string respondingPlural = null, string propoundingParty="[Propounding]")
        //{
        //    List<string> result = new List<string>();

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["ID"]).InnerText == ID)
        //        {
        //            foreach (XmlNode node in response.ChildNodes.Item(ResponseChild["Text"]).ChildNodes)
        //            {
        //                string innerText = node.InnerText;

        //                if (respondingPlural != null)
        //                {
        //                    List<string> verbs = new List<string>();
        //                    for (var i = 0; i <= response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Count - 1; i++)
        //                    {
        //                        try
        //                        {
        //                            verbs.Add(response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Item(i).ChildNodes.Item(SingPlur[respondingPlural]).InnerText);
        //                        }
        //                        catch { }
        //                    }

        //                    innerText = innerText.Replace("[Responding]", respondingParty);
        //                    innerText = innerText.Replace("[Propounding]", propoundingParty);
        //                    innerText = innerText.Replace("[Paragraph/Request]", paraRequest(docType));
        //                    innerText = innerText.Replace("[Request/Interrogatory]", RequestInterrog(docType));
        //                    innerText = innerText.Replace("[documents/information]", DocsInfo1(docType));
        //                    innerText = innerText.Replace("[documents that are/information that is]", DocsInfo(docType));

        //                    if (verbs.Count > 0)
        //                    {
        //                        innerText = innerText.Replace("[verb1]", verbs[0]);
        //                    }
        //                    if (verbs.Count > 1)
        //                    {
        //                        innerText = innerText.Replace("[verb2]", verbs[1]);
        //                    }
        //                    if (verbs.Count > 2)
        //                    {
        //                        innerText = innerText.Replace("[verb3]", verbs[2]);
        //                    }
        //                }

        //                result.Add(node.InnerText);
        //            }

        //        }
        //    }
        //    return result;
        //}

        //public static List<string> GetNameByDocType(string docType)
        //{
        //    List<string> result = new List<string>();

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(DocType[docType]).InnerText == "True")
        //        {
        //            foreach (XmlNode text in response.ChildNodes.Item(ResponseChild["Name"]).ChildNodes)
        //            {
        //                result.Add(text.InnerText);
        //            }
        //        }
        //    }


        //    return result;
        //}

        //public static string GetIDByName(string Name)
        //{
        //    string result = string.Empty;

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["Name"]).InnerText == Name)
        //        {
        //            result = response.ChildNodes.Item(ResponseChild["ID"]).InnerText;
        //        }
        //    }
        //    return result;
        //}

        //public static List<string> GetTextByDocType(string docType)
        //{
        //    List<string> result = new List<string>();

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(DocType[docType]).InnerText == "True")
        //        {
        //            foreach (XmlNode text in response.ChildNodes.Item(ResponseChild["Text"]).ChildNodes)
        //            {
        //                result.Add(text.InnerText);
        //            }
        //        }
        //    }
        //    return result;
        //}

        //public static List<string> GetNamesByDocType(string docType)
        //{
        //    List<string> result = new List<string>();

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(DocType[docType]).InnerText == "True")
        //        {
        //            result.Add(response.ChildNodes.Item(ResponseChild["Name"]).InnerText);
        //        }
        //    }
        //    return result;
        //}


        //public static List<string> GetTextByDocType(string docType, string respondingParty, string respondingPlural, string propoundingParty)
        //{
        //    List<string> result = new List<string>();

        //    foreach (XmlNode response in nodesResponse)
        //    {
        //        if (response.ChildNodes.Item(ResponseChild["DocType"]).ChildNodes.Item(DocType[docType]).InnerText == "True")
        //        {
        //            List<string> verbs = new List<string>();
        //            for (var i = 0; i <= response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Count-1; i++)
        //            {
        //                verbs.Add(response.ChildNodes.Item(ResponseChild["Verbs"]).ChildNodes.Item(i).ChildNodes.Item(SingPlur[respondingPlural]).InnerText);
        //            }

        //            foreach (XmlNode text in response.ChildNodes.Item(ResponseChild["Text"]).ChildNodes)
        //            {
        //                result.Add(text.InnerText);

        //                for(var i = 0; i<= result.Count-1; i++)
        //                {
        //                    result[i] = result[i].Replace("[Responding]", respondingParty);
        //                    result[i] = result[i].Replace("[Propounding]", propoundingParty);
        //                    result[i] = result[i].Replace("[Paragraph/Request]", paraRequest(docType));
        //                    result[i] = result[i].Replace("[Request/Interrogatory]", RequestInterrog(docType));
        //                    result[i] = result[i].Replace("[documents/information]", DocsInfo1(docType));
        //                    result[i] = result[i].Replace("[documents that are/information that is]", DocsInfo(docType));

        //                    result[i] = result[i].Replace("[verb1]", verbs[0]);
        //                    if (verbs.Count > 1)
        //                    {
        //                        result[i] = result[i].Replace("[verb2]", verbs[1]);
        //                    }
        //                    if (verbs.Count > 2)
        //                    {
        //                        result[i] = result[i].Replace("[verb3]", verbs[2]);
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}
    }
}
