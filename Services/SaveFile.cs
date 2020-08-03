using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services
{
    public class SaveFile
    {
        public SaveFile()
        {
            SaveFilePath();
            CheckIfFileIsAvailable();
        }


        public string Path { get; private set; }
        public void SaveFilePath()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF|*.pdf";
            saveFileDialog1.Title = "Export Redacted PDF";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                Path = saveFileDialog1.FileName;
            }
            else
            { Path = null; }

        }

        public bool FileAvailable { get; private set; }

        public void CheckIfFileIsAvailable()
        {
            FileInfo file = new FileInfo(Path);
            if (!file.Exists)
            { FileAvailable = true; }
            else
            {
                try
                {
                    using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                        FileAvailable = true;
                    }
                }
                catch (IOException)
                {
                    //the file is unavailable because it is:
                        //still being written to
                        //or being processed by another thread
                        //or does not exist (has already been processed)
                    FileAvailable = false;
                    MessageBox.Show("File is open in another window or program. Please close the file and try again.");

                }
            }
        }

        public string FileMarking { get; set; }

    }
}
