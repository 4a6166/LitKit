﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Services.Exhibit;
using LitKit1.Controls;
using Microsoft.Office.Interop.Word;
using LitKit1.Controls.ExhibitControls;
using System.Runtime.InteropServices;
using Services;
using Microsoft.Office.Core;
using LitKit1.Controls.AnsResControls;
using Services.Licensing;

namespace LitKit1
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

            try { AddTaskPanes(Application.ActiveDocument); }
            catch { }

            ApplicationEvents4_Event app = (ApplicationEvents4_Event)Application;
            app.DocumentOpen += new ApplicationEvents4_DocumentOpenEventHandler(Application_DocumentOpen);
            app.NewDocument += new ApplicationEvents4_NewDocumentEventHandler(Application_NewDocument);
            app.DocumentBeforeClose += new ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentClose);

        }


        private void AddTaskPanes(Word.Document doc)
        {
            ClearTaskPanes(doc.ActiveWindow);
            AddExhibitControlMain(doc.ActiveWindow);
            AddAnsResControlMain(doc.ActiveWindow);

        }

        public void AddExhibitControlMain(object window)
        {
            ExhibitMain = new ctrlExhibitMain();
            ExhibitTaskPane = this.CustomTaskPanes.Add(ExhibitMain, "LitKit Exhibit Tool", window);
            ExhibitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            ExhibitTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;
            ExhibitTaskPane.Width = 350;
            

            ExhibitPanes.Add(window, ExhibitTaskPane);
        }

        /// <summary>
        /// Needed to get the correct Exhibit Pane for each document window. Call Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.ExhibitPanes[_app.ActiveWindow]; and ActivePane.Control.Controls.Clear(); when trying to update the controls for a pane.
        /// </summary>
        public Dictionary<object, Microsoft.Office.Tools.CustomTaskPane> ExhibitPanes = new Dictionary<object, Microsoft.Office.Tools.CustomTaskPane>();

        public void AddAnsResControlMain(object window)
        {
            AnsResMain = new ctrlAnsResMain();
            AnsResTaskPane = this.CustomTaskPanes.Add(AnsResMain, "LitKit Response Tool", window);
            AnsResMain.Dock = System.Windows.Forms.DockStyle.Fill;
            AnsResTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionLeft;
            AnsResTaskPane.Width = 350;

            AnsResPanes.Add(window, AnsResTaskPane);
        }

        /// <summary>
        /// Needed to get the correct Answer/Response Pane for each document window. Call Tools.CustomTaskPane ActivePane = Globals.ThisAddIn.AnsResPanes[_app.ActiveWindow]; and ActivePane.Control.Controls.Clear(); when trying to update the controls for a pane.
        /// </summary>
        public Dictionary<object, Microsoft.Office.Tools.CustomTaskPane> AnsResPanes= new Dictionary<object, Microsoft.Office.Tools.CustomTaskPane>();

        #region Adding Task Pane Controls

        public ctrlExhibitMain ExhibitMain;
        public Microsoft.Office.Tools.CustomTaskPane ExhibitTaskPane;

        public ctrlAnsResMain AnsResMain;
        public Microsoft.Office.Tools.CustomTaskPane AnsResTaskPane;
        // Need to call AnsResPanes.Controls.Control.Visible = true;
        
        #endregion

        private void Application_DocumentOpen(Word.Document Doc)
        {
            AddTaskPanes(Doc);
        }

        private void Application_NewDocument(Word.Document Doc)
        {
            AddTaskPanes(Doc);
        }
        private void Application_DocumentClose(Word.Document doc, ref bool Cancel)
        {
            ClearTaskPanes(doc.ActiveWindow);
            Cancel = false;
        }



        public void ClearTaskPanes(Window window)
        {
            for (var i = 0; i < CustomTaskPanes.Count; i++)
            {
                if (CustomTaskPanes[i].Window == window)
                {
                    CustomTaskPanes.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ReturnFocus()
        {
            Word.Application app = Globals.ThisAddIn.Application as Word.Application;
            Word.Window window = app.ActiveWindow;
            window.SetFocus();
            if (window != null) Marshal.ReleaseComObject(window);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
