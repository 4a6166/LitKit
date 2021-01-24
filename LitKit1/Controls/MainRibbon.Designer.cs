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
    //        Microsoft.Office.Tools.Ribbon.RibbonGroup grpShortcuts;
    //        Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
    //        Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl2 = this.Factory.CreateRibbonDialogLauncher();
    //        Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl3 = this.Factory.CreateRibbonDialogLauncher();
    //        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainRibbon));
    //        Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl4 = this.Factory.CreateRibbonDialogLauncher();
    //        Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl5 = this.Factory.CreateRibbonDialogLauncher();
    //        this.menu1 = this.Factory.CreateRibbonMenu();
    //        this.btnPilcrow = this.Factory.CreateRibbonButton();
    //        this.btnSectionMark = this.Factory.CreateRibbonButton();
    //        this.btnTM = this.Factory.CreateRibbonButton();
    //        this.btnCopyright = this.Factory.CreateRibbonButton();
    //        this.btnNBS = this.Factory.CreateRibbonButton();
    //        this.btnNBHyphen = this.Factory.CreateRibbonButton();
    //        this.btnNDash = this.Factory.CreateRibbonButton();
    //        this.btnMDash = this.Factory.CreateRibbonButton();
    //        this.btnShowHide = this.Factory.CreateRibbonToggleButton();
    //        this.btnKeepWithNext = this.Factory.CreateRibbonButton();
    //        this.ClipboardButton = this.Factory.CreateRibbonButton();
    //        this.togglebtnSmallCaps = this.Factory.CreateRibbonToggleButton();
    //        this.btnReplace = this.Factory.CreateRibbonButton();
    //        this.MainTab = this.Factory.CreateRibbonTab();
    //        this.grpRedactions = this.Factory.CreateRibbonGroup();
    //        this.tglMarkRedaction = this.Factory.CreateRibbonToggleButton();
    //        this.unmarkRedact = this.Factory.CreateRibbonButton();
    //        this.btnClearAllRedactions = this.Factory.CreateRibbonButton();
    //        this.menu2 = this.Factory.CreateRibbonMenu();
    //        this.redactedPDF = this.Factory.CreateRibbonButton();
    //        this.unredactedPDF = this.Factory.CreateRibbonButton();
    //        this.btnHighlightedPDF = this.Factory.CreateRibbonButton();
    //        this.grpCitationsTool = this.Factory.CreateRibbonGroup();
    //        this.btnExhibitTool = this.Factory.CreateRibbonButton();
    //        this.menu6 = this.Factory.CreateRibbonMenu();
    //        this.btnPinCite = this.Factory.CreateRibbonButton();
    //        this.btnRemovePinCite = this.Factory.CreateRibbonButton();
    //        this.btnIndexOfExhibits = this.Factory.CreateRibbonButton();
    //        this.btnRemoveCiteLocks = this.Factory.CreateRibbonButton();
    //        this.testExhibits = this.Factory.CreateRibbonButton();
    //        this.grpAnsRes = this.Factory.CreateRibbonGroup();
    //        this.btnResposeTool = this.Factory.CreateRibbonButton();
    //        this.grpFormattingTools = this.Factory.CreateRibbonGroup();
    //        this.menuTranscript = this.Factory.CreateRibbonMenu();
    //        this.btnBlockTranscript = this.Factory.CreateRibbonButton();
    //        this.btnInLineTranscript = this.Factory.CreateRibbonButton();
    //        this.menu5 = this.Factory.CreateRibbonMenu();
    //        this.btnLatin = this.Factory.CreateRibbonButton();
    //        this.button1 = this.Factory.CreateRibbonButton();
    //        this.menu3 = this.Factory.CreateRibbonMenu();
    //        this.btnSingleSpace = this.Factory.CreateRibbonButton();
    //        this.btnDoubleSpace = this.Factory.CreateRibbonButton();
    //        this.btnSmrtQuotes = this.Factory.CreateRibbonButton();
    //        this.btnInsertNBS = this.Factory.CreateRibbonButton();
    //        this.btnBlockQuotes = this.Factory.CreateRibbonButton();
    //        this.menu4 = this.Factory.CreateRibbonMenu();
    //        this.btnAddOxfordComma = this.Factory.CreateRibbonButton();
    //        this.btnRemoveOxfordComma = this.Factory.CreateRibbonButton();
    //        this.btnRemoveLineBreaks = this.Factory.CreateRibbonButton();
    //        this.HyphenToEnDashbtn = this.Factory.CreateRibbonButton();
    //        this.grpSupport = this.Factory.CreateRibbonGroup();
    //        this.btnHowTo = this.Factory.CreateRibbonButton();
    //        this.CustomerSupport = this.Factory.CreateRibbonButton();
    //        this.TestButton1 = this.Factory.CreateRibbonButton();
    //        grpShortcuts = this.Factory.CreateRibbonGroup();
    //        grpShortcuts.SuspendLayout();
    //        this.MainTab.SuspendLayout();
    //        this.grpRedactions.SuspendLayout();
    //        this.grpCitationsTool.SuspendLayout();
    //        this.grpAnsRes.SuspendLayout();
    //        this.grpFormattingTools.SuspendLayout();
    //        this.grpSupport.SuspendLayout();
    //        this.SuspendLayout();
    //        // 
    //        // grpShortcuts
    //        // 
    //        ribbonDialogLauncherImpl1.Enabled = false;
    //        ribbonDialogLauncherImpl1.Visible = false;
    //        grpShortcuts.DialogLauncher = ribbonDialogLauncherImpl1;
    //        grpShortcuts.Items.Add(this.menu1);
    //        grpShortcuts.Items.Add(this.btnShowHide);
    //        grpShortcuts.Items.Add(this.btnKeepWithNext);
    //        grpShortcuts.Items.Add(this.ClipboardButton);
    //        grpShortcuts.Items.Add(this.togglebtnSmallCaps);
    //        grpShortcuts.Items.Add(this.btnReplace);
    //        grpShortcuts.Label = "Shortcuts";
    //        grpShortcuts.Name = "grpShortcuts";
    //        // 
    //        // menu1
    //        // 
    //        this.menu1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.menu1.Image = global::LitKit1.Properties.Resources.LegalSymbol_32px;
    //        this.menu1.Items.Add(this.btnPilcrow);
    //        this.menu1.Items.Add(this.btnSectionMark);
    //        this.menu1.Items.Add(this.btnTM);
    //        this.menu1.Items.Add(this.btnCopyright);
    //        this.menu1.Items.Add(this.btnNBS);
    //        this.menu1.Items.Add(this.btnNBHyphen);
    //        this.menu1.Items.Add(this.btnNDash);
    //        this.menu1.Items.Add(this.btnMDash);
    //        this.menu1.Label = "Legal Symbols";
    //        this.menu1.Name = "menu1";
    //        this.menu1.OfficeImageId = "ParagraphMarks";
    //        this.menu1.ScreenTip = "Legal Symbols";
    //        this.menu1.ShowImage = true;
    //        this.menu1.SuperTip = "Inserts the selected legal symbol at the surrent text selection.";
    //        // 
    //        // btnPilcrow
    //        // 
    //        this.btnPilcrow.Label = "Paragraph Symbol (¶)";
    //        this.btnPilcrow.Name = "btnPilcrow";
    //        this.btnPilcrow.ScreenTip = "Paragraph Symbol (¶)";
    //        this.btnPilcrow.ShowImage = true;
    //        this.btnPilcrow.SuperTip = "Inserts a Paragraph Symbol (Pilcrow): ¶";
    //        this.btnPilcrow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPilcrow_Click);
    //        // 
    //        // btnSectionMark
    //        // 
    //        this.btnSectionMark.Label = "Section Symbol (§)";
    //        this.btnSectionMark.Name = "btnSectionMark";
    //        this.btnSectionMark.ScreenTip = "Section Symbol (§)";
    //        this.btnSectionMark.ShowImage = true;
    //        this.btnSectionMark.SuperTip = "Inserts a Section Symbol: §";
    //        this.btnSectionMark.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertSectionMark_Click);
    //        // 
    //        // btnTM
    //        // 
    //        this.btnTM.Label = "Trademark Symbol (™)";
    //        this.btnTM.Name = "btnTM";
    //        this.btnTM.ScreenTip = "Trademark Symbol (™)";
    //        this.btnTM.ShowImage = true;
    //        this.btnTM.SuperTip = "Inserts a Trademark Symbol: ™";
    //        this.btnTM.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertTM_Click);
    //        // 
    //        // btnCopyright
    //        // 
    //        this.btnCopyright.Label = "Copyright Symbol (©)";
    //        this.btnCopyright.Name = "btnCopyright";
    //        this.btnCopyright.ScreenTip = "Copyright Symbol (©)";
    //        this.btnCopyright.ShowImage = true;
    //        this.btnCopyright.SuperTip = "Inserts a Copyright Symbol: ©";
    //        this.btnCopyright.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertCopyright_Click);
    //        // 
    //        // btnNBS
    //        // 
    //        this.btnNBS.Label = "Non-Breaking Space (°)";
    //        this.btnNBS.Name = "btnNBS";
    //        this.btnNBS.ScreenTip = "Non-Breaking Space (°)";
    //        this.btnNBS.ShowImage = true;
    //        this.btnNBS.SuperTip = "Inserts a Non-Breaking Space: °";
    //        this.btnNBS.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertNBS_Click);
    //        // 
    //        // btnNBHyphen
    //        // 
    //        this.btnNBHyphen.Label = "Non-Breaking Hyphen (-)";
    //        this.btnNBHyphen.Name = "btnNBHyphen";
    //        this.btnNBHyphen.ScreenTip = "Non-Breaking Hyphen (-)";
    //        this.btnNBHyphen.ShowImage = true;
    //        this.btnNBHyphen.SuperTip = "Inserts a Non-Breaking Hyphen: -";
    //        this.btnNBHyphen.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnNBHyphen_Click);
    //        // 
    //        // btnNDash
    //        // 
    //        this.btnNDash.Label = "En-Dash (–)";
    //        this.btnNDash.Name = "btnNDash";
    //        this.btnNDash.ScreenTip = "En-Dash (–)";
    //        this.btnNDash.ShowImage = true;
    //        this.btnNDash.SuperTip = "Inserts an En-Dash: –";
    //        this.btnNDash.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertNDash_Click);
    //        // 
    //        // btnMDash
    //        // 
    //        this.btnMDash.Label = "Em-Dash (—)";
    //        this.btnMDash.Name = "btnMDash";
    //        this.btnMDash.ScreenTip = "Em-Dash (—)";
    //        this.btnMDash.ShowImage = true;
    //        this.btnMDash.SuperTip = "Inserts an Em-Dash: —";
    //        this.btnMDash.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.insertMDash_Click);
    //        // 
    //        // btnShowHide
    //        // 
    //        this.btnShowHide.Image = global::LitKit1.Properties.Resources.ShowHideFMarks_16px;
    //        this.btnShowHide.Label = "Show / Hide ¶";
    //        this.btnShowHide.Name = "btnShowHide";
    //        this.btnShowHide.OfficeImageId = "ParagraphMarks";
    //        this.btnShowHide.ScreenTip = "Show or Hide Formatting Markings";
    //        this.btnShowHide.ShowImage = true;
    //        this.btnShowHide.SuperTip = "Shows formatting marks (non-printed characters) throughout the document. This ena" +
    //"bles you to better see which formatting is being applied in white spaces.";
    //        this.btnShowHide.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnShowHide_Click_1);
    //        // 
    //        // btnKeepWithNext
    //        // 
    //        this.btnKeepWithNext.Image = global::LitKit1.Properties.Resources.KeepWithNext_16px;
    //        this.btnKeepWithNext.Label = "Keep With Next";
    //        this.btnKeepWithNext.Name = "btnKeepWithNext";
    //        this.btnKeepWithNext.OfficeImageId = "StylesStyleSeparator";
    //        this.btnKeepWithNext.ScreenTip = "Keep With Next";
    //        this.btnKeepWithNext.ShowImage = true;
    //        this.btnKeepWithNext.SuperTip = "Applies \"keep with next\" formatting to the selected paragraph, preventing it from" +
    //" being separated from the next paragraph by page breaks.";
    //        this.btnKeepWithNext.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnKeepWithNext_Click);
    //        // 
    //        // ClipboardButton
    //        // 
    //        this.ClipboardButton.Label = "View Clipboard";
    //        this.ClipboardButton.Name = "ClipboardButton";
    //        this.ClipboardButton.OfficeImageId = "ShowClipboard";
    //        this.ClipboardButton.ScreenTip = "View Detailed Clipboard";
    //        this.ClipboardButton.ShowImage = true;
    //        this.ClipboardButton.SuperTip = "Shows the Windows Clipboard pannel, allowing for multiple items to be copied, pas" +
    //"ted, and stored.";
    //        this.ClipboardButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ClipboardButton_Click);
    //        // 
    //        // togglebtnSmallCaps
    //        // 
    //        this.togglebtnSmallCaps.Label = "Small Caps";
    //        this.togglebtnSmallCaps.Name = "togglebtnSmallCaps";
    //        this.togglebtnSmallCaps.OfficeImageId = "TextSmallCaps";
    //        this.togglebtnSmallCaps.ScreenTip = "Toggle Small Caps";
    //        this.togglebtnSmallCaps.ShowImage = true;
    //        this.togglebtnSmallCaps.SuperTip = "When activated, text is inputted using small capital letters instead of lowercase" +
    //".";
    //        this.togglebtnSmallCaps.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.togglebtnSmallCaps_Click);
    //        // 
    //        // btnReplace
    //        // 
    //        this.btnReplace.Label = "Find/Replace";
    //        this.btnReplace.Name = "btnReplace";
    //        this.btnReplace.OfficeImageId = "ReplaceDialog";
    //        this.btnReplace.ScreenTip = "Find and Replace";
    //        this.btnReplace.ShowImage = true;
    //        this.btnReplace.SuperTip = "Opens the Find and Replace dialog instead of the modern Word navigation pane that" +
    //" opens when [ctrl + f] is pressed.";
    //        this.btnReplace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReplace_Click);
    //        // 
    //        // MainTab
    //        // 
    //        this.MainTab.Groups.Add(this.grpRedactions);
    //        this.MainTab.Groups.Add(this.grpCitationsTool);
    //        this.MainTab.Groups.Add(this.grpAnsRes);
    //        this.MainTab.Groups.Add(this.grpFormattingTools);
    //        this.MainTab.Groups.Add(grpShortcuts);
    //        this.MainTab.Groups.Add(this.grpSupport);
    //        this.MainTab.Label = "LitKit";
    //        this.MainTab.Name = "MainTab";
    //        this.MainTab.Position = this.Factory.RibbonPosition.BeforeOfficeId("TabReviewWord");
    //        // 
    //        // grpRedactions
    //        // 
    //        ribbonDialogLauncherImpl2.Enabled = false;
    //        ribbonDialogLauncherImpl2.Visible = false;
    //        this.grpRedactions.DialogLauncher = ribbonDialogLauncherImpl2;
    //        this.grpRedactions.Items.Add(this.tglMarkRedaction);
    //        this.grpRedactions.Items.Add(this.unmarkRedact);
    //        this.grpRedactions.Items.Add(this.btnClearAllRedactions);
    //        this.grpRedactions.Items.Add(this.menu2);
    //        this.grpRedactions.Label = "Redactions";
    //        this.grpRedactions.Name = "grpRedactions";
    //        // 
    //        // tglMarkRedaction
    //        // 
    //        this.tglMarkRedaction.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.tglMarkRedaction.Image = global::LitKit1.Properties.Resources.MarkSelection_32px;
    //        this.tglMarkRedaction.Label = "Mark Selection for Redaction";
    //        this.tglMarkRedaction.Name = "tglMarkRedaction";
    //        this.tglMarkRedaction.ScreenTip = "Mark Selection for Redaction";
    //        this.tglMarkRedaction.ShowImage = true;
    //        this.tglMarkRedaction.SuperTip = "Applies a marking that designates the selected text for redaction. Redactions are" +
    //" applied using the Create PDF button.";
    //        this.tglMarkRedaction.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tglMarkRedaction_Click);
    //        // 
    //        // unmarkRedact
    //        // 
    //        this.unmarkRedact.Image = global::LitKit1.Properties.Resources.UnmarkSelection_16px;
    //        this.unmarkRedact.Label = "Unmark Selection";
    //        this.unmarkRedact.Name = "unmarkRedact";
    //        this.unmarkRedact.OfficeImageId = "DatasheetColumnRename";
    //        this.unmarkRedact.ScreenTip = "Unmark Redactions in Selection";
    //        this.unmarkRedact.ShowImage = true;
    //        this.unmarkRedact.SuperTip = "Removes the redaction marks in the selected text.";
    //        this.unmarkRedact.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.unmarkRedact_Click);
    //        // 
    //        // btnClearAllRedactions
    //        // 
    //        this.btnClearAllRedactions.Image = global::LitKit1.Properties.Resources.ClearAllRedactions_16px;
    //        this.btnClearAllRedactions.Label = "Clear All Redactions";
    //        this.btnClearAllRedactions.Name = "btnClearAllRedactions";
    //        this.btnClearAllRedactions.OfficeImageId = "ClearFormats";
    //        this.btnClearAllRedactions.ScreenTip = "Clear Redaction Marks";
    //        this.btnClearAllRedactions.ShowImage = true;
    //        this.btnClearAllRedactions.SuperTip = "Removes all redaction marks from the document.";
    //        this.btnClearAllRedactions.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnClearAllRedactions_Click);
    //        // 
    //        // menu2
    //        // 
    //        this.menu2.Image = global::LitKit1.Properties.Resources.CreatePDF_16px;
    //        this.menu2.Items.Add(this.redactedPDF);
    //        this.menu2.Items.Add(this.unredactedPDF);
    //        this.menu2.Items.Add(this.btnHighlightedPDF);
    //        this.menu2.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.menu2.Label = "Create PDF";
    //        this.menu2.Name = "menu2";
    //        this.menu2.OfficeImageId = "MailMergeMergeToPrinter";
    //        this.menu2.ShowImage = true;
    //        this.menu2.SuperTip = "Creates a PDF of the document, either applying the inserted redactions or removin" +
    //"g them and allowing the user to apply a header marking the confidentiality of th" +
    //"e document";
    //        // 
    //        // redactedPDF
    //        // 
    //        this.redactedPDF.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.redactedPDF.Image = global::LitKit1.Properties.Resources.CreateRedactedPDF_32px;
    //        this.redactedPDF.Label = "Create Redacted PDF";
    //        this.redactedPDF.Name = "redactedPDF";
    //        this.redactedPDF.OfficeImageId = "FileSaveAsPdfOrXps";
    //        this.redactedPDF.ScreenTip = "Create Redacted PDF";
    //        this.redactedPDF.ShowImage = true;
    //        this.redactedPDF.SuperTip = "Creates a PDF of the current document that is redacted where marked with the Reda" +
    //"ction Tool.";
    //        this.redactedPDF.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.redactedPDF_Click);
    //        // 
    //        // unredactedPDF
    //        // 
    //        this.unredactedPDF.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.unredactedPDF.Image = global::LitKit1.Properties.Resources.CreateUnredactedPDF_32px;
    //        this.unredactedPDF.Label = "Create Unredacted PDF";
    //        this.unredactedPDF.Name = "unredactedPDF";
    //        this.unredactedPDF.OfficeImageId = "Grammar";
    //        this.unredactedPDF.ScreenTip = "Create Unredacted PDF";
    //        this.unredactedPDF.ShowImage = true;
    //        this.unredactedPDF.SuperTip = "Creates a PDF of the current document with no redaction marks and an option confi" +
    //"dentiality header.";
    //        this.unredactedPDF.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.unredactedPDF_Click);
    //        // 
    //        // btnHighlightedPDF
    //        // 
    //        this.btnHighlightedPDF.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.btnHighlightedPDF.Image = global::LitKit1.Properties.Resources.CreateHighlightedPDF_32px_PrelimEdit3;
    //        this.btnHighlightedPDF.Label = "Create Highlighted PDF";
    //        this.btnHighlightedPDF.Name = "btnHighlightedPDF";
    //        this.btnHighlightedPDF.ScreenTip = "Create Highlighted PDF";
    //        this.btnHighlightedPDF.ShowImage = true;
    //        this.btnHighlightedPDF.SuperTip = "Creates a PDF of the current document that is highlighted where marked with the R" +
    //"edaction Tool and an optional confidentiality header.";
    //        this.btnHighlightedPDF.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnHighlightedPDF_Click);
    //        // 
    //        // grpCitationsTool
    //        // 
    //        ribbonDialogLauncherImpl3.Enabled = false;
    //        ribbonDialogLauncherImpl3.Visible = false;
    //        this.grpCitationsTool.DialogLauncher = ribbonDialogLauncherImpl3;
    //        this.grpCitationsTool.Items.Add(this.btnExhibitTool);
    //        this.grpCitationsTool.Items.Add(this.menu6);
    //        this.grpCitationsTool.Items.Add(this.btnIndexOfExhibits);
    //        this.grpCitationsTool.Items.Add(this.btnRemoveCiteLocks);
    //        this.grpCitationsTool.Items.Add(this.testExhibits);
    //        this.grpCitationsTool.Label = "Citations";
    //        this.grpCitationsTool.Name = "grpCitationsTool";
    //        // 
    //        // btnExhibitTool
    //        // 
    //        this.btnExhibitTool.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.btnExhibitTool.Image = global::LitKit1.Properties.Resources.ExhibitTool_32px;
    //        this.btnExhibitTool.Label = "Citations Tool";
    //        this.btnExhibitTool.Name = "btnExhibitTool";
    //        this.btnExhibitTool.OfficeImageId = "BaselineSave";
    //        this.btnExhibitTool.ScreenTip = "LitKit Citations Tool";
    //        this.btnExhibitTool.ShowImage = true;
    //        this.btnExhibitTool.SuperTip = resources.GetString("btnExhibitTool.SuperTip");
    //        this.btnExhibitTool.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CitationsTool_Click);
    //        // 
    //        // menu6
    //        // 
    //        this.menu6.Image = global::LitKit1.Properties.Resources.AddPincite_16px;
    //        this.menu6.Items.Add(this.btnPinCite);
    //        this.menu6.Items.Add(this.btnRemovePinCite);
    //        this.menu6.Label = "Pincite";
    //        this.menu6.Name = "menu6";
    //        this.menu6.ScreenTip = "Pincite";
    //        this.menu6.ShowImage = true;
    //        this.menu6.SuperTip = "Adds or Removes a pincite from a citation that has been entered with the Citation" +
    //" Tool.";
    //        // 
    //        // btnPinCite
    //        // 
    //        this.btnPinCite.Image = global::LitKit1.Properties.Resources.AddPincite_16px_PrelimineEdit;
    //        this.btnPinCite.Label = "Add Pincite";
    //        this.btnPinCite.Name = "btnPinCite";
    //        this.btnPinCite.OfficeImageId = "Pushpin";
    //        this.btnPinCite.ScreenTip = "Add Pincite";
    //        this.btnPinCite.ShowImage = true;
    //        this.btnPinCite.SuperTip = "Adds a pincite to an existing citation. This pincite stays with the citation, whe" +
    //"ther or not the format changes.";
    //        this.btnPinCite.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnPinCite_Click);
    //        // 
    //        // btnRemovePinCite
    //        // 
    //        this.btnRemovePinCite.Image = global::LitKit1.Properties.Resources.RemovePincite_16px;
    //        this.btnRemovePinCite.Label = "Remove Pincite";
    //        this.btnRemovePinCite.Name = "btnRemovePinCite";
    //        this.btnRemovePinCite.OfficeImageId = "CancelRequest";
    //        this.btnRemovePinCite.ScreenTip = "Remove a Pincite";
    //        this.btnRemovePinCite.ShowImage = true;
    //        this.btnRemovePinCite.SuperTip = "Remove a pincite from an existing citation.";
    //        this.btnRemovePinCite.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemovePinCite_Click);
    //        // 
    //        // btnIndexOfExhibits
    //        // 
    //        this.btnIndexOfExhibits.Image = global::LitKit1.Properties.Resources.IndexOfExhibits_16px;
    //        this.btnIndexOfExhibits.Label = "Index of Exhibits";
    //        this.btnIndexOfExhibits.Name = "btnIndexOfExhibits";
    //        this.btnIndexOfExhibits.OfficeImageId = "ListSetNumberingValue";
    //        this.btnIndexOfExhibits.ScreenTip = "Insert Index of Exhibits";
    //        this.btnIndexOfExhibits.ShowImage = true;
    //        this.btnIndexOfExhibits.SuperTip = "Inserts a table index of exhibits in the document at your current selection. This" +
    //" table will not be updated when Exhibits are moved, edited, or deleted.";
    //        this.btnIndexOfExhibits.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.IndexOfExhibits_Click);
    //        // 
    //        // btnRemoveCiteLocks
    //        // 
    //        this.btnRemoveCiteLocks.Label = "Remove Locks";
    //        this.btnRemoveCiteLocks.Name = "btnRemoveCiteLocks";
    //        this.btnRemoveCiteLocks.OfficeImageId = "MasterDocumentLockSubdocument";
    //        this.btnRemoveCiteLocks.ScreenTip = "Remove Citation Locks";
    //        this.btnRemoveCiteLocks.ShowImage = true;
    //        this.btnRemoveCiteLocks.SuperTip = "Removes the Content Controls containing citations that have been inserted with th" +
    //"e Citation Tool. The citation text remains in the document but will not be updat" +
    //"ed or refreshed.";
    //        this.btnRemoveCiteLocks.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemoveCiteLocks_Click);
    //        // 
    //        // testExhibits
    //        // 
    //        this.testExhibits.Label = "Add Test Exhibits";
    //        this.testExhibits.Name = "testExhibits";
    //        this.testExhibits.ScreenTip = "FOR TESTING PURPOSES";
    //        this.testExhibits.Visible = false;
    //        this.testExhibits.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddTestCitations);
    //        // 
    //        // grpAnsRes
    //        // 
    //        ribbonDialogLauncherImpl4.Enabled = false;
    //        ribbonDialogLauncherImpl4.Visible = false;
    //        this.grpAnsRes.DialogLauncher = ribbonDialogLauncherImpl4;
    //        this.grpAnsRes.Items.Add(this.btnResposeTool);
    //        this.grpAnsRes.Label = "Responses";
    //        this.grpAnsRes.Name = "grpAnsRes";
    //        // 
    //        // btnResposeTool
    //        // 
    //        this.btnResposeTool.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.btnResposeTool.Image = global::LitKit1.Properties.Resources.ResponseTool_32px;
    //        this.btnResposeTool.Label = "Response Tool";
    //        this.btnResposeTool.Name = "btnResposeTool";
    //        this.btnResposeTool.ScreenTip = "Discovery Response Tool";
    //        this.btnResposeTool.ShowImage = true;
    //        this.btnResposeTool.SuperTip = "Opens the LitKit Discovery Response Tool panel, allowing you to save, adjust, and" +
    //" insert repetitive language in Discovery Response documents with a single click." +
    //"";
    //        this.btnResposeTool.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ResponseTool_Click);
    //        // 
    //        // grpFormattingTools
    //        // 
    //        this.grpFormattingTools.Items.Add(this.menuTranscript);
    //        this.grpFormattingTools.Items.Add(this.menu5);
    //        this.grpFormattingTools.Items.Add(this.menu3);
    //        this.grpFormattingTools.Items.Add(this.btnSmrtQuotes);
    //        this.grpFormattingTools.Items.Add(this.btnInsertNBS);
    //        this.grpFormattingTools.Items.Add(this.btnBlockQuotes);
    //        this.grpFormattingTools.Items.Add(this.menu4);
    //        this.grpFormattingTools.Items.Add(this.btnRemoveLineBreaks);
    //        this.grpFormattingTools.Items.Add(this.HyphenToEnDashbtn);
    //        this.grpFormattingTools.Label = "Formatting Tools";
    //        this.grpFormattingTools.Name = "grpFormattingTools";
    //        // 
    //        // menuTranscript
    //        // 
    //        this.menuTranscript.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.menuTranscript.Image = global::LitKit1.Properties.Resources.PasteTranscript_32px;
    //        this.menuTranscript.Items.Add(this.btnBlockTranscript);
    //        this.menuTranscript.Items.Add(this.btnInLineTranscript);
    //        this.menuTranscript.Label = "Paste Transcript Text";
    //        this.menuTranscript.Name = "menuTranscript";
    //        this.menuTranscript.OfficeImageId = "MasterDocumentShow";
    //        this.menuTranscript.ScreenTip = "Paste Text Copied from a Transcript";
    //        this.menuTranscript.ShowImage = true;
    //        this.menuTranscript.SuperTip = resources.GetString("menuTranscript.SuperTip");
    //        // 
    //        // btnBlockTranscript
    //        // 
    //        this.btnBlockTranscript.Label = "Paste Transcript Text as Block Quote";
    //        this.btnBlockTranscript.Name = "btnBlockTranscript";
    //        this.btnBlockTranscript.OfficeImageId = "MailMergeMergeFieldInsert";
    //        this.btnBlockTranscript.ShowImage = true;
    //        this.btnBlockTranscript.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnBlockTranscript_Click);
    //        // 
    //        // btnInLineTranscript
    //        // 
    //        this.btnInLineTranscript.Label = "Paste Transcript Text as In-Text Quote";
    //        this.btnInLineTranscript.Name = "btnInLineTranscript";
    //        this.btnInLineTranscript.OfficeImageId = "MailMergeGreetingLineInsert";
    //        this.btnInLineTranscript.ShowImage = true;
    //        this.btnInLineTranscript.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnInLineTranscript_Click);
    //        // 
    //        // menu5
    //        // 
    //        this.menu5.Image = global::LitKit1.Properties.Resources.LatinWords_16px;
    //        this.menu5.Items.Add(this.btnLatin);
    //        this.menu5.Items.Add(this.button1);
    //        this.menu5.Label = "Latin Words";
    //        this.menu5.Name = "menu5";
    //        this.menu5.ScreenTip = "Italicize Latin Words";
    //        this.menu5.ShowImage = true;
    //        this.menu5.SuperTip = "Italicizes or un-italicizes Latin words and phrases throughout the document.";
    //        // 
    //        // btnLatin
    //        // 
    //        this.btnLatin.Label = "Italicize Latin";
    //        this.btnLatin.Name = "btnLatin";
    //        this.btnLatin.ShowImage = true;
    //        this.btnLatin.SuperTip = "Italicizes Latin words and phrases commonly used in the legal world";
    //        this.btnLatin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLatin_Click);
    //        // 
    //        // button1
    //        // 
    //        this.button1.Label = "Un-italicize Latin";
    //        this.button1.Name = "button1";
    //        this.button1.ShowImage = true;
    //        this.button1.SuperTip = "Removes italics from Latin words and phrases commonly used in the legal world";
    //        this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.UnItalicizeLatin_Click_1);
    //        // 
    //        // menu3
    //        // 
    //        this.menu3.Image = global::LitKit1.Properties.Resources.SenteceSpacing_16px;
    //        this.menu3.Items.Add(this.btnSingleSpace);
    //        this.menu3.Items.Add(this.btnDoubleSpace);
    //        this.menu3.Label = "Sentence Spacing";
    //        this.menu3.Name = "menu3";
    //        this.menu3.ScreenTip = "Change Spacing Between Sentences";
    //        this.menu3.ShowImage = true;
    //        this.menu3.SuperTip = "Adds or removes a double space between sentences in the document.";
    //        // 
    //        // btnSingleSpace
    //        // 
    //        this.btnSingleSpace.Label = "Single Space Between Sentences";
    //        this.btnSingleSpace.Name = "btnSingleSpace";
    //        this.btnSingleSpace.ShowImage = true;
    //        this.btnSingleSpace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSingleSpace_Click);
    //        // 
    //        // btnDoubleSpace
    //        // 
    //        this.btnDoubleSpace.Label = "Double Space Between Sentences";
    //        this.btnDoubleSpace.Name = "btnDoubleSpace";
    //        this.btnDoubleSpace.ShowImage = true;
    //        this.btnDoubleSpace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnDoubleSpace_Click);
    //        // 
    //        // btnSmrtQuotes
    //        // 
    //        this.btnSmrtQuotes.Image = global::LitKit1.Properties.Resources.SmartQuotes_16px;
    //        this.btnSmrtQuotes.Label = "Smart Quotes";
    //        this.btnSmrtQuotes.Name = "btnSmrtQuotes";
    //        this.btnSmrtQuotes.ScreenTip = "Replace With Smart Quotes";
    //        this.btnSmrtQuotes.ShowImage = true;
    //        this.btnSmrtQuotes.SuperTip = "Replaces dumb (stright) quotes and apostrophes with smart (curly) versions throug" +
    //"hout the document";
    //        this.btnSmrtQuotes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSmrtQuotes_Click);
    //        // 
    //        // btnInsertNBS
    //        // 
    //        this.btnInsertNBS.Image = global::LitKit1.Properties.Resources.InsertNBS_16px;
    //        this.btnInsertNBS.Label = "Insert NBS";
    //        this.btnInsertNBS.Name = "btnInsertNBS";
    //        this.btnInsertNBS.ScreenTip = "Insert Non-Breaking Spaces";
    //        this.btnInsertNBS.ShowImage = true;
    //        this.btnInsertNBS.SuperTip = "Scans the document and inserts non-breaking spaced where preferred but not curren" +
    //"tly present. Examples include: before numerals, between days and months, and fol" +
    //"lowing common titles such as Ms.";
    //        this.btnInsertNBS.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnInsertNBS_Click);
    //        // 
    //        // btnBlockQuotes
    //        // 
    //        this.btnBlockQuotes.Label = "Block Quotes";
    //        this.btnBlockQuotes.Name = "btnBlockQuotes";
    //        this.btnBlockQuotes.ScreenTip = "Block Quotes";
    //        this.btnBlockQuotes.Visible = false;
    //        this.btnBlockQuotes.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnBlockQuotes_Click);
    //        // 
    //        // menu4
    //        // 
    //        this.menu4.Image = global::LitKit1.Properties.Resources.OxfordComma_16px;
    //        this.menu4.Items.Add(this.btnAddOxfordComma);
    //        this.menu4.Items.Add(this.btnRemoveOxfordComma);
    //        this.menu4.Label = "Oxford Comma";
    //        this.menu4.Name = "menu4";
    //        this.menu4.ScreenTip = "Oxford Comma";
    //        this.menu4.ShowImage = true;
    //        this.menu4.SuperTip = "Adds or removes Oxford (serialized) commas within the document.";
    //        this.menu4.Visible = false;
    //        // 
    //        // btnAddOxfordComma
    //        // 
    //        this.btnAddOxfordComma.Label = "Add Oxford Commas";
    //        this.btnAddOxfordComma.Name = "btnAddOxfordComma";
    //        this.btnAddOxfordComma.ShowImage = true;
    //        this.btnAddOxfordComma.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOxfordComma_Click);
    //        // 
    //        // btnRemoveOxfordComma
    //        // 
    //        this.btnRemoveOxfordComma.Label = "Remove Oxford Commas";
    //        this.btnRemoveOxfordComma.Name = "btnRemoveOxfordComma";
    //        this.btnRemoveOxfordComma.ShowImage = true;
    //        this.btnRemoveOxfordComma.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemoveOxfordComma_Click);
    //        // 
    //        // btnRemoveLineBreaks
    //        // 
    //        this.btnRemoveLineBreaks.Label = "Remove Line Breaks";
    //        this.btnRemoveLineBreaks.Name = "btnRemoveLineBreaks";
    //        this.btnRemoveLineBreaks.OfficeImageId = "BreakInsertDialog";
    //        this.btnRemoveLineBreaks.ScreenTip = "Remove Line Breaks from Selection";
    //        this.btnRemoveLineBreaks.ShowImage = true;
    //        this.btnRemoveLineBreaks.SuperTip = "Remove Line Breaks ( new line returns) from the current selection.";
    //        this.btnRemoveLineBreaks.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnRemoveLineBreaks_Click);
    //        // 
    //        // HyphenToEnDashbtn
    //        // 
    //        this.HyphenToEnDashbtn.Label = "Replace with En-Dash";
    //        this.HyphenToEnDashbtn.Name = "HyphenToEnDashbtn";
    //        this.HyphenToEnDashbtn.OfficeImageId = "HyphenationMenu";
    //        this.HyphenToEnDashbtn.ScreenTip = "Replace Hyphens with an En-Dash (–) Between Number Ranges";
    //        this.HyphenToEnDashbtn.ShowImage = true;
    //        this.HyphenToEnDashbtn.SuperTip = "Replace hyphens in the middle of number ranges with En-Dashes: #-# to #–#";
    //        this.HyphenToEnDashbtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.HyphenToEnDashbtn_Click);
    //        // 
    //        // grpSupport
    //        // 
    //        this.grpSupport.DialogLauncher = ribbonDialogLauncherImpl5;
    //        this.grpSupport.Items.Add(this.btnHowTo);
    //        this.grpSupport.Items.Add(this.CustomerSupport);
    //        this.grpSupport.Items.Add(this.TestButton1);
    //        this.grpSupport.Label = "Support";
    //        this.grpSupport.Name = "grpSupport";
    //        this.grpSupport.DialogLauncherClick += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Support_DialogLauncherClick);
    //        // 
    //        // btnHowTo
    //        // 
    //        this.btnHowTo.Label = "User Guide";
    //        this.btnHowTo.Name = "btnHowTo";
    //        this.btnHowTo.OfficeImageId = "FunctionsLogicalInsertGallery";
    //        this.btnHowTo.ScreenTip = "LitKit User Guide";
    //        this.btnHowTo.ShowImage = true;
    //        this.btnHowTo.SuperTip = "Opens the online User Guide, which contains Prelimine\'s \"How-To\" library, in your" +
    //" browser.";
    //        this.btnHowTo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.HowTo_Click);
    //        // 
    //        // CustomerSupport
    //        // 
    //        this.CustomerSupport.Image = global::LitKit1.Properties.Resources.Support_16px;
    //        this.CustomerSupport.Label = "Contact Us";
    //        this.CustomerSupport.Name = "CustomerSupport";
    //        this.CustomerSupport.OfficeImageId = "TechnicalSupport";
    //        this.CustomerSupport.ScreenTip = "Contact Us";
    //        this.CustomerSupport.ShowImage = true;
    //        this.CustomerSupport.SuperTip = "Send an email to support@prelimine.com.";
    //        this.CustomerSupport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CustomerSupport_Click);
    //        // 
    //        // TestButton1
    //        // 
    //        this.TestButton1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
    //        this.TestButton1.Image = global::LitKit1.Properties.Resources.Group;
    //        this.TestButton1.Label = "TestClass Button";
    //        this.TestButton1.Name = "TestButton1";
    //        this.TestButton1.OfficeImageId = "TipWizardHelp";
    //        this.TestButton1.ShowImage = true;
    //        this.TestButton1.Visible = false;
    //        this.TestButton1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Test_Button_Click);
    //        // 
    //        // MainRibbon
    //        // 
    //        this.Name = "MainRibbon";
    //        this.RibbonType = "Microsoft.Word.Document";
    //        this.Tabs.Add(this.MainTab);
    //        this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
    //        grpShortcuts.ResumeLayout(false);
    //        grpShortcuts.PerformLayout();
    //        this.MainTab.ResumeLayout(false);
    //        this.MainTab.PerformLayout();
    //        this.grpRedactions.ResumeLayout(false);
    //        this.grpRedactions.PerformLayout();
    //        this.grpCitationsTool.ResumeLayout(false);
    //        this.grpCitationsTool.PerformLayout();
    //        this.grpAnsRes.ResumeLayout(false);
    //        this.grpAnsRes.PerformLayout();
    //        this.grpFormattingTools.ResumeLayout(false);
    //        this.grpFormattingTools.PerformLayout();
    //        this.grpSupport.ResumeLayout(false);
    //        this.grpSupport.PerformLayout();
    //        this.ResumeLayout(false);

        }

        #endregion

        //internal Microsoft.Office.Tools.Ribbon.RibbonTab MainTab;
        //internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpCitationsTool;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton ClipboardButton;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnExhibitTool;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton TestButton1;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnKeepWithNext;
        //internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpSupport;
        //internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpAnsRes;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPinCite;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemovePinCite;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnIndexOfExhibits;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton CustomerSupport;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu1;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPilcrow;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSectionMark;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnTM;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCopyright;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNBS;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNBHyphen;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNDash;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnMDash;
        //internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpRedactions;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton unmarkRedact;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnClearAllRedactions;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu2;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton redactedPDF;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton unredactedPDF;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnInLineTranscript;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuTranscript;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnBlockTranscript;
        //internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btnShowHide;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSmrtQuotes;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnBlockQuotes;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnDoubleSpace;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnInsertNBS;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLatin;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddOxfordComma;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu3;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSingleSpace;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu4;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemoveOxfordComma;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu5;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton testExhibits;
        //internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpFormattingTools;
        //internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton togglebtnSmallCaps;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnReplace;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnHighlightedPDF;
        //internal Microsoft.Office.Tools.Ribbon.RibbonMenu menu6;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemoveCiteLocks;
        //internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tglMarkRedaction;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnResposeTool;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnHowTo;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemoveLineBreaks;
        //internal Microsoft.Office.Tools.Ribbon.RibbonButton HyphenToEnDashbtn;
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon Ribbon1
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
