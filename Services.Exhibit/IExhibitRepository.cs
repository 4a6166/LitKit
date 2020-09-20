using System.Collections.Generic;
using static Tools.Exhibit.ExhibitRepository;

namespace Tools.Exhibit
{
    public interface IExhibitRepository  //CRUD interface setup
    {
        void AddExhibit(string Description, string BatesNumber); //Create

        IEnumerable<Exhibit> GetExhibits(); //Read

        Exhibit GetExhibit(string id); //Read

        void UpdateExhibit(string id, string Description, string BatesNumber); //Update

        void DeleteExhibit(string id); //Delete

        string GetFormatting(FormatNodes node);

        void UpdateFormatting(string FirstCite, string FollowingCites, string IndexStyle, string IndexStart, bool UniformCites, bool IdCite, bool FormatCustomized);

    }
}
