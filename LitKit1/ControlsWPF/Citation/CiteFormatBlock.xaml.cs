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
                    case CiteFormatPieceType.INTROLONG:
                        var a = new MenuItem() { Header = "Exhibit" };
                        a.Click += delegate { cm_ActionsIntro(a.Header); };

                        var b = new MenuItem() { Header = "Ex." };
                        b.Click += delegate { cm_ActionsIntro(b.Header); };

                        var c = new MenuItem() { Header = "Exh." };
                        c.Click += delegate { cm_ActionsIntro(c.Header); };

                        var d = new MenuItem() { Header = "Appendix" };
                        d.Click += delegate { cm_ActionsIntro(d.Header); };

                        var e = new MenuItem() { Header = "Appx." };
                        e.Click += delegate { cm_ActionsIntro(e.Header); };

                        var f = new MenuItem() { Header = "Tab" };
                        f.Click += delegate { cm_ActionsIntro(f.Header); };

                        BlockContextMenu.Items.Add(a);
                        BlockContextMenu.Items.Add(b);
                        BlockContextMenu.Items.Add(c);
                        BlockContextMenu.Items.Add(d);
                        BlockContextMenu.Items.Add(e);
                        BlockContextMenu.Items.Add(f);
                        BlockContextMenu.Items.Add(new Separator());
                        break;
                    case CiteFormatPieceType.INTROSHORT:
                        var a1 = new MenuItem() { Header = "Exhibit" };
                        a1.Click += delegate { cm_ActionsIntro(a1.Header); };

                        var b1 = new MenuItem() { Header = "Ex." };
                        b1.Click += delegate { cm_ActionsIntro(b1.Header); };

                        var c1 = new MenuItem() { Header = "Exh." };
                        c1.Click += delegate { cm_ActionsIntro(c1.Header); };

                        var d1 = new MenuItem() { Header = "Appendix" };
                        d1.Click += delegate { cm_ActionsIntro(d1.Header); };

                        var e1 = new MenuItem() { Header = "Appx." };
                        e1.Click += delegate { cm_ActionsIntro(e1.Header); };

                        var f1 = new MenuItem() { Header = "Tab" };
                        f1.Click += delegate { cm_ActionsIntro(f1.Header); };

                        BlockContextMenu.Items.Add(a1);
                        BlockContextMenu.Items.Add(b1);
                        BlockContextMenu.Items.Add(c1);
                        BlockContextMenu.Items.Add(d1);
                        BlockContextMenu.Items.Add(e1);
                        BlockContextMenu.Items.Add(f1);
                        BlockContextMenu.Items.Add(new Separator());
                        break;
                    case CiteFormatPieceType.INDEX:
                        var g = new MenuItem() { Header = "Numeric" };
                        g.Click += delegate { cm_ActionsIndex(g.Header); };

                        var h = new MenuItem() { Header = "Alphabetic" };
                        h.Click += delegate { cm_ActionsIndex(h.Header); };

                        var i = new MenuItem() { Header = "Roman" };
                        i.Click += delegate { cm_ActionsIndex(i.Header); };

                        BlockContextMenu.Items.Add(g);
                        BlockContextMenu.Items.Add(h);
                        BlockContextMenu.Items.Add(i);
                        BlockContextMenu.Items.Add(new Separator());
                        break;
                    case CiteFormatPieceType.FREETEXT:
                        var j = new MenuItem() { Header = "Edit Text" };
                        j.Click += delegate { cm_UpdateFreeText(); };
                        BlockContextMenu.Items.Add(j);
                        BlockContextMenu.Items.Add(new Separator());
                        break;
                }

                var remove = new MenuItem() { Header = "Remove this Block" };
                remove.Click += delegate { cm_Remove(); };
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

        private void cm_ActionsIntro(object labelValue)
        {
            // // For making both long and short cite intros match
            //var piece1 = ViewModel.FormatList_Long.FirstOrDefault(n => n.Type == CiteFormatPieceType.INTRO);
            //if (piece1 != null)
            //{ piece1.DisplayText = labelValue.ToString(); }

            //var piece2 = ViewModel.FormatList_Short.FirstOrDefault(n => n.Type == CiteFormatPieceType.INTRO);
            //if (piece2 != null)
            //{ piece2.DisplayText = labelValue.ToString(); }

            var formatPiece = (CiteFormatPiece)this.DataContext;
            formatPiece.DisplayText = labelValue.ToString();
        }

        private void cm_ActionsIndex(object labelValue)
        {
            string insert = "";
            switch (labelValue.ToString())
            {
                case "Numeric":
                insert = "#";
                    break;
                case "Alphabetic":
                    insert = "A";
                    break;
                case "Roman":
                    insert = "IV";
                    break;
            }
            var piece1 = ViewModel.FormatList_Long.FirstOrDefault(n => n.Type == CiteFormatPieceType.INDEX);
            if (piece1 != null)
            { piece1.DisplayText = insert; }

            var piece2 = ViewModel.FormatList_Short.FirstOrDefault(n => n.Type == CiteFormatPieceType.INDEX);
            if (piece2 != null)
            { piece2.DisplayText = insert; }

            //ViewModel.Repository.CiteFormatting.ExhibitIntro = labelValue.ToString();
        }



        private void cm_Remove()
        {

            var formatPiece = (CiteFormatPiece)this.DataContext;

            var formatList = ViewModel.FormatList_Long;
            if(!formatList.Contains(formatPiece))
            {
                formatList = ViewModel.FormatList_Short;
            }

            if (formatPiece.Type == CiteFormatPieceType.PIN)
            {
                System.Windows.Forms.DialogResult mb = System.Windows.Forms.MessageBox.Show("Note: Removing the PIN block will prevent the addition of Pincites to all exhibits. Are you sure you want to continue?", "Confirm", System.Windows.Forms.MessageBoxButtons.OKCancel);
                if (mb == System.Windows.Forms.DialogResult.OK)
                {
                    formatList.Remove(formatPiece); 
                }
            }
            else formatList.Remove(formatPiece); 

        }

        private void cm_UpdateFreeText()
        {
            var formatPiece = (CiteFormatPiece)this.DataContext;
            ViewModel.ChooseFreeTextEditBlock(formatPiece);
        }

        #endregion

    }
}
