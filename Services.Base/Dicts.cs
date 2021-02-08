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

        public static string GetExpressionFilePath(string filename, out bool pulledStandardDict)
        {
            pulledStandardDict = false;
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
                    else
                    {
                        pulledStandardDict = true;
                        return standardDictPath;
                    }
                }
            }
            catch (Exception e)
            {
                pulledStandardDict = true;
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

        public static string getStandardDict(string filename)
        {

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            string Parent = Directory.GetCurrentDirectory();            
            
            string result = Parent + @"\Services\" + filename;

            if (File.Exists(result))
            {
                return result;
            }
            else
            {
                // Path for debug purposes. Should not exist/be activated in installed version
                result = @"C:\Users\Jake\Google Drive (jacob.field@prelimine.com)\repos\LitKit1_git\LitKit1\Services.RibbonButtons\Dictionaries\" + filename;

                if(!File.Exists(result))
                {
                    result = null;
                }
                return result;
            }

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

        public static bool UpdatePersonalDict(string filename, string UpdateText, bool pulledStandardDict)
        {
            if (pulledStandardDict)
            {
                MessageBox.Show("There was an error collecting the dictionary. Please try again.");
                Log.Info("Error updating personal dictionary " + filename);
                return false;
            }
            else
            {
                string path = GetPersonalDict(filename);
                StreamWriter sw = new StreamWriter(path, append: false);

                sw.Write(UpdateText);
                sw.Close();
                return true;

            }
        }

    }
}
