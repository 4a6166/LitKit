/* 
 * © Copyright 2021, Prelimine LLC., All rights reserved. 
 * Use and reproduction of code contained in the associated program and DLLs are subject to the applicable license agreement.
 */



using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using Microsoft.Office.Core;

namespace LitKit1
{
    public partial class ThisAddIn
    {
        public Microsoft.Office.Tools.CustomTaskPane CitationTaskPane;
        /// <summary>
        /// Needed to get the correct Citation Pane for each document window. 
        /// </summary>
        public Dictionary<object, Microsoft.Office.Tools.CustomTaskPane> CitationPanes = new Dictionary<object, Microsoft.Office.Tools.CustomTaskPane>();

        public Microsoft.Office.Tools.CustomTaskPane ResponseTaskPane;
        /// <summary>
        /// Needed to get the correct Response Pane for each document window. 
        /// </summary>
        public Dictionary<object, Microsoft.Office.Tools.CustomTaskPane> ResponsePanes = new Dictionary<object, Microsoft.Office.Tools.CustomTaskPane>();




        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("New session started ***********"); // Logs stored in C:\Users\[user]\AppData\Roaming\Prelimine\LitKit-log


            try { AddTaskPanes(Application.ActiveDocument); }
            catch { }

            ApplicationEvents4_Event app = (ApplicationEvents4_Event)Application;
            app.DocumentOpen += new ApplicationEvents4_DocumentOpenEventHandler(Application_DocumentOpen);
            app.NewDocument += new ApplicationEvents4_NewDocumentEventHandler(Application_NewDocument);
            app.DocumentBeforeClose += new ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentClose);

        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void AddTaskPanes(Word.Document doc)
        {
            log.Info("AddTaskPane run start");

            ClearTaskPanes(doc.ActiveWindow);
            AddCitationControlMain(doc.ActiveWindow);
            AddResponseControlMain(doc.ActiveWindow);

        }

        public void AddCitationControlMain(object window)
        {
            log.Info("AddCitationControlMain run start");

            var CitationMain = new ControlsWPF.HoldingControl();
                /*new ControlsWPF.HoldingControl(new ControlsWPF.Citation.CiteMain());*/
            CitationTaskPane = this.CustomTaskPanes.Add(CitationMain, "LitKit Citation Tool", window);
            CitationMain.Dock = System.Windows.Forms.DockStyle.Fill;
            CitationTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;
            CitationTaskPane.Width = 350;

            CitationPanes.Add(window, CitationTaskPane);
        }

        public void AddResponseControlMain(object window)
        {
            log.Info("AddCitationControlMain run start");

            var ResponseMain = new ControlsWPF.HoldingControl();
            /*new ControlsWPF.HoldingControl(new ControlsWPF.Response.ResponseMain());*/
            ResponseTaskPane = this.CustomTaskPanes.Add(ResponseMain, "LitKit Response Tool", window);
            ResponseMain.Dock = System.Windows.Forms.DockStyle.Fill;
            ResponseTaskPane.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;
            ResponseTaskPane.Width = 350;


            ResponsePanes.Add(window, ResponseTaskPane);
        }



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
            //ClearTaskPanes(doc.ActiveWindow);  /*Causes panes to not be loadable if doc is closed then cancelled on save.*/
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
