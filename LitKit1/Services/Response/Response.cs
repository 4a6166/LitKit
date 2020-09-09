﻿using System.Collections.Generic;

namespace Services.Response
{
    public class Response
    {
        public Response(string ID, string Name, List<bool> DocTypes, string DisplayText)
        {
            this.ID = ID;
            this.Name = Name;
            this.DocTypes = DocTypes;
            this.DisplayText = DisplayText;
        }


        public string ID { get; private set; }
        public string Name { get; set; }

        public List<bool> DocTypes { get; set; }

        public string DisplayText { get; set; }


    }
}