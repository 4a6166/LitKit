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
        [Description("Answer to a Complaint")]
        Complaint,

        [Description("Response to Requests for Admission")]
        Admission,

        [Description("Response to Requests for Production of Documents")]
        Production,

        [Description("Response to Interrogatories")]
        Interrogatory
    }

    public enum StandardLanguageOptions
    {
        DocType,
        Text,
        Verbs
    }
}
