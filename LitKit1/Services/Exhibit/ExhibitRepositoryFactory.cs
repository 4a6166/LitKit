using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Services.Exhibit
{
    public class ExhibitRepositoryFactory
    {


        public static IExhibitRepository GetRepository(string repositoryType, Application _app)
        {
            ExhibitRepository repository;

            switch (repositoryType)
            {
                case "XML": 
                    repository = new ExhibitRepository(_app);
                    break;
                default:
                    throw new ArgumentException("Invalid repository type");
            }

            return repository;

        }
    }
}
