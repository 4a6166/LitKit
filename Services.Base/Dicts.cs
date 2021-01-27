using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.Base
{
    public class Dicts
    {
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetExpressionFilePath(string filename)
        {
            try
            {
                var personalDict = GetPersonalDict(filename);
                if (File.Exists(personalDict))
                {
                    return personalDict;
                }
                else
                {
                    var standardDictPath = getStandardDict(filename);
                    if (copyStandardToPersonal(standardDictPath, personalDict))
                    {
                        return personalDict;
                    }
                    else return standardDictPath;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                MessageBox.Show("Personal dictionary Could not be loaded. Applying changes using standard dictionary.");
                return getStandardDict(filename); 
            }
        }

        public static string GetPersonalDict(string filename)
        {
            try
            {
                string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                filePath = (filePath + @"\Prelimine\" + filename);
                string path = Convert.ToString(filePath);

                return path;

            }
            catch (SecurityException e)
            {
                Log.Error(e.Message);
                MessageBox.Show(e.Message);
                return null;
            }
            catch
            {
                return null;
            }
        }

        private static string getStandardDict(string filename)
        {

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            string Parent = Directory.GetCurrentDirectory();            
            
            string result = Parent + @"\Services\" + filename; 

            if (File.Exists(result))
            {
                return result;
            }
            else return null;

        }

        private static bool copyStandardToPersonal(string pathToCopy, string pathToPaste)
        {
            try
            {
                File.Copy(pathToCopy, pathToPaste, false);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                MessageBox.Show(e.Message);

                return false;
            }            
        }
    }
}
