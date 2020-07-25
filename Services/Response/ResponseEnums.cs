using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Response
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
        Complaint,
        Admission,
        Production,
        Interrogatory
    }

    public enum StandardLanguageOptions
    {
        DocType,
        Text,
        Verbs
    }
}
