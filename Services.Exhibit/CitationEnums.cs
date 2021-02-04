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
        IntroLong = 1,
        IntroShort = 2,
        Long = 4,
        Short = 8,
        IndexStyle = 16,
        IndexStart = 32,
        Parentheses = 64,
        IdCite = 128,
        IntroBold = 256
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
        INTROLONG = 1,
        INTROSHORT = 2,

        INDEX = 4,
        DESC = 8,
        OTHERID = 16,
        PIN = 32,

        FREETEXT = 64,

        LPARENS = 128,
        RPARENS = 256,
        COMMA = 512,
    }

}
