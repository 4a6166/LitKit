using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tools.Citation.Format
{
    public class FormatBlock
    {
        public string label { get; set; }
        public string blockType { get; set; }
        public /*System.Windows.Media.Brush*/ string color { get; set; }
        public List<string> contextMenuItems { get; set; }
        public string FormatString { get; set; }

        public FormatBlock()
        {

        }

        /// <summary>
        /// Feeds from ExhibitFormat, where the FormatString is the text inside double curly braces {{ }}
        /// </summary>
        /// <param name="FormatString">This is the text inside the double curly braces. The braces are not included.</param>
        /// <param name="Formatting">Needed to display the Intro and Index label text changes</param>
        public FormatBlock(string FormatString, CiteFormatting Formatting)  //{INTRO}{INDEX}{,}{DESC}{(}{PIN}{)}{Some Free Text}{Other Free Text}
        {
            switch (FormatString)
            {
                case "INTRO":
                    this.FormatString = FormatString;
                    blockType = "Intro";
                    label = Formatting.ExhibitIntro;
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Exhibit",
                        "Ex.",
                        "Exh.",
                        "Appendix",
                        "Appx.",
                        "Tab",
                        "Remove",
                    };
                    break;

                case "INDEX":
                    this.FormatString = FormatString;
                    blockType = "Index";
                    switch(Formatting.ExhibitIndexStyle)
                        {
                        case ExhibitIndexStyle.Numbers:
                            label = "#";
                            break;
                        case ExhibitIndexStyle.Letters:
                            label = "A";
                            break;
                        case ExhibitIndexStyle.Roman:
                            label = "IV";
                            break;
                        default:
                            throw new Exception("Exhibit Index Style not found");
                    }
                    
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Numeric",
                        "Alphabetic",
                        "Roman Numeral",
                        "Remove",
                    };
                    break;

                case "DESC":
                    this.FormatString = FormatString;
                    blockType = "Description";
                    label = "Description";
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Remove",
                    };
                    break;

                case "PIN":
                    this.FormatString = FormatString;
                    blockType = "PIN";
                    label = "PIN";
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Do not include a placeholder for Pincites",
                    };
                    break;

                case ",":
                    this.FormatString = FormatString;
                    blockType = "Comma";
                    label = ",";
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Remove",
                    };
                    break;

                case "(":
                    this.FormatString = FormatString;
                    blockType = "LeftParens";
                    label = "(";
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Remove",
                    };
                    break;

                case ")":
                    this.FormatString = FormatString;
                    blockType = "RightParens";
                    label = ")";
                    color = "#0000ff";
                    contextMenuItems = new List<string>()
                    {
                        "Remove",
                    };
                    break;

                default:
                    this.FormatString = FormatString;
                    blockType = "Free Text";
                    label = FormatString;
                    color = "#0000ff";  ///////////////////////////////TODO: change these colors so they are all different
                    contextMenuItems = new List<string>()
                    {
                        "Edit",
                        "Remove",
                    };
                    break;

            }
        }
    }
}
