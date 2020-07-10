//----<XML NameSpaces>----
using DocumentFormat.OpenXml.Packaging;
using System.IO;
//---< Word Adddin >-----
using Application = Microsoft.Office.Interop.Word.Application;
//---</ Word Addin >-----

namespace Services.Exhibit
{
    class XmlClass
    {
        #region constructor and readonly allowing access to ThisAddIn
        public XmlClass(Application _app)
        {
            this._app = _app;
        }

        // ADD: using Application = Microsoft.Office.Interop.Word.Application; 
        private readonly Microsoft.Office.Interop.Word.Application _app;
        #endregion

        public static void WriteCustomXML(string path)
        {
            StreamWriter doc = new StreamWriter(path);

            var ID = 1;
            string longCite = "Test Long Cite";
            string shortCite = "Test Short Cite";

            string quotes = "\u0022";

            doc.WriteLine("<?xml version=" +
                quotes + "1.0" + quotes +
                "?><catalog xmlns=" +
                quotes + "Sample XML - Exhibits" + quotes +
                ">");

            doc.WriteLine(@"<Exhibit id=" + quotes + ID.ToString() + quotes + ">");
            doc.WriteLine(@"<LongCite>" + longCite + @"</LongCite>");
            doc.WriteLine(@"<ShortCite>" + shortCite + @"</ShortCite>");
            doc.WriteLine(@"</Exhibit>");

            ID++;

            doc.WriteLine(@"<Exhibit id=" + quotes + ID.ToString() + quotes + ">");
            doc.WriteLine(@"<LongCite>" + longCite + @"</LongCite>");
            doc.WriteLine(@"<ShortCite>" + shortCite + @"</ShortCite>");
            doc.WriteLine(@"</Exhibit>");

            doc.Close();
        }

        /// <summary>
        /// Currently not working due to file path issues
        /// </summary>
        public void AddXmlToDoc()
        {
            /* In order to reference through XML pane (developer tab)
             * Needs to save main XML file under name "item1" to  *.docx\customXml\
             * Needs to save file named "itemProps1.xml" to *.docx\customXml\
                    Example contents: 
                    <?xml version="1.0" encoding="UTF-8" standalone="no"?>
                    <ds:datastoreItem ds:itemID="{61854F35-310D-448A-A52B-42970F26969D}" xmlns:ds="http://schemas.openxmlformats.org/officeDocument/2006/customXml"><ds:schemaRefs><ds:schemaRef ds:uri="Sample XML - Books"/></ds:schemaRefs></ds:datastoreItem>
             * Needs to save file named "item1.xml.rels" to *.docx\customXml\_rels\
                    Example contents:
                    <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                    <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps" Target="itemProps2.xml"/></Relationships>
             */

            string quotes = "\u0022";

            string pathMain = _app.ActiveDocument.Path + @"\customXml\" + "item1.xml";
            string pathProp = _app.ActiveDocument.Path + @"\customXml\" + "item1Props1.xml";
            string pathRel = pathMain + @"\_rels\" + "item1.xml.rels";

            WriteCustomXML(pathMain);

            StreamWriter prop = new StreamWriter(pathProp);
            prop.WriteLine(@"<? xml version = " +
                quotes + "1.0" + quotes +
                " encoding = " +
                quotes + "UTF-8" + quotes +
                " standalone = " +
                quotes + "no" + quotes +
                "?>");
            prop.WriteLine(@"<ds:datastoreItem ds:itemID=" +
                quotes + "{ 61854F35 - 310D - 448A - A52B - 42970F26969D}" + quotes +
                " xmlns:ds=" +
                quotes + "http://schemas.openxmlformats.org/officeDocument/2006/customXml" + quotes +
                "><ds:schemaRefs><ds:schemaRef ds:uri=" +
                quotes + "Sample XML - Exhibits" + quotes +
                "/></ds:schemaRefs></ds:datastoreItem>");

            prop.Close();

            StreamWriter rel = new StreamWriter(pathRel);
            rel.WriteLine(@"<?xml version=" +
                quotes + "1.0" + quotes +
                " encoding=" +
                quotes + "UTF - 8" + quotes +
                " standalone=" +
                quotes + "yes" + quotes +
                "?>");
            rel.WriteLine(@"<Relationships xmlns=" +
                quotes + "http://schemas.openxmlformats.org/package/2006/relationships" + quotes +
                "><Relationship Id=" +
                quotes + "rId1" + quotes +
                "Type=" +
                quotes + "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps" + quotes +
                " Target=" +
                quotes + "itemProps1.xml" + quotes +
                "/></Relationships>");

            rel.Close();


        }

        /// <summary>
        /// Creates a Doc from an XML file using a template and a console app
        /// From: https://stackoverflow.com/questions/50117531/generate-a-word-document-docx-using-data-from-an-xml-file-convert-xml-to-a-w
        /// </summary>
        public void GenerateDocument()
        {
            string rootPath = @"C:\Temp";
            string xmlDataFile = rootPath + @"\MyNewData.xml";
            string templateDocument = rootPath + @"\MyTemplate.docx";
            string outputDocument = rootPath + @"\MyGeneratedDocument.docx";

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(templateDocument, true))
            {
                //get the main part of the document which contains CustomXMLParts
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                //delete all CustomXMLParts in the document. If needed only specific CustomXMLParts can be deleted using the CustomXmlParts IEnumerable
                mainPart.DeleteParts<CustomXmlPart>(mainPart.CustomXmlParts);

                //add new CustomXMLPart with data from new XML file
                CustomXmlPart myXmlPart = mainPart.AddCustomXmlPart(CustomXmlPartType.CustomXml);
                using (FileStream stream = new FileStream(xmlDataFile, FileMode.Open))
                {
                    myXmlPart.FeedData(stream);
                }
            }
        }
    }
}
