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
using Tools.Exhibit;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteMain.xaml
    /// </summary>
    public partial class CiteMain : UserControl
    {
        List<Exhibit> Citations;

        public CiteMain()
        {
            InitializeComponent();
            this.Citations = new List<Exhibit>();
            AddTestExhibits();

            AddSearchBar();
            AddCitesToPanel();
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

        public void AddCitesToPanel()
        {
            foreach (Exhibit citation in Citations)
            {
                CiteBlock citeBlock = new CiteBlock(citation, CiteBlockStackPanel, 0, 1);
                citeBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                CiteBlockStackPanel.Children.Add(citeBlock);
            }
        }


        public void AddTestExhibits()
        {
            Exhibit exhibita = new Exhibit("2014.06.26 Risinger Deposition that took place on 6/26/2014", "DEF000001");
            Citations.Add(exhibita);

            for (int i = 0; i < 5; i++)
            {
                Exhibit exhibit = new Exhibit("Test Exhibit " + i, "ABC0000" + i);
                exhibit.CiteType = CiteType.Exhibit;
                Citations.Add(exhibit);
            }

            for (int i = 0; i < 5; i++)
            {
                Exhibit exhibit = new Exhibit("Test Legal Cite " + i, "ABC0000" + i);
                exhibit.CiteType = CiteType.Legal;
                Citations.Add(exhibit);
            }

            for (int i = 0; i < 5; i++)
            {
                Exhibit exhibit = new Exhibit("Test Record Cite " + i, "ABC0000" + i);
                exhibit.CiteType = CiteType.Record;
                Citations.Add(exhibit);
            }

            for (int i = 0; i < 5; i++)
            {
                Exhibit exhibit = new Exhibit("Test Other Cite " + i, "ABC0000" + i);
                exhibit.CiteType = CiteType.Other;
                Citations.Add(exhibit);
            }
        }
    }
}
