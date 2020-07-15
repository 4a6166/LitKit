using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Answers
{
    public static class AnsCustomXMLFrame
    {
        public static string FrameCustomXMLDoc(XName rootName)
        {

            XDocument xDocument =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(rootName, "")
                );

            xDocument.Element(rootName).Add(new XElement("Parties",
                    new XElement("Responding", "TBD Responding"),
                    new XElement("Propounding", "TBD Propounding"),
                    new XElement("RespondSingular", "True"),
                    new XElement("PropoudSingular", "True")
                    ));

            ///TODO: add the text for each of the preset answers ////////////////////////////////////////////////////////////////////

            // Singular Objections
            {
                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection1"),
                    new XElement("Name", "Cumulative"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection2"),
                    new XElement("Name", "Equally Available"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection3"),
                    new XElement("Name", "Overbroad"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection4"),
                    new XElement("Name", "Premature Contention"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection5"),
                    new XElement("Name", "Premature Exper"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection6"),
                    new XElement("Name", "Privilege"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection7"),
                    new XElement("Name", "Proportionality"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection8"),
                    new XElement("Name", "Publicly Available"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection9"),
                    new XElement("Name", "Repetitive"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection10"),
                    new XElement("Name", "Response"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection11"),
                    new XElement("Name", "Rule33(d)"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));
            }

            // Plural Objections
            {
                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection1"),
                    new XElement("Name", "Cumulative"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection2"),
                    new XElement("Name", "Equally Available"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection3"),
                    new XElement("Name", "Overbroad"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection4"),
                    new XElement("Name", "Premature Contention"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection5"),
                    new XElement("Name", "Premature Exper"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection6"),
                    new XElement("Name", "Privilege"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection7"),
                    new XElement("Name", "Proportionality"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection8"),
                    new XElement("Name", "Publicly Available"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection9"),
                    new XElement("Name", "Repetitive"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection10"),
                    new XElement("Name", "Response"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection11"),
                    new XElement("Name", "Rule33(d)"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));
            }

            // Singular Responses
            {
                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response1"),
                    new XElement("Name", "Admit"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response2"),
                    new XElement("Name", "Deny"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response3"),
                    new XElement("Name", "Deny Remaining"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response4"),
                    new XElement("Name", "Incorporate Prior"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response5"),
                    new XElement("Name", "Lack Knowledge"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response6"),
                    new XElement("Name", "Legal Allegation"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response7"),
                    new XElement("Name", "Quotes Document"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response8"),
                    new XElement("Name", "Response"),
                    new XElement("Text", ""),
                    new XElement("Singular", "True")
                    ));
            }

            // Plural Responses
            {
                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response1"),
                    new XElement("Name", "Admit"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response2"),
                    new XElement("Name", "Deny"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response3"),
                    new XElement("Name", "Deny Remaining"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response4"),
                    new XElement("Name", "Incorporate Prior"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response5"),
                    new XElement("Name", "Lack Knowledge"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response6"),
                    new XElement("Name", "Legal Allegation"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response7"),
                    new XElement("Name", "Quotes Document"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response8"),
                    new XElement("Name", "Response"),
                    new XElement("Text", ""),
                    new XElement("Singular", "False")
                    ));
            }
                return xDocument.ToString();

        }
    }
}
