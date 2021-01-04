using LitKit1.ControlsWPF.Citation.ViewModels;
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
using Tools.Citation;

namespace LitKit1.ControlsWPF.Citation
{
    /// <summary>
    /// Interaction logic for CiteFormatBlock.xaml
    /// </summary>
    public partial class CiteFormatBlock : UserControl
    {
        bool ContextMenuAdded = false;
        CiteMainVM ViewModel;

        public CiteFormatBlock()
        {
            ViewModel = Globals.Ribbons.Ribbon1.citeVMDict[Globals.ThisAddIn.Application.ActiveWindow];
            InitializeComponent();
        }


        private void setContextMenu()
        {
            if (!ContextMenuAdded)
            {
                var type = (CiteFormatPiece)this.DataContext;

                switch (type.Type)
                {
                    case CiteFormatPieceType.INTRO:
                        var a = new MenuItem() { Header = "Exhibit" };
                        a.Click += delegate { cm_Exhibit(); };

                        var b = new MenuItem() { Header = "Ex." };
                        b.Click += delegate { cm_Ex(); };

                        var c = new MenuItem() { Header = "Exh." };
                        c.Click += delegate { cm_Exh(); };

                        var d = new MenuItem() { Header = "Appendix" };
                        d.Click += delegate { cm_Appendix(); };

                        var e = new MenuItem() { Header = "Appx." };
                        e.Click += delegate { cm_Appx(); };

                        var f = new MenuItem() { Header = "Tab" };
                        f.Click += delegate { cm_Tab(); };

                        BlockContextMenu.Items.Add(a);
                        BlockContextMenu.Items.Add(b);
                        BlockContextMenu.Items.Add(c);
                        BlockContextMenu.Items.Add(d);
                        BlockContextMenu.Items.Add(e);
                        BlockContextMenu.Items.Add(f);
                        break;
                    case CiteFormatPieceType.INDEX:
                        var g = new MenuItem() { Header = "Numeric" };
                        g.Click += delegate { cm_Numeric(); };

                        var h = new MenuItem() { Header = "Alphabetic" };
                        h.Click += delegate { cm_Alphabetic(); };

                        var i = new MenuItem() { Header = "Roman" };
                        i.Click += delegate { cm_Roman(); };

                        BlockContextMenu.Items.Add(g);
                        BlockContextMenu.Items.Add(h);
                        BlockContextMenu.Items.Add(i);
                        break;

                }

                var remove = new MenuItem() { Header = "Remove this Block" };
                remove.Click += delegate { cm_Remove(); };
                BlockContextMenu.Items.Add(new Separator());
                BlockContextMenu.Items.Add(remove);

                ContextMenuAdded = true;
            }
    }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            DropDown.Visibility = Visibility.Visible;
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            DropDown.Visibility = Visibility.Collapsed;
        }

        private void DragDropGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            DropDown.Visibility = Visibility.Collapsed;
        }

        private void DragDropGrid_MouseEnter(object sender, MouseEventArgs e)
        {
           setContextMenu();
           DropDown.Visibility = Visibility.Visible;
        }

        private void DropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BlockContextMenu.IsOpen = true;
        }


        #region Context Menu Actions

        private void cm_Exhibit()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "Exhibit";

            //TODO: update the repository intro type **********************************************************************************************
        }

        private void cm_Ex()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "Ex.";
        }


        private void cm_Exh()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "Exh.";
        }


        private void cm_Appendix()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "Appendix";
        }
        private void cm_Appx()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "Appx.";
        }
        private void cm_Tab()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "Tab";
        }

        private void cm_Numeric()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "#";
        }

        private void cm_Alphabetic()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "A";
        }

        private void cm_Roman()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = "IV";
        }

        private void cm_Remove()
        {

            var formatPiece = (CiteFormatPiece)this.DataContext;

            if (formatPiece.Type == CiteFormatPieceType.PIN)
            {
                System.Windows.Forms.DialogResult mb = System.Windows.Forms.MessageBox.Show("Note: Removing the PIN block will prevent the addition of Pincites to all exhibits. Are you sure you want to continue?", "Confirm", System.Windows.Forms.MessageBoxButtons.OKCancel);
                if (mb == System.Windows.Forms.DialogResult.OK)
                {
                    ViewModel.FormatList_Long.Remove(formatPiece); //TODO: Only removes from Format List Long. Need to determine if block is in short list and remove from there if so instead
                }
            }
            else ViewModel.FormatList_Long.Remove(formatPiece); //TODO: Only removes from Format List Long. Need to determine if block is in short list and remove from there if so instead

        }

        #endregion
    }
}
