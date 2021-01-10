﻿using LitKit1.ControlsWPF.Citation.ViewModels;
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
    /// Interaction logic for CiteAdd.xaml
    /// </summary>
    public partial class CiteAdd : UserControl
    {

        private CiteType citeType = CiteType.Exhibit;

        private CiteMainVM ViewModel;

        private string ExhibitIntro;
        private ExhibitIndexStyle ExhibitIndexStyle;

        bool firstTimeOpen = true;

        const string LongDescPlaceholderText_exhibit = "Example Description of an Exhibit";
        const string LongDescPlaceholderText_Legal = "Palsgraf v. Long Island R.R. Co., 162 N.E. 99,101 (N.Y. 1928)";
        const string LongDescPlaceholderText_Record = "ECF No 31 (Amended Complaint)";
        const string LongDescPlaceholderText_Other = "John Smith, Myths (2003)";
        string LongDescPlaceholderText = LongDescPlaceholderText_exhibit;

        const string ShortDescPlaceholderText_Legal = "Paslgraf, 162 N.E at 101";
        const string ShortDescPlaceholderText_Record = "ECF No. 31";
        const string ShortDescPlaceholderText_Other = "Smith, supra";
        string ShortDescPlaceholderText = ShortDescPlaceholderText_Legal;

        const string OtherIDPlaceholderText = "ABC0001234";


        public CiteAdd()
        {
            ViewModel = Globals.Ribbons.Ribbon1.citeVMDict[Globals.ThisAddIn.Application.ActiveWindow];
            ExhibitIntro = ViewModel.Repository.CiteFormatting.ExhibitIntro;
            ExhibitIndexStyle = ViewModel.Repository.CiteFormatting.ExhibitIndexStyle;

            InitializeComponent();

            Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
            Format_ShortDescriptionTextBox.Text = ShortDescPlaceholderText;
            Format_OtherIdentifierTextBox.Text = OtherIDPlaceholderText;

            UpdateExhibitIntroLabel();

        }

        private void Format_TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstTimeOpen)
            {
                citeType = (CiteType)Format_TypeComboBox.SelectedItem;
                ResetForm();
            }
            else firstTimeOpen = false;
        }

        private void Format_TypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            //citeType = (CiteType)Format_TypeComboBox.SelectedItem;
            //ResetForm();
        }


        private void ResetForm()
        {
            switch (citeType)
            {
                case CiteType.Exhibit:
                    {
                        LongDescPlaceholderText = LongDescPlaceholderText_exhibit;

                        if (Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_exhibit || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Legal || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Record || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Other)
                        {
                            Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
                            Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }

                        Format_LongDescriptionLabel.Content = "Exhibit Description";
                        Format_ShortDescription.Visibility = Visibility.Collapsed;
                        Format_LongDescriptionExhibitLabel.Visibility = Visibility.Visible;
                        tbPIN.Visibility = Visibility.Collapsed;
                        AddPinLong.Visibility = Visibility.Collapsed;

                    }
                    break;
                case CiteType.Legal:
                    {
                        LongDescPlaceholderText = LongDescPlaceholderText_Legal;
                        ShortDescPlaceholderText = ShortDescPlaceholderText_Legal;

                        if (Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_exhibit || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Legal || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Record || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Other)
                        {
                            Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
                            Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }
                        if (Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Legal || Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Record || Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Other)
                        {
                            Format_ShortDescriptionTextBox.Text = ShortDescPlaceholderText;
                            Format_ShortDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }

                        Format_LongDescriptionLabel.Content = "Long Cite";
                        Format_ShortDescription.Visibility = Visibility.Visible;
                        Format_LongDescriptionExhibitLabel.Visibility = Visibility.Collapsed;
                        tbPIN.Visibility = Visibility.Visible;
                        AddPinLong.Visibility = Visibility.Visible;

                    }
                    break;
                case CiteType.Record:
                    {
                        LongDescPlaceholderText = LongDescPlaceholderText_Record;
                        ShortDescPlaceholderText = ShortDescPlaceholderText_Record;

                        if (Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_exhibit || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Legal || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Record || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Other)
                        {
                            Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
                            Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }
                        if (Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Legal || Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Record || Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Other)
                        {
                            Format_ShortDescriptionTextBox.Text = ShortDescPlaceholderText;
                            Format_ShortDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }

                        Format_LongDescriptionLabel.Content = "Long Cite";
                        Format_ShortDescription.Visibility = Visibility.Visible;
                        Format_LongDescriptionExhibitLabel.Visibility = Visibility.Collapsed;
                        tbPIN.Visibility = Visibility.Visible;
                        AddPinLong.Visibility = Visibility.Visible;

                    }
                    break;
                case CiteType.Other:
                    {
                        LongDescPlaceholderText = LongDescPlaceholderText_Other;
                        ShortDescPlaceholderText = ShortDescPlaceholderText_Other;

                        if (Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_exhibit || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Legal || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Record || Format_LongDescriptionTextBox.Text == LongDescPlaceholderText_Other)
                        {
                            Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
                            Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }
                        if (Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Legal || Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Record || Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText_Other)
                        {
                            Format_ShortDescriptionTextBox.Text = ShortDescPlaceholderText;
                            Format_ShortDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
                        }

                        Format_LongDescriptionLabel.Content = "Long Cite";
                        Format_ShortDescription.Visibility = Visibility.Visible;
                        Format_LongDescriptionExhibitLabel.Visibility = Visibility.Collapsed;
                        tbPIN.Visibility = Visibility.Visible;
                        AddPinLong.Visibility = Visibility.Visible;

                    }
                    break;
                default:
                    throw new Exception("CiteType not found.");
            }
        }

        private void UpdateExhibitIntroLabel()
        {
            string a = ExhibitIntro;

            string b = "";
            switch (ExhibitIndexStyle)
            {
                case ExhibitIndexStyle.Numbers:
                    b = "1";
                    break;
                case ExhibitIndexStyle.Letters:
                    b = "A";
                    break;
                case ExhibitIndexStyle.Roman:
                    b = "I";
                    break;
                default:
                    break;
            }

            Format_LongDescriptionExhibitLabel.Content = a + " " + b;

        }

        private void btnAddCitation_Click(object sender, RoutedEventArgs e)
        {
            bool goodCite = true;
            string longText = Format_LongDescriptionTextBox.Text;
            if (longText == LongDescPlaceholderText || longText == "")
            {
                goodCite = false;
            }
            string shortText = Format_ShortDescriptionTextBox.Text;
            if (shortText == ShortDescPlaceholderText || shortText == "")
            {
                shortText = longText;
            }
            string otherText = Format_OtherIdentifierTextBox.Text;
            if (otherText == OtherIDPlaceholderText || otherText == "")
            {
                otherText = "";
            }

            if (goodCite)
            {
                var cite = new Tools.Citation.Citation(citeType, longText, shortText, otherText);

                ViewModel.AddNewCite(cite);

                this.Visibility = Visibility.Collapsed;
                btnCANCELAddCitation_Click(sender, e);
            }
            else System.Windows.Forms.MessageBox.Show("A description must be provided to continue");

        }

        private void btnCANCELAddCitation_Click(object sender, RoutedEventArgs e)
        {
            Format_TypeComboBox.SelectedIndex = 0;
            Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
            Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;

            Format_ShortDescriptionTextBox.Text = ShortDescPlaceholderText;
            Format_ShortDescriptionTextBox.Foreground = Brushes.DarkSlateGray;

            Format_OtherIdentifierTextBox.Text = OtherIDPlaceholderText;
            Format_OtherIdentifierTextBox.Foreground = Brushes.DarkSlateGray;
            this.Visibility = Visibility.Collapsed;
        }

        private void Format_LongDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Format_LongDescriptionTextBox.Text == LongDescPlaceholderText)
            {
                Format_LongDescriptionTextBox.Text = "";
                Format_LongDescriptionTextBox.Foreground = Brushes.Black;
            }
            //else
            // if (Format_LongDescriptionTextBox.Text == "")
            //{
            //    Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
            //    Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
            //}
            else
            {
                Format_LongDescriptionTextBox.Foreground = Brushes.Black;
            }
        }

        private void Format_LongDescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (Format_LongDescriptionTextBox.Text == LongDescPlaceholderText)
            //{
            //    Format_LongDescriptionTextBox.Text = "";
            //    Format_LongDescriptionTextBox.Foreground = Brushes.Black;
            //}
            //else
             if (Format_LongDescriptionTextBox.Text == "")
            {
                Format_LongDescriptionTextBox.Text = LongDescPlaceholderText;
                Format_LongDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
            }
            else
            {
                Format_LongDescriptionTextBox.Foreground = Brushes.Black;
            }
        }

        private void Format_ShortDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Format_ShortDescriptionTextBox.Text == ShortDescPlaceholderText)
            {
                Format_ShortDescriptionTextBox.Text = "";
                Format_ShortDescriptionTextBox.Foreground = Brushes.Black;
            }
            else
            {
                Format_ShortDescriptionTextBox.Foreground = Brushes.Black;
            }

        }

        private void Format_ShortDescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Format_ShortDescriptionTextBox.Text == "")
            {
                Format_ShortDescriptionTextBox.Text = ShortDescPlaceholderText;
                Format_ShortDescriptionTextBox.Foreground = Brushes.DarkSlateGray;
            }
            else
            {
                Format_ShortDescriptionTextBox.Foreground = Brushes.Black;
            }

        }

        private void Format_OtherIdentifierTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            switch (Format_OtherIdentifierTextBox.Text)
            {
                case OtherIDPlaceholderText:
                    Format_OtherIdentifierTextBox.Text = "";
                    Format_OtherIdentifierTextBox.Foreground = Brushes.Black;
                    break;
                //case "":
                //    Format_OtherIdentifierTextBox.Text = OtherIDPlaceholderText;
                //    Format_OtherIdentifierTextBox.Foreground = Brushes.DarkSlateGray;
                //    break;

                default:
                    Format_OtherIdentifierTextBox.Foreground = Brushes.Black;
                    break;
            }
        }

        private void Format_OtherIdentifierTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            switch (Format_OtherIdentifierTextBox.Text)
            {
                //case OtherIDPlaceholderText:
                //    Format_OtherIdentifierTextBox.Text = "";
                //    Format_OtherIdentifierTextBox.Foreground = Brushes.Black;
                //    break;
                case "":
                    Format_OtherIdentifierTextBox.Text = OtherIDPlaceholderText;
                    Format_OtherIdentifierTextBox.Foreground = Brushes.DarkSlateGray;
                    break;

                default:
                    Format_OtherIdentifierTextBox.Foreground = Brushes.Black;
                    break;
            }

        }

        #region Formatting buttons

        private void AddFormattingMarks(TextBox TB, string FormatMark)
        {
            if (TB.SelectionLength == 0)
            {
                var first = TB.Text.Substring(0, TB.SelectionStart);
                var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);
                TB.Text = first + FormatMark + " " + FormatMark + last;

            }
            else
            {
                var first = TB.Text.Substring(0, TB.SelectionStart);
                var sel = TB.Text.Substring(TB.SelectionStart, TB.SelectionLength);
                var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);

                TB.Text = first + FormatMark + sel + FormatMark + last;
            }
        }

        private void BoldbtnLong_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(Format_LongDescriptionTextBox, "**");

        }

        private void ItalicsbtnLong_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(Format_LongDescriptionTextBox, "//");

        }

        private void UnderlinebtnLong_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(Format_LongDescriptionTextBox, "__");
        }

        private void NBSbtnLong_Click(object sender, RoutedEventArgs e)
        {
            string FormatMark = @"` `"; /*"\\u00A0"*/
            TextBox TB = Format_LongDescriptionTextBox;

            var first = TB.Text.Substring(0, TB.SelectionStart);
            var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);
            TB.Text = first + FormatMark + last;

        }

        private void PinLong_Click(object sender, RoutedEventArgs e)
        {
            string FormatMark = "{{PIN}}";
            TextBox TB = Format_LongDescriptionTextBox;
            if (TB.Text.Contains(FormatMark))
            {
                System.Windows.Forms.MessageBox.Show("A Pincite Placeholder has already been added.");
            }
            else
            {
                var first = TB.Text.Substring(0, TB.SelectionStart);
                var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);
                TB.Text = first + FormatMark + last;
            }
        }

        private void BoldbtnShort_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(Format_ShortDescriptionTextBox, "**");

        }

        private void ItalicsbtnShort_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(Format_ShortDescriptionTextBox, "//");

        }

        private void UnderlinebtnShort_Click(object sender, RoutedEventArgs e)
        {
            AddFormattingMarks(Format_ShortDescriptionTextBox, "__");
        }

        private void NBSbtnShort_Click(object sender, RoutedEventArgs e)
        {
            string FormatMark = @"` `"; /*"\\u00A0"*/
            TextBox TB = Format_ShortDescriptionTextBox;

            var first = TB.Text.Substring(0, TB.SelectionStart);
            var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);
            TB.Text = first + FormatMark + last;

        }

        private void PinShort_Click(object sender, RoutedEventArgs e)
        {
            string FormatMark = "{{PIN}}";
            TextBox TB = Format_ShortDescriptionTextBox;

            if (TB.Text.Contains(FormatMark))
            {
                System.Windows.Forms.MessageBox.Show("A Pincite Placeholder has already been added.");
            }
            else
            {
                var first = TB.Text.Substring(0, TB.SelectionStart);
                var last = TB.Text.Substring(TB.SelectionStart + TB.SelectionLength);
                TB.Text = first + FormatMark + last;
            }
        }
        #endregion
    }
}
