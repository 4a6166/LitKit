using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Services.Response
{
    public class Response
    {
        public Response(string Name, List<bool> DocTypes, string DisplayText)
        {
            this.ID = Guid.NewGuid().ToString();

            this.Name = Name;
            this.DocTypes = DocTypes;
            this.DisplayText = DisplayText;
        }

        public Response(string ID, Word.Application _app)
        {

            var response = new ResponseRespository(_app).GetResponse(ID);
            this.ID = ID;
            this.Name = response.Name;
            this.DisplayText = response.DisplayText;
            this.DocTypes = response.DocTypes;

        }

        public string ID { get; private set; }
        public string Name { get; set; }

        public List<bool> DocTypes { get; set; }

        public string DisplayText { get; set; }


    }
}
