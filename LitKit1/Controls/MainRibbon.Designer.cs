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
            this.ClipboardButton = this.Factory.CreateRibbonButton();
            this.btnKeepWithNext = this.Factory.CreateRibbonButton();
            this.CustomerSupport = this.Factory.CreateRibbonButton();
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
            this.MainTab = this.Factory.CreateRibbonTab();
            this.grpExhibitTool = this.Factory.CreateRibbonGroup();
            this.ExhibitTestButton = this.Factory.CreateRibbonButton();
            this.btnPinCite = this.Factory.CreateRibbonButton();
            this.btnRemovePinCite = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.grpAnsRes = this.Factory.CreateRibbonGroup();
            this.splitbtnResposeTool = this.Factory.CreateRibbonSplitButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.RedactionsGroup = this.Factory.CreateRibbonGroup();
            this.markRedact = this.Factory.CreateRibbonButton();
            this.unmarkRedact = this.Factory.CreateRibbonButton();
            this.btnClearAllRedactions = this.Factory.CreateRibbonButton();
            this.menu2 = this.Factory.CreateRibbonMenu();
            this.redactedPDF = this.Factory.CreateRibbonButton();
            this.unredactedPDF = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.ExhibitChangeControl = this.Factory.CreateRibbonButton();
            grpShortcuts = this.Factory.CreateRibbonGroup();
            grpShortcuts.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.grpExhibitTool.SuspendLayout();
            this.grpAnsRes.SuspendLayout();
            this.RedactionsGroup.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpShortcuts
            // 
            grpShortcuts.Items.Add(this.ClipboardButton);
            grpShortcuts.Items.Add(this.btnKeepWithNext);
            grpShortcuts.Items.Add(this.CustomerSupport);
            grpShortcuts.Items.Add(this.menuTranscript);
            grpShortcuts.Items.Add(this.menu1);
            grpShortcuts.Label = "Shortcuts";
            grpShortcuts.Name = "grpShortcuts";
            // 
            // ClipboardButton
            // 
            this.ClipboardButton.Label = "View Clipboard";
            this.ClipboardButton.Name = "ClipboardButton";
            this.ClipboardButton.OfficeImageId = "ShowClipboard";
            this.ClipboardButton.ShowImage = true;
            this.ClipboardButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ClipboardButton_Click);
            // 
            // btnKeepWithNext
            // 
            this.btnKeepWithNext.Label = "Keep With Next";
            this.btnKeepWithNext.Name = "btnKeepWithNext";
            this.btnKeepWithNext.OfficeImageId = "StylesStyleSeparator";
            this.btnKeepWithNext.ShowImage = true;
            this.btnKeepWithNext.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnKeepWithNext_Click);
            // 
            // CustomerSupport
            // 
            this.CustomerSupport.Label = "Contact Us";
            this.CustomerSupport.Name = "CustomerSupport";
            this.CustomerSupport.OfficeImageId = "TechnicalSupport";
            this.CustomerSupport.ShowImage = true;
            this.CustomerSupport.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CustomerSupport_Click);
            // 
            // menuTranscript
            // 
            this.menuTranscript.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.menuTranscript.Items.Add(this.btnBlockTranscript);
            this.menuTranscript.Items.Add(this.btnInLineTranscript);
            this.menuTranscript.Label = "Paste Transcript Text";
            this.menuTranscript.Name = "menuTranscript";
            this.menuTranscript.OfficeImageId = "MasterDocumentShow";
            this.menuTranscript.ShowImage = true;
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
            // MainTab
            // 
            this.MainTab.Groups.Add(grpShortcuts);
            this.MainTab.Groups.Add(this.grpExhibitTool);
            this.MainTab.Groups.Add(this.grpAnsRes);
            this.MainTab.Groups.Add(this.RedactionsGroup);
            this.MainTab.Groups.Add(this.group1);
            this.MainTab.Label = "LitKit";
            this.MainTab.Name = "MainTab";
            this.MainTab.Position = this.Factory.RibbonPosition.AfterOfficeId("TabHome");
            // 
            // grpExhibitTool
            // 
            this.grpExhibitTool.Items.Add(this.ExhibitTestButton);
            this.grpExhibitTool.Items.Add(this.btnPinCite);
            this.grpExhibitTool.Items.Add(this.btnRemovePinCite);
            this.grpExhibitTool.Items.Add(this.button2);
            this.grpExhibitTool.Label = "Exhibits";
            this.grpExhibitTool.Name = "grpExhibitTool";
            // 
            // ExhibitTestButton
            // 
            this.ExhibitTestButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ExhibitTestButton.Label = "Exhibit Tool";
            this.ExhibitTestButton.Name = "ExhibitTestButton";
            this.ExhibitTestButton.OfficeImageId = "BaselineSave";
            this.ExhibitTestButton.ShowImage = true;
            this.ExhibitTestButton.SuperTip = "Display the LitKit Exhibit Tool";
            this.ExhibitTestButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ExhibitTestButton_Click);
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
            // button2
            // 
            this.button2.Label = "Index of Exhibits";
            this.button2.Name = "button2";
            this.button2.OfficeImageId = "ListSetNumberingValue";
            this.button2.ShowImage = true;
            this.button2.SuperTip = "Inserts a table index of exhibits in the document at your current selection. This" +
    " table will not be updated when Exhibits are moved, edited, or deleted.";
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
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
            this.splitbtnResposeTool.Items.Add(this.button3);
            this.splitbtnResposeTool.Items.Add(this.button4);
            this.splitbtnResposeTool.Label = "Answer and Response Tool";
            this.splitbtnResposeTool.Name = "splitbtnResposeTool";
            this.splitbtnResposeTool.OfficeImageId = "ReplyAllWithInstantMessage";
            this.splitbtnResposeTool.SuperTip = "Display the LitKit Discovery Response Tool";
            this.splitbtnResposeTool.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Label = "Update Party or Parties";
            this.button3.Name = "button3";
            this.button3.OfficeImageId = "InviteAttendees";
            this.button3.ShowImage = true;
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Label = "Customize Language";
            this.button4.Name = "button4";
            this.button4.OfficeImageId = "OmsCustomizeLayout";
            this.button4.ShowImage = true;
            this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button4_Click);
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
            this.markRedact.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.markRedact_Click);
            // 
            // unmarkRedact
            // 
            this.unmarkRedact.Label = "Unmark Selection";
            this.unmarkRedact.Name = "unmarkRedact";
            this.unmarkRedact.OfficeImageId = "DatasheetColumnRename";
            this.unmarkRedact.ShowImage = true;
            this.unmarkRedact.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.unmarkRedact_Click);
            // 
            // btnClearAllRedactions
            // 
            this.btnClearAllRedactions.Label = "Clear All Redactions";
            this.btnClearAllRedactions.Name = "btnClearAllRedactions";
            this.btnClearAllRedactions.OfficeImageId = "ClearFormats";
            this.btnClearAllRedactions.ShowImage = true;
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
            // group1
            // 
            this.group1.Items.Add(this.ExhibitChangeControl);
            this.group1.Label = "Test Buttons";
            this.group1.Name = "group1";
            // 
            // ExhibitChangeControl
            // 
            this.ExhibitChangeControl.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ExhibitChangeControl.Image = global::LitKit1.Properties.Resources.Group;
            this.ExhibitChangeControl.Label = "TestClass Button";
            this.ExhibitChangeControl.Name = "ExhibitChangeControl";
            this.ExhibitChangeControl.OfficeImageId = "TipWizardHelp";
            this.ExhibitChangeControl.ShowImage = true;
            this.ExhibitChangeControl.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ExhibitChangeControl_Click);
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
            this.grpExhibitTool.ResumeLayout(false);
            this.grpExhibitTool.PerformLayout();
            this.grpAnsRes.ResumeLayout(false);
            this.grpAnsRes.PerformLayout();
            this.RedactionsGroup.ResumeLayout(false);
            this.RedactionsGroup.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab MainTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpExhibitTool;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ClipboardButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ExhibitTestButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ExhibitChangeControl;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnKeepWithNext;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpAnsRes;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPinCite;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRemovePinCite;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CustomerSupport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
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
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon Ribbon1
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
