using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public static class ExpressionsRepository
    {
        public static bool ReadRepository(string path, List<string> Expressions)
        {
            StreamReader reader = new StreamReader(path);

            try
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith(@"\\") && !String.IsNullOrWhiteSpace(line))
                    {
                        Expressions.Add(line);
                    }
                }

                reader.Close();
                return true;
            }
            catch 
            { 
                reader.Close();
                return false; 
            }

        }

    }
}
