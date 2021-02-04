using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Exhibit
{
    public enum FormatNodes
    {
        FirstCite, //"Intro"
        FollowingCites, //"Numbering"
        IndexStyle, //"FirstOnly"
        IndexStart, //"DescBatesFormat"
        UniformCites, //"Parentheses"
        IdCite, //"IdCite"
        FormatCustomized,

        Intro,
        DescBatesFormat,
        Parentheses
    }
    public enum IntroOptions
    {
        Exhibit, //"Exhibit"
        Ex, //"Ex."
        Exh, //"Exh."
        Appendix, //"Appendix"
        Appx, //"Appx."
        Tab //"Tab"
    }
    public enum NumberingOptions
    {
        Numbers, //"1, 2, 3..."
        Letters, //"A, B, C..."
        RomanNumerals //"I, II, III..."
    }
    public enum FirstOnlyOptions
    {
        FirstOnly, //"In first citation only"
        AllCites, //"In all citations"
        DoNotInclude //"In no citations"
    }
    public enum DescBatesFormatOptions
    {
        Description, //"Description"
        Description_Bates, //"Description, Bates"
        Description_P_Bates_P_, //"Description (Bates)"
        _P_Description_P_, //"(Description)"
        _P_Description_Bates_P_ //"(Description, Bates)"
    }
    public enum CiteType
    {
        None,
        Exhibit, 
        LegalOrRecordCitation,
        Legal,
        Record,
        Other,
    }




    public class EnumSwitch
    {

        public IntroOptions IntroOptions_TextSwitchEnum(string TextToSwitch)
        {
            IntroOptions result;
            switch (TextToSwitch)
            {
                case "Exhibit":
                    result = IntroOptions.Exhibit;
                    break;
                case "Ex.":
                    result = IntroOptions.Ex;
                    break;
                case "Exh.":
                    result = IntroOptions.Exh;
                    break;
                case "Appendix":
                    result = IntroOptions.Appendix;
                    break;
                case "Appx.":
                    result = IntroOptions.Appx;
                    break;
                case "Tab":
                    result = IntroOptions.Tab;
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return result;
        }
        public string IntroOptions_EnumSwitchText(IntroOptions EnumToSwitch)
        {
            string result;
            switch (EnumToSwitch)
            {
                case IntroOptions.Exhibit:
                    result = "Exhibit";
                    break;
                case IntroOptions.Ex:
                    result = "Ex.";
                    break;
                case IntroOptions.Exh:
                    result = "Exh.";
                    break;
                case IntroOptions.Appendix:
                    result = "Appendix";
                    break;
                case IntroOptions.Appx:
                    result = "Appx.";
                    break;
                case IntroOptions.Tab:
                    result = "Tab";
                    break;
                default:
                    throw new Exception("Correct Node not sent to method");
            }
            return result;
        }

        public NumberingOptions NumberingOptions_TextSwitchEnum(string TextToSwitch)
        {
            NumberingOptions result;
            switch (TextToSwitch)
            {
                case "Numbers":
                    result = NumberingOptions.Numbers;
                    break;
                case "Letters":
                    result = NumberingOptions.Letters;
                    break;
                case "RomanNumerals":
                    result = NumberingOptions.RomanNumerals;
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return result;
        }
        public string NumberingOptions_EnumSwitchText(NumberingOptions EnumToSwitch)
        {
            string result;
            switch (EnumToSwitch)
            {
                case NumberingOptions.Numbers:
                    result = "Numbers";
                    break;
                case NumberingOptions.Letters:
                    result = "Letters";
                    break;
                case NumberingOptions.RomanNumerals:
                    result = "RomanNumerals";
                    break;
                default:
                    throw new Exception("Correct Node not sent to method");
            }
            return result;
        }

        public FirstOnlyOptions FirstOnlyOptions_TextSwitchEnum(string TextToSwitch)
        {
            FirstOnlyOptions result;
            switch (TextToSwitch)
            {
                case "In first citation only":
                    result = FirstOnlyOptions.FirstOnly;
                    break;
                case "In all citations":
                    result = FirstOnlyOptions.AllCites;
                    break;
                case "In no citations":
                    result = FirstOnlyOptions.DoNotInclude;
                    break;
                default:
                    throw new Exception("Correct text not sent to method");
            }
            return result;
        }
        public string FirstOnlyOptions_EnumSwitchText(FirstOnlyOptions EnumToSwitch)
        {
            string result;
            switch (EnumToSwitch)
            {
                case FirstOnlyOptions.FirstOnly:
                    result = "In first citation only";
                    break;
                case FirstOnlyOptions.AllCites:
                    result = "In all citations";
                    break;
                case FirstOnlyOptions.DoNotInclude:
                    result = "In no citations";
                    break;
                default:
                    throw new Exception("Correct Node not sent to method");
            }
            return result;
        }

        public DescBatesFormatOptions DescBatesFormatOptions_TextSwitchEnum(string TextToSwitch)
        {
            DescBatesFormatOptions result;
            switch (TextToSwitch)
            {
                case "Description":
                    result = DescBatesFormatOptions.Description;
                    break;
                case "Description, Bates":
                    result = DescBatesFormatOptions.Description_Bates;
                    break;
                case "Description (Bates)":
                    result = DescBatesFormatOptions.Description_P_Bates_P_;
                    break;
                case "(Description)":
                    result = DescBatesFormatOptions._P_Description_P_;
                    break;
                case "(Description, Bates)":
                    result = DescBatesFormatOptions._P_Description_Bates_P_;
                    break;

                //case "Description":
                //    result = DescBatesFormatOptions.Description;
                //    break;
                case "Description_Bates":
                    result = DescBatesFormatOptions.Description_Bates;
                    break;
                case "Description_P_Bates_P_":
                    result = DescBatesFormatOptions.Description_P_Bates_P_;
                    break;
                case "_P_Description_P_":
                    result = DescBatesFormatOptions._P_Description_P_;
                    break;
                case "_P_Description_Bates_P_":
                    result = DescBatesFormatOptions._P_Description_Bates_P_;
                    break;

                default:
                    throw new Exception("Correct text not sent to method");
            }
            return result;
        }
        public string DescBatesFormatOptions_EnumSwitchText(DescBatesFormatOptions EnumToSwitch)
        {
            string result;
            switch (EnumToSwitch)
            {
                case DescBatesFormatOptions.Description:
                    result = "Description";
                    break;
                case DescBatesFormatOptions.Description_Bates:
                    result = "Description, Bates";
                    break;
                case DescBatesFormatOptions.Description_P_Bates_P_:
                    result = "Description (Bates)";
                    break;
                case DescBatesFormatOptions._P_Description_P_:
                    result = "(Description)";
                    break;
                case DescBatesFormatOptions._P_Description_Bates_P_:
                    result = "(Description, Bates)";
                    break;
                default:
                    throw new Exception("Correct Node not sent to method");
            }
            return result;
        }
    }
}
