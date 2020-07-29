namespace LitKit1
{
    partial class MainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MainRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonGroup grpShortcuts;
            this.menuTranscript = this.Factory.CreateRibbonMenu();
            this.btnBlockTranscript = this.Factory.CreateRibbonButton();
            this.btnInLineTranscript = this.Factory.CreateRibbonButton();
            this.menu1 = this.Factory.CreateRibbonMenu();
            this.btnPilcrow = this.Factory.CreateRibbonButton();
            this.btnSectionMark = this.Factory.CreateRibbonButton();
            this.btnTM = this.Factory.CreateRibbonButton();
            this.btnCopyright = this.Factory.CreateRibbonButton();
            this.btnNBS = this.Factory.CreateRibbonButton();
            this.btnNBHyphen = this.Factory.CreateRibbonButton();
            this.btnNDash = this.Factory.CreateRibbonButton();
            this.btnMDash = this.Factory.CreateRibbonButton();
            this.menu5 = this.Factory.CreateRibbonMenu();
            this.btnLatin = this.Factory.CreateRibbonButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.menu4 = this.Factory.CreateRibbonMenu();
            this.btnAddOxfordComma = this.Factory.CreateRibbonButton();
            this.btnRemoveOxfordComma = this.Factory.CreateRibbonButton();
            this.menu3 = this.Factory.CreateRibbonMenu();
            this.btnSingleSpace = this.Factory.CreateRibbonButton();
            this.btnDoubleSpace = this.Factory.CreateRibbonButton();
            this.btnSmrtQuotes = this.Factory.CreateRibbonButton();
            this.btnInsertNBS = this.Factory.CreateRibbonButton();
            this.btnBlockQuotes = this.Factory.CreateRibbonButton();
            this.btnShowHide = this.Factory.CreateRibbonToggleButton();
            this.btnKeepWithNext = this.Factory.CreateRibbonButton();
            this.ClipboardButton = this.Factory.CreateRibbonButton();
            this.MainTab = this.Factory.CreateRibbonTab();
            this.RedactionsGroup = this.Factory.CreateRibbonGroup();
            this.markRedact = this.Factory.CreateRibbonButton();
            this.unmarkRedact = this.Factory.CreateRibbonButton();
            this.btnClearAllRedactions = this.Factory.CreateRibbonButton();
            this.menu2 = this.Factory.CreateRibbonMenu();
            this.redactedPDF = this.Factory.CreateRibbonButton();
            this.unredactedPDF = this.Factory.CreateRibbonButton();
            this.grpExhibitTool = this.Factory.CreateRibbonGroup();
            this.btnExhibitTool = this.Factory.CreateRibbonButton();
            this.btnPinCite = this.Factory.CreateRibbonButton();
            this.btnRemovePinCite = this.Factory.CreateRibbonButton();
            this.btnIndexOfExhibits = this.Factory.CreateRibbonButton();
            this.grpAnsRes = this.Factory.CreateRibbonGroup();
            this.splitbtnResposeTool = this.Factory.CreateRibbonSplitButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.CustomerSupport = this.Factory.CreateRibbonButton();
            this.ExhibitChangeControl = this.Factory.CreateRibbonButton();
            grpShortcuts = this.Factory.CreateRibbonGroup();
            grpShortcuts.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.RedactionsGroup.SuspendLayout();
            this.grpExhibitTool.SuspendLayout();
            this.grpAnsRes.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpShortcuts
            // 
            grpShortcuts.Items.Add(this.menuTranscript);
            grpShortcuts.Items.Add(this.menu1);
            grpShortcuts.Items.Add(this.menu5);
            grpShortcuts.Items.Add(this.menu4);
            grpShortcuts.Items.Add(this.menu3);
            grpShortcuts.Items.Add(this.btnSmrtQuotes);
            grpShortcuts.Items.Add(this.btnInsertNBS);
            grpShortcuts.Items.Add(this.btnBlockQuotes);
            grpShortcuts.Items.Add(this.btnShowHide);
            grpShortcuts.Items.Add(this.btnKeepWithNext);
            grpShortcuts.Items.Add(this.ClipboardButton);
            grpShortcuts.Label = "Shortcuts";
            grpShortcuts.Name = "grpShortcuts";
            // 
            // menuTranscript
            // 
            this.menuTranscript.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menuTranscript.Items.Add(this.btnBlockTranscript);
            this.menuTranscript.Items.Add(this.btnInLineTranscript);
            this.menuTranscript.Label = "Paste Transcript Text";
            this.menuTranscript.Name = "menuTranscript";
            this.menuTranscript.OfficeImageId = "MasterDocumentShow";
            this.menuTranscript.ScreenTip = "Formats and inserts text from transcripts and either block or in-line quotes.";
            this.menuTranscript.ShowImage = true;
            this.menuTranscript.SuperTip = "Removes line numbering from transcript text and formats the text to display corre" +
    "ctly as either a block or in-line quote.";
            // 
            // btnBlockTranscript
            // 
            this.btnBlockTranscript.Label = "Paste Transcript Text as Block Quote";
            this.btnBlockTranscript.Name = "btnBlockTranscript";
            this.btnBlockTranscript.OfficeImageId = "MailMergeMergeFieldInsert";
            this.btnBlockTranscript.ShowImage = true;
            this.btnBlockTranscript.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnBlockTranscript_Click);
            // 
            // btnInLineTranscript
            // 
            this.btnInLineTranscript.Label = "Paste Transcript Text as In-Text Quote";
            this.btnInLineTranscript.Name = "btnInLineTranscript";
            this.btnInLineTranscript.OfficeImageId = "MailMergeGreetingLineInsert";
            this.btnInLineTranscript.ShowImage = true;
            this.btnInLineTranscript.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnInLineTranscript_Click);
            // 
            // menu1
            // 
            this.menu1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menu1.Items.Add(this.btnPilcrow);
            this.menu1.Items.Add(this.btnSectionMark);
            this.menu1.Items.Add(this.btnTM);
            this.menu1.Items.Add(this.btnCopyright);
            this.menu1.Items.Add(this.btnNBS);
            this.menu1.Items.Add(this.btnNBHyphen);
            this.menu1.Items.Add(this.btnNDash);
            this.menu1.Items.Add(this.btnMDash);
            this.menu1.Label = "Legal Symbols";
            this.menu1.Name = "menu1";
            this.menu1.OfficeImageId = "ParagraphMarks";
            this.menu1.ShowImage = true;
            this.menu1.SuperTip = "Inserts the selected legal symbol at the cursor position";
            // 
            // btnPilcrow
            // 
            this.btnPilcrow.Label = "Paragraph Symbol (¶)";
            this.btnPilcrow.Name = "btnPilcrow";
            this.btnPilcrow.ShowImage = true;
            this.btnPilcrow.SuperTip = "Inserts a Paragraph Symbol (Pilcrow): ¶";
            this.btnPilcrow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPilcrow_Click);
            // 
            // btnSectionMark
            // 
            this.btnSectionMark.Label = "Section Symbol (§)";
            this.btnSectionMark.Name = "btnSectionMark";
            this.btnSectionMark.ShowImage = true;
            this.btnSectionMark.SuperTip = "Inserts a Section Symbol: §";
            this.btnSectionMark.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertSectionMark_Click);
            // 
            // btnTM
            // 
            this.btnTM.Label = "Trademark Symbol (™)";
            this.btnTM.Name = "btnTM";
            this.btnTM.ShowImage = true;
            this.btnTM.SuperTip = "Inserts a Trademark Symbol: ™";
            this.btnTM.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertTM_Click);
            // 
            // btnCopyright
            // 
            this.btnCopyright.Label = "Copyright Symbol (©)";
            this.btnCopyright.Name = "btnCopyright";
            this.btnCopyright.ShowImage = true;
            this.btnCopyright.SuperTip = "Inserts a Copyright Symbol: ©";
            this.btnCopyright.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertCopyright_Click);
            // 
            // btnNBS
            // 
            this.btnNBS.Label = "Non-Breaking Space (°)";
            this.btnNBS.Name = "btnNBS";
            this.btnNBS.ShowImage = true;
            this.btnNBS.SuperTip = "Inserts a Non-Breaking Space: °";
            this.btnNBS.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertNBS_Click);
            // 
            // btnNBHyphen
            // 
            this.btnNBHyphen.Label = "Non-Breaking Hyphen (-)";
            this.btnNBHyphen.Name = "btnNBHyphen";
            this.btnNBHyphen.ShowImage = true;
            this.btnNBHyphen.SuperTip = "Inserts a Non-Breaking Hyphen: -";
            this.btnNBHyphen.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNBHyphen_Click);
            // 
            // btnNDash
            // 
            this.btnNDash.Label = "N-Dash (–)";
            this.btnNDash.Name = "btnNDash";
            this.btnNDash.ShowImage = true;
            this.btnNDash.SuperTip = "Inserts an N-Dash: –";
            this.btnNDash.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertNDash_Click);
            // 
            // btnMDash
            // 
            this.btnMDash.Label = "M-Dash (—)";
            this.btnMDash.Name = "btnMDash";
            this.btnMDash.ShowImage = true;
            this.btnMDash.SuperTip = "Inserts an M-Dash: —";
            this.btnMDash.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertMDash_Click);
            // 
            // menu5
            // 
            this.menu5.Items.Add(this.btnLatin);
            this.menu5.Items.Add(this.button1);
            this.menu5.Label = "Latin Words";
            this.menu5.Name = "menu5";
            // 
            // btnLatin
            // 
            this.btnLatin.Label = "Italicize Latin";
            this.btnLatin.Name = "btnLatin";
            this.btnLatin.ShowImage = true;
            this.btnLatin.SuperTip = "Italicizes Latin words and phrases commonly used in the legal world";
            this.btnLatin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLatin_Click);
            // 
            // button1
            // 
            this.button1.Label = "Un-italicize Latin";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.SuperTip = "Removes italics from Latin words and phrases commonly used in the legal world";
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click_1);
            // 
            // menu4
            // 
            this.menu4.Items.Add(this.btnAddOxfordComma);
            this.menu4.Items.Add(this.btnRemoveOxfordComma);
            this.menu4.Label = "Oxford Comma";
            this.menu4.Name = "menu4";
            this.menu4.SuperTip = "Adds or removes Oxford (serialized) commas within the document";
            // 
            // btnAddOxfordComma
            // 
            this.btnAddOxfordComma.Label = "Add Oxford Commas";
            this.btnAddOxfordComma.Name = "btnAddOxfordComma";
            this.btnAddOxfordComma.ShowImage = true;
            this.btnAddOxfordComma.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOxfordComma_Click);
            // 
            // btnRemoveOxfordComma
            // 
            this.btnRemoveOxfordComma.Label = "Remove Oxford Commas";
            this.btnRemoveOxfordComma.Name = "btnRemoveOxfordComma";
            this.btnRemoveOxfordComma.ShowImage = true;
            this.btnRemoveOxfordComma.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemoveOxfordComma_Click);
            // 
            // menu3
            // 
            this.menu3.Items.Add(this.btnSingleSpace);
            this.menu3.Items.Add(this.btnDoubleSpace);
            this.menu3.Label = "Sentence Spacing";
            this.menu3.Name = "menu3";
            this.menu3.SuperTip = "Adds or removes a double space following the sentences";
            // 
            // btnSingleSpace
            // 
            this.btnSingleSpace.Label = "Single Space Between Sentences";
            this.btnSingleSpace.Name = "btnSingleSpace";
            this.btnSingleSpace.ShowImage = true;
            this.btnSingleSpace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSingleSpace_Click);
            // 
            // btnDoubleSpace
            // 
            this.btnDoubleSpace.Label = "Double Space Between Sentences";
            this.btnDoubleSpace.Name = "btnDoubleSpace";
            this.btnDoubleSpace.ShowImage = true;
            this.btnDoubleSpace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDoubleSpace_Click);
            // 
            // btnSmrtQuotes
            // 
            this.btnSmrtQuotes.Label = "Smart Quotes";
            this.btnSmrtQuotes.Name = "btnSmrtQuotes";
            this.btnSmrtQuotes.SuperTip = "Replaces dumb quotes with smart quotes in the document";
            this.btnSmrtQuotes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSmrtQuotes_Click);
            // 
            // btnInsertNBS
            // 
            this.btnInsertNBS.Label = "Insert NBS";
            this.btnInsertNBS.Name = "btnInsertNBS";
            this.btnInsertNBS.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnInsertNBS_Click);
            // 
            // btnBlockQuotes
            // 
            this.btnBlockQuotes.Label = "Block Quotes";
            this.btnBlockQuotes.Name = "btnBlockQuotes";
            this.btnBlockQuotes.Visible = false;
            this.btnBlockQuotes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnBlockQuotes_Click);
            // 
            // btnShowHide
            // 
            this.btnShowHide.Label = "Show / Hide ¶";
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.OfficeImageId = "ParagraphMarks";
            this.btnShowHide.ShowImage = true;
            this.btnShowHide.SuperTip = "Shows or hides formatting marks (non-printed characters) in the document";
            this.btnShowHide.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnShowHide_Click_1);
            // 
            // btnKeepWithNext
            // 
            this.btnKeepWithNext.Label = "Keep With Next";
            this.btnKeepWithNext.Name = "btnKeepWithNext";
            this.btnKeepWithNext.OfficeImageId = "StylesStyleSeparator";
            this.btnKeepWithNext.ShowImage = true;
            this.btnKeepWithNext.SuperTip = "Applies \"keep with next\" formatting to the selected paragraph";
            this.btnKeepWithNext.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnKeepWithNext_Click);
            // 
            // ClipboardButton
            // 
            this.ClipboardButton.Label = "View Clipboard";
            this.ClipboardButton.Name = "ClipboardButton";
            this.ClipboardButton.OfficeImageId = "ShowClipboard";
            this.ClipboardButton.ShowImage = true;
            this.ClipboardButton.SuperTip = "Shows the Windows Clipboard pannel, allowing for multiple items to be copied, pas" +
    "ted, and stored.";
            this.ClipboardButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ClipboardButton_Click);
            // 
            // MainTab
            // 
            this.MainTab.Groups.Add(this.RedactionsGroup);
            this.MainTab.Groups.Add(this.grpExhibitTool);
            this.MainTab.Groups.Add(this.grpAnsRes);
            this.MainTab.Groups.Add(grpShortcuts);
            this.MainTab.Groups.Add(this.group1);
            this.MainTab.Label = "LitKit";
            this.MainTab.Name = "MainTab";
            this.MainTab.Position = this.Factory.RibbonPosition.AfterOfficeId("TabHome");
            // 
            // RedactionsGroup
            // 
            this.RedactionsGroup.Items.Add(this.markRedact);
            this.RedactionsGroup.Items.Add(this.unmarkRedact);
            this.RedactionsGroup.Items.Add(this.btnClearAllRedactions);
            this.RedactionsGroup.Items.Add(this.menu2);
            this.RedactionsGroup.Label = "Redactions";
            this.RedactionsGroup.Name = "RedactionsGroup";
            // 
            // markRedact
            // 
            this.markRedact.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.markRedact.Label = "Mark Selection for Redaction";
            this.markRedact.Name = "markRedact";
            this.markRedact.OfficeImageId = "ReviewShowMarkupMenu";
            this.markRedact.ShowImage = true;
            this.markRedact.SuperTip = "Applies a marking that designates the selected text for redaction";
            this.markRedact.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.markRedact_Click);
            // 
            // unmarkRedact
            // 
            this.unmarkRedact.Label = "Unmark Selection";
            this.unmarkRedact.Name = "unmarkRedact";
            this.unmarkRedact.OfficeImageId = "DatasheetColumnRename";
            this.unmarkRedact.ShowImage = true;
            this.unmarkRedact.SuperTip = "Removes the redaction mark on the selected text";
            this.unmarkRedact.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.unmarkRedact_Click);
            // 
            // btnClearAllRedactions
            // 
            this.btnClearAllRedactions.Label = "Clear All Redactions";
            this.btnClearAllRedactions.Name = "btnClearAllRedactions";
            this.btnClearAllRedactions.OfficeImageId = "ClearFormats";
            this.btnClearAllRedactions.ShowImage = true;
            this.btnClearAllRedactions.SuperTip = "Removes all redactions from the document";
            this.btnClearAllRedactions.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnClearAllRedactions_Click);
            // 
            // menu2
            // 
            this.menu2.Items.Add(this.redactedPDF);
            this.menu2.Items.Add(this.unredactedPDF);
            this.menu2.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menu2.Label = "Create PDF";
            this.menu2.Name = "menu2";
            this.menu2.OfficeImageId = "MailMergeMergeToPrinter";
            this.menu2.ShowImage = true;
            this.menu2.SuperTip = "Creates a PDF of the document, either applying the inserted redactions or removin" +
    "g them and allowing the user to apply a header marking the confidentiality of th" +
    "e document";
            // 
            // redactedPDF
            // 
            this.redactedPDF.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.redactedPDF.Label = "Create Redacted PDF";
            this.redactedPDF.Name = "redactedPDF";
            this.redactedPDF.OfficeImageId = "FileSaveAsPdfOrXps";
            this.redactedPDF.ShowImage = true;
            this.redactedPDF.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.redactedPDF_Click);
            // 
            // unredactedPDF
            // 
            this.unredactedPDF.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.unredactedPDF.Label = "Create Unredacted PDF";
            this.unredactedPDF.Name = "unredactedPDF";
            this.unredactedPDF.OfficeImageId = "Grammar";
            this.unredactedPDF.ShowImage = true;
            this.unredactedPDF.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.unredactedPDF_Click);
            // 
            // grpExhibitTool
            // 
            this.grpExhibitTool.Items.Add(this.btnExhibitTool);
            this.grpExhibitTool.Items.Add(this.btnPinCite);
            this.grpExhibitTool.Items.Add(this.btnRemovePinCite);
            this.grpExhibitTool.Items.Add(this.btnIndexOfExhibits);
            this.grpExhibitTool.Label = "Exhibits";
            this.grpExhibitTool.Name = "grpExhibitTool";
            // 
            // btnExhibitTool
            // 
            this.btnExhibitTool.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnExhibitTool.Label = "Exhibit Tool";
            this.btnExhibitTool.Name = "btnExhibitTool";
            this.btnExhibitTool.OfficeImageId = "BaselineSave";
            this.btnExhibitTool.ShowImage = true;
            this.btnExhibitTool.SuperTip = "Display the LitKit Exhibit Tool";
            this.btnExhibitTool.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ExhibitTool_Click);
            // 
            // btnPinCite
            // 
            this.btnPinCite.Label = "Add Pincite";
            this.btnPinCite.Name = "btnPinCite";
            this.btnPinCite.OfficeImageId = "Pushpin";
            this.btnPinCite.ShowImage = true;
            this.btnPinCite.SuperTip = "Add a pincite to an existing Exhibit.";
            this.btnPinCite.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPinCite_Click);
            // 
            // btnRemovePinCite
            // 
            this.btnRemovePinCite.Label = "Remove Pincite";
            this.btnRemovePinCite.Name = "btnRemovePinCite";
            this.btnRemovePinCite.OfficeImageId = "CancelRequest";
            this.btnRemovePinCite.ShowImage = true;
            this.btnRemovePinCite.SuperTip = "Remove a pincite from an existing Exhibit.";
            this.btnRemovePinCite.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemovePinCite_Click);
            // 
            // btnIndexOfExhibits
            // 
            this.btnIndexOfExhibits.Label = "Index of Exhibits";
            this.btnIndexOfExhibits.Name = "btnIndexOfExhibits";
            this.btnIndexOfExhibits.OfficeImageId = "ListSetNumberingValue";
            this.btnIndexOfExhibits.ShowImage = true;
            this.btnIndexOfExhibits.SuperTip = "Inserts a table index of exhibits in the document at your current selection. This" +
    " table will not be updated when Exhibits are moved, edited, or deleted.";
            this.btnIndexOfExhibits.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.IndexOfExhibits_Click);
            // 
            // grpAnsRes
            // 
            this.grpAnsRes.Items.Add(this.splitbtnResposeTool);
            this.grpAnsRes.Label = "Responses";
            this.grpAnsRes.Name = "grpAnsRes";
            // 
            // splitbtnResposeTool
            // 
            this.splitbtnResposeTool.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.splitbtnResposeTool.Items.Add(this.button4);
            this.splitbtnResposeTool.Label = "Response Tool";
            this.splitbtnResposeTool.Name = "splitbtnResposeTool";
            this.splitbtnResposeTool.OfficeImageId = "ReplyAllWithInstantMessage";
            this.splitbtnResposeTool.SuperTip = "Display the LitKit Discovery Response Tool";
            this.splitbtnResposeTool.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Label = "Customize Answers and Responses";
            this.button4.Name = "button4";
            this.button4.OfficeImageId = "OmsCustomizeLayout";
            this.button4.ShowImage = true;
            this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button4_Click);
            // 
            // group1
            // 
            this.group1.Items.Add(this.CustomerSupport);
            this.group1.Items.Add(this.ExhibitChangeControl);
            this.group1.Label = "Support";
            this.group1.Name = "group1";
            // 
            // CustomerSupport
            // 
            this.CustomerSupport.Label = "Contact Us";
            this.CustomerSupport.Name = "CustomerSupport";
            this.CustomerSupport.OfficeImageId = "TechnicalSupport";
            this.CustomerSupport.ShowImage = true;
            this.CustomerSupport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CustomerSupport_Click);
            // 
            // ExhibitChangeControl
            // 
            this.ExhibitChangeControl.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ExhibitChangeControl.Enabled = false;
            this.ExhibitChangeControl.Image = global::LitKit1.Properties.Resources.Group;
            this.ExhibitChangeControl.Label = "TestClass Button";
            this.ExhibitChangeControl.Name = "ExhibitChangeControl";
            this.ExhibitChangeControl.OfficeImageId = "TipWizardHelp";
            this.ExhibitChangeControl.ShowImage = true;
            this.ExhibitChangeControl.Visible = false;
            this.ExhibitChangeControl.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Test_Button_Click);
            // 
            // MainRibbon
            // 
            this.Name = "MainRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.MainTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            grpShortcuts.ResumeLayout(false);
            grpShortcuts.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.MainTab.PerformLayout();
            this.RedactionsGroup.ResumeLayout(false);
            this.RedactionsGroup.PerformLayout();
            this.grpExhibitTool.ResumeLayout(false);
            this.grpExhibitTool.PerformLayout();
            this.grpAnsRes.ResumeLayout(false);
            this.grpAnsRes.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab MainTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpExhibitTool;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ClipboardButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnExhibitTool;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ExhibitChangeControl;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnKeepWithNext;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpAnsRes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPinCite;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemovePinCite;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnIndexOfExhibits;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CustomerSupport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPilcrow;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSectionMark;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTM;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCopyright;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNBS;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNBHyphen;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNDash;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMDash;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup RedactionsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton markRedact;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton unmarkRedact;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnClearAllRedactions;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton redactedPDF;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton unredactedPDF;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton splitbtnResposeTool;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnInLineTranscript;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuTranscript;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnBlockTranscript;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btnShowHide;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSmrtQuotes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnBlockQuotes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDoubleSpace;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnInsertNBS;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLatin;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddOxfordComma;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSingleSpace;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemoveOxfordComma;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon Ribbon1
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
