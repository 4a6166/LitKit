using LicenseSpring;
using System;
using System.IO;
using System.Windows.Forms;

namespace Services.License
{
    /// <summary>
    /// refer to https://docs.licensespring.com/docs/initializing-the-sdk-1
    /// </summary>
    public class LS
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ILicenseManager _licenseManager;


        public LS()
        {
            log4net.Config.XmlConfigurator.Configure();

            _licenseManager = getInstance();
        }
        private ILicenseManager getInstance()
        {

            var configuration = new LicenseSpringConfiguration(
                  apiKey: "1c6cf10d-58eb-4e40-9a44-aa3bf36414e2",
                  sharedKey: "J-58rsdq7zRa6KYv9MD4hS1Wj7IQyxLmC4y9rIAN5mY",
                  productCode: "test1",
                  appName: "LitKit Test",
                  appVersion: "0.0.01Test"
                  );

            var licenseManager = LicenseManager.GetInstance();

            licenseManager.Initialize(configuration);

            return licenseManager;

        }     

        public ILicense GetLicense()
        {
            ILicense license = _licenseManager.CurrentLicense();

            if (license == null)
            {
                Log.Info("License returned null");
            }
            else
            {
                try
                {
                    Log.Info("License is valid: " + license.IsValid());
                    Log.Info("License days remaining: " + license.DaysRemainingUTC());
                }
                catch { Log.Error("License Information not avaialable"); }
            }
            return license;
        }

        public string GetTrialKey(string email = "")
        {
            string trialLicense = null;
            try
            {
                //trialKey = _licenseManager.GetTrialKey("someemail@gmail.com");
                trialLicense = _licenseManager.GetTrialLicense(email);
                return trialLicense;
            }
            catch (LicenseSpringException e)
            {
                // something went wrong, check specific exception type
                Log.Error(e.Message);
                return e.Message;
            }

        }

        public void ActivateLicenseKey(string LicenseKey)
        {
            Log.Info("License Key activating");
            try
            {
                ILicense activated = _licenseManager.ActivateLicense(LicenseKey);
            }
            catch(LicenseActivationException e)
            {
                Log.Error(e.Message);
            }
        }

        public void ActivateLicenseKeyOffline(string OfflineLicensePath)
        {
            try
            {
                ILicense activated = _licenseManager.ActivateLicenseOffline(OfflineLicensePath);
            }
            catch (ActivationFileException e)
            {
                Log.Error(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Log.Error(e.Message);
            }
        }

        public string CreateLicenseOffline(string LicenseKey, string OfflineLicensePath)
        {
            try
            {
                string path = _licenseManager.GetOfflineActivationFile(LicenseKey, OfflineLicensePath);
                return path;
            }
            catch (InvalidOperationException e)
            {
                Log.Error(e.Message);
                return e.Message;
            }
        }

        public void GetInstallationFile(string LicenseKey, string appVersion = null)
        {
            var installationFile = _licenseManager.GetInstallationFile(LicenseKey, appVersion);

            //System.Diagnostics.Process.Start(installationFile.Url);

            new WebBrowser().Navigate(installationFile.Url);

        }

    }
}
