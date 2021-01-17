using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Response
{

    public enum DocPropsNode
    {
        Responding,
        RespondingPlural,
        Propounding,
        DocType
    }

    public enum ResponseDocs
    {
        AnswerToComplaint,
        RequestForAdmission,
        RequestForDocumentProduction,
        Interrogatory
    }

    public enum DocType
    {
        [Description("Answer a Complaint")]
        Complaint,

        [Description("Respond to Requests for Admission")]
        Admission,

        [Description("Respond to Requests for Production of Documents")]
        Production,

        [Description("Respond to Interrogatories")]
        Interrogatory
    }

    public enum StandardLanguageOptions
    {
        DocType,
        Text,
        Verbs
    }
}
