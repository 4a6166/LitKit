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


            // Objections
            {
                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection1"),
                    new XElement("Name", "Cumulative"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as cumulative of prior discovery requests."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection2"),
                    new XElement("Name", "Equally Available"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as requesting [documents/information] equally available to [Propounding Party/Parties]."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection3"),
                    new XElement("Name", "Overbroad"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as overbroad and burdensome."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection4"),
                    new XElement("Name", "Premature Contention"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as premature to the extent it requires disclosure of contentions. Discovery is ongoing and [Responding Party/Parties] will supplement this Response at the appropriate time."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection5"),
                    new XElement("Name", "Premature Expert"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as premature to the extent it requires disclosure of expert testimony. Discovery is ongoing and [Responding Party/Parties] will supplement this Response at the appropriate time."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection6"),
                    new XElement("Name", "Privilege"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] to the extent it requests the disclosure of attorney work product or [documents/information] protected by the attorney-client privilege."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection7"),
                    new XElement("Name", "Proportionality"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] to the extent it seeks [documents/information] that is not relevant to any party's claim or defense, nor proportional to the needs of the case."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection8"),
                    new XElement("Name", "Publicly Available"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as requesting [documents/information] that are publicly available."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection9"),
                    new XElement("Name", "Repetitive"),
                    new XElement("Text", "[Responding Party/Parties] [verb] to this [Request/Interrogatory] as repetitive of prior discovery requests and directs [Propounding Party/Parties] to ...."),
                    new XElement("Verb",
                        new XElement("Singular", "objects"),
                        new XElement("Plural", "object")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection10"),
                    new XElement("Name", "Response"),
                    new XElement("Text", "Subject to and without waiving the foregoing objections, [Responding Party/Parties] [verb] ..."),
                    new XElement("Verb",
                        new XElement("Singular", "states"),
                        new XElement("Plural", "state")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Objection11"),
                    new XElement("Name", "Rule 33(d)"),
                    new XElement("Text", "As permitted by Rule 33(d) of the Federal Rules of Civil Procedure, [Responding Party/Parties] [verb] [Propounding Party/Parties] to the following documents:"),
                    new XElement("Verb",
                        new XElement("Singular", "directs"),
                        new XElement("Plural", "direct")
                    )));
            }

            // Responses -- May have more than one "Text" or "Verb"
            {
                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response1"),
                    new XElement("Name", "Admit"),
                    new XElement("Text", "Admitted."),
                        new XElement("Text2", "[Responding Party/Parties] [verb] the allegations contained in Paragraph [paragraph]."),
                        new XElement("Text3", "[Responding Party/Parties] [verb] the allegations contained in this Paragraph."),
                        new XElement("Text4", "Paragraph [paragraph] is admitted."),
                    new XElement("Verb",
                        new XElement("Singular", "admits"),
                        new XElement("Plural", "admit")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response2"),
                    new XElement("Name", "Deny"),
                    new XElement("Text", "Denied."),
                        new XElement("Text2", "[Responding Party/Parties] [verb] the allegations contained in Paragraph [paragraph]."),
                        new XElement("Text3", "[Responding Party/Parties] [verb] the allegations contained in this Paragraph."),
                        new XElement("Text4", "Paragraph [paragraph] is denied."),
                    new XElement("Verb",
                        new XElement("Singular", "denies"),
                        new XElement("Plural", "deny")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response3"),
                    new XElement("Name", "Deny Remaining"),
                    new XElement("Text", "[Responding Party/Parties] [verb] the remainder of the allegations contained in Paragraph [paragraph]."),
                        new XElement("Text2", "[Responding Party/Parties] [verb] the remainder of the allegations contained in this Paragraph."),
                        new XElement("Text3", "The remaining allegations in Paragraph [paragraph] are denied."),
                    new XElement("Verb",
                        new XElement("Singular", "denies"),
                        new XElement("Plural", "deny")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response4"),
                    new XElement("Name", "Incorporate Prior"),
                    new XElement("Text","[Responding Party/Parties] hereby [verb] the responses from the preceding paragraphs."),
                    new XElement("Verb",
                        new XElement("Singular", "incorporates"),
                        new XElement("Plural", "incorporate")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response5"),
                    new XElement("Name", "Lack Knowledge"),
                    new XElement("Text","[Responding Party/Parties] [verb] knowledge or information sufficient to form a belief about the truth of the allegations contained in this Paragraph."),
                        new XElement("Text2", "[Responding Party/Parties] [verb] knowledge or information sufficient to form a belief about the truth of the allegations contained in Paragraph [paragraph]."),
                    new XElement("Verb",
                        new XElement("Singular", "lacks"),
                        new XElement("Plural", "lack")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response6"),
                    new XElement("Name", "Legal Allegation"),
                    new XElement("Text", "Paragraph [paragraph] contains legal allegations to which no answer is necessary."),
                        new XElement("2", "This Paragraph contains legal allegations to which no answer is necessary"),
                    new XElement("Verb",
                        new XElement("Singular", "contains"),
                        new XElement("Plural", "contain")
                    )));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response7"),
                    new XElement("Name", "Quotes Document"),
                    new XElement("Text", "[Responding Party/Parties] [verb] that Paragraph [paragraph] accurately quotes a document, but [verb2] any characterization of the document and [verb3] that the document speaks for itself."),
                    new XElement("Verb",
                        new XElement("Singular", "admits"),
                        new XElement("Plural", "admit")
                    ),
                    new XElement("Verb2",
                        new XElement("Singular", "denies"),
                        new XElement("Plural", "deny")
                    ),
                    new XElement("Verb3",
                        new XElement("Singular", "states"),
                        new XElement("Plural", "state")
                    )
                    ));

                xDocument.Element(rootName).Add(new XElement("Answer",
                    new XElement("ID", "Response8"),
                    new XElement("Name", "Response"),
                    new XElement("Text","Subject to and without waiving the foregoing objections, [Responding Party/Parties] [verb] ..."),
                    new XElement("Verb",
                        new XElement("Singular", "states"),
                        new XElement("Plural", "state")
                    )));
            }


            return xDocument.ToString();

        }
    }
}
