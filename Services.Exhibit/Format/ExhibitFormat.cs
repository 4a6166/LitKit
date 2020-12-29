using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.Citation.Format
{
    public class ExhibitFormat
    {
        private List<FormatBlock> longCiteFormatBlocks;

        public List<FormatBlock> LongCiteFormatBlocks
        {
            get { return longCiteFormatBlocks; }
            set
            {
                //property changed event
                longCiteFormatBlocks = value;
            }
        }

        private List<FormatBlock> shortCiteFormatBlocks;

        public List<FormatBlock> ShortCiteFormatBlocks
        {
            get { return shortCiteFormatBlocks; }
            set
            {
                //property changed event
                shortCiteFormatBlocks = value;
            }
        }

        public CitationRepository repository { get; private set; }

        public ExhibitFormat(CitationRepository repository)
        {
            this.repository = repository;
            LoadFormatBlocks();

        }

        private void LoadFormatBlocks()
        {
            //TODO: set up blocks from the repository
            LoadTestBlock();
        }

        private void LoadTestBlock()
        {
            longCiteFormatBlocks = new List<FormatBlock>()
            {
                new FormatBlock()
                {
                    blockType="Intro", color="#0000ff", label="Exhibit", FormatString="{INTRO}",
                    contextMenuItems= new List<string>()
                    {
                        "Exhibit",
                        "Ex.",
                        "Exh.",
                        "Appendix",
                        "Appx.",
                        "Tab",
                        "Remove",
                    }

                },

                new FormatBlock()
                {
                    blockType="Index", color="#008000", label="#", FormatString="{INDEX}",
                    contextMenuItems= new List<string>()
                    {
                        "Numeric",
                        "Alphabetic",
                        "Roman Numeral",
                        "Remove",
                    }
                },

                new FormatBlock()
                {
                    blockType="Description", color="#FFFF00", label="Description", FormatString="{DESC}",
                    contextMenuItems= new List<string>
                    {
                        "Remove",
                    }
                },

            };

            shortCiteFormatBlocks = new List<FormatBlock>()
            {
                new FormatBlock()
                {
                    blockType="Intro", color="#0000ff", label="Exhibit", FormatString="{INTRO}",
                    contextMenuItems= new List<string>()
                    {
                        "Exhibit",
                        "Ex.",
                        "Exh.",
                        "Appendix",
                        "Appx.",
                        "Tab",
                        "Remove",
                    }

                },

                new FormatBlock()
                {
                    blockType="Index", color="#008000", label="#", FormatString="{INDEX}",
                    contextMenuItems= new List<string>()
                    {
                        "Numeric",
                        "Alphabetic",
                        "Roman Numeral",
                        "Remove",
                    }
                },

                new FormatBlock()
                {
                    blockType="Description", color="#FFFF00", label="Description", FormatString="{DESC}",
                    contextMenuItems= new List<string>
                    {
                        "Remove",
                    }
                },

            };
        }

        /// <summary>
        /// Formats the blocks as a string to be stored int he database
        /// </summary>
        /// <param name="formatBlocks"></param>
        /// <returns></returns>
        public string GetFormattingString(List<FormatBlock> formatBlocks)
        {
            string result = "";

            for (int i = 0; i < formatBlocks.Count; i++)
            {
                result += formatBlocks[i].FormatString;
            }

            Regex CloseSpacesCommas = new Regex(@" ,");
            Regex CloseSpacesLeftParens = new Regex(@"\( ");
            Regex CloseSpacesRightParens = new Regex(@" \)");

            result = CloseSpacesCommas.Replace(result, @",");
            result = CloseSpacesLeftParens.Replace(result, @"(");
            result = CloseSpacesRightParens.Replace(result, @")");

            return result;
        }

        /// <summary>
        /// Transforms the database format string into format blocks
        /// </summary>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public List<FormatBlock> GetFormatBlocks(string formatString)  //{INTRO}{INDEX}{,}{DESC}{(}{PIN}{)}{FT1}{FT2}
        {

            List<FormatBlock> result = new List<FormatBlock>();

            var formatStringParts = formatString.Split(new string[] { "{{", "}}" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < formatStringParts.Length; i++)
            {
                result.Add(new FormatBlock(formatStringParts[i], repository.CiteFormatting));
            }

            return result;
        }


    }
}
