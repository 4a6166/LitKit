using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Word;
using Tools.Citation;
using Application = Microsoft.Office.Interop.Word.Application;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteMain.xaml
    /// </summary>
    public partial class CiteMain : UserControl
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Application _app;
        //CiteHelper helper;
        //CitationRepository repository;        

        public CiteMain()
        {
            log.Debug("CiteMain started");

            this._app = Globals.ThisAddIn.Application;
            
            //this.helper = new CiteHelper(_app);
            //this.repository = helper.repository;


            InitializeComponent();
            log.Debug("CiteMain Initialized");
            
            AddSearchBar();

            //var test = TestCites();
            //log.Debug("Test Cites Created");

            //AddCitesToPanel(test);
            //log.Debug("Test Cites added to the panel");
        }

        private void AddSearchBar()
        {
            Controls.SearchBar searchBar = new Controls.SearchBar(CiteBlockStackPanel);
            Grid.SetColumn(searchBar, 1);
            Grid.SetColumnSpan(searchBar, 2);
            Grid.SetRow(searchBar, 1);
            searchBar.HorizontalAlignment = HorizontalAlignment.Stretch;
            searchBar.VerticalAlignment = VerticalAlignment.Top;
            MainGrid.Children.Add(searchBar);
        }

        public void AddCitesToPanel(List<Tools.Exhibit.Citation> citations)
        {
            foreach (Tools.Exhibit.Citation citation in citations)
            {
                string LongExample = citation.CiteType.ToString() + " 1, "+citation.LongDescription;  //repository.CiteFormatting.FormatCiteText(citation, CitePlacementType.Long, null, 1);
                CiteBlock citeBlock = new CiteBlock(citation, LongExample, CiteBlockStackPanel, 0, 1);
                citeBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                CiteBlockStackPanel.Children.Add(citeBlock);
            }
        }


        public List<Tools.Citation.Citation>TestCites()
        {
            List<Tools.Citation.Citation> citations = new List<Tools.Citation.Citation>();

            Tools.Citation.Citation citationFirst = new Tools.Citation.Citation("TESTID1", CiteType.Other, "Test First Citation LongDescription");
            citations.Add(citationFirst);

            for (int i = 0; i< 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID"+i.ToString(), CiteType.Exhibit, "Test Long EXHIBIT " +i.ToString());
                citations.Add(citation);
            }

            for (int i = 0; i < 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID" + i.ToString(), CiteType.Legal, "Test Long LEGAL " + i.ToString());
                citations.Add(citation);
            }

            for (int i = 0; i < 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID" + i.ToString(), CiteType.Record, "Test Long RECORD " + i.ToString());
                citations.Add(citation);
            }
            for (int i = 0; i < 5; i++)
            {
                Tools.Citation.Citation citation = new Tools.Citation.Citation("TESTID" + i.ToString(), CiteType.Other, "Test Long OTHER " + i.ToString());
                citations.Add(citation);
            }

            return citations;

        }
    }
}
