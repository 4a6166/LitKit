using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Citation
{
    [Flags]
    public enum CiteType
    {
        //None = 0,
        Exhibit = 1,
        Legal = 2,
        Record = 4,
        Other = 8,

        //All = Exhibit | Legal | Record | Other,
        //Outside = Legal | Record | Other,
    }

    [Flags]
    public enum ExhibitIndexStyle
    {
        Empty = 0,
        Numbers = 1,
        Letters = 2,
        Roman = 4,
    }

    [Flags]
    public enum FormatNode
    {
        Intro = 1,
        Long = 2,
        Short = 4,
        IndexStyle = 8,
        IndexStart = 16,
        Parentheses = 32,
        IdCite = 64
    }

    [Flags]
    public enum CitePlacementType
    {
        None = 0,
        Long = 1,
        Short = 2,
        Id = 4,
    }

    [Flags]
    public enum CiteFormatPieceType
    {
        INTRO = 1,
        INDEX = 2,
        DESC = 4,
        OTHERID = 8,
        PIN = 16,

        FREETEXT = 32,

        LPARENS = 64,
        RPARENS = 128,
        COMMA = 256,
    }

}
