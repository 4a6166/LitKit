namespace Services
{
    public abstract class BaseService
    {
        protected const string NEW_LINE = "\r\n";
        protected const int TRUE = -1;
        protected const int FALSE = 0;

        private string[] _allStates;
        protected string[] AllStates
        {
            get
            {
                return _allStates ??
                  (
                    _allStates = new string[]
                    {
              "CALIFORNIA",
              "TEXAS",
              "NEW YORK",
              "ILLINOIS"
                    }
                  );
            }
        }

        private string[] _courtInfoKeywords;
        protected string[] CourtInfoKeywords
        {
            get
            {
                return _courtInfoKeywords ??
                  (
                    _courtInfoKeywords = new string[]
                    {
              "DIVISION",
                    }
                  );
            }
        }

        private string[] _stopKeywords;
        protected string[] StopKeywords
        {
            get
            {
                return _stopKeywords ??
                  (
                    _stopKeywords = new string[]
                    {
              "PLAINTIFF",
              "JURISDICTION"
                    }
                  );

            }
        }

        private string[] _plaintiffKeywords;
        protected string[] PlaintiffKeywords
        {
            get
            {
                return _plaintiffKeywords ??
                  (
                    _plaintiffKeywords = new string[]
                    {
              "PLAINTIFF,",
              "PLAINTIFFS,",
              "PETITIONER,",
              "PETITIONERS,",
              "MOVANT,",
              "MOVANTS,",
              "APPELLANT,",
              "APPELLANTS,",
              "PLAINTIFF-APPELLEE,",
              "PLAINTIFFS-APPELLEES,",
              "PLAINTIFF-APPELLANT,",
              "PLAINTIFF-APPELLANTS,",
              "APPLICANT,",
              "APPLICANTS,"
                    }
                  );
            }
        }

        private string[] _defendantKeywords;
        protected string[] DefendantKeywords
        {
            get
            {

                return _defendantKeywords ??
                  (
                    _defendantKeywords = new string[]
                    {
              "DEFENDANT.",
              "DEFENDANTS.",
              "RESPONDENT.",
              "RESPONDENTS.",
              "APPELLEE.",
              "APPELLEES.",
              "DEFENDANT-APPELLEE.",
              "DEFENDANTS-APPELLEES.",
              "DEFENDANTS-APPELLANTS.",
              "DEFENDANT-APPELLANT."
                    }
                  );
            }
        }

        private string[] _versusKeywords;
        protected string[] VersusKeywords
        {
            get
            {
                return _versusKeywords ?? (_versusKeywords = new string[] { "V.", "V", "VS", "VS.", "VERSUS", "AGAINST", "-AGAINST-" });
            }
        }

        /*
         #region Constructor
    public CommonBase()
    {
      XmlFileName = Directory.GetCurrentDirectory() + @"\XmlOutput.xml";
      XsdFileName = Directory.GetCurrentDirectory() + @"\XmlOutput.xsd";
    }
    #endregion

    #region Properties
    private string _ResultText;

    [XmlIgnore]
    public string ResultText
    {
      get { return _ResultText; }
      set {
        _ResultText = value;
        RaisePropertyChanged("ResultText");
      }
    }

    private string _XmlFileName;

    [XmlIgnore]
    public string XmlFileName
    {
      get { return _XmlFileName; }
      set {
        _XmlFileName = value;
        RaisePropertyChanged("XmlFileName");
      }
    }
    
    private string _XsdFileName;

    [XmlIgnore]
    public string XsdFileName
    {
      get { return _XsdFileName; }
      set {
        _XsdFileName = value;
        RaisePropertyChanged("XsdFileName");
      }
    }
    #endregion

    #region ReadXmlFile Method
    public void ReadXmlFile()
    {
      ResultText = string.Empty;

      ResultText = ReadFile(XmlFileName);
    }
    #endregion

    #region ReadXsdFile Method
    public void ReadXsdFile()
    {
      ResultText = string.Empty;

      ResultText = ReadFile(XsdFileName);
    }
    #endregion

    #region ReadFile Method
    protected virtual string ReadFile(string fileName)
    {
      string ret = string.Empty;

      if (!string.IsNullOrEmpty(fileName)) {
        if (File.Exists(fileName)) {
          ret = File.ReadAllText(fileName, Encoding.Unicode);
        }
        else {
          ret = "File: '" + fileName + "' does not exist.";
        }
      }

      return ret;
    }
    #endregion

    #region INotifyPropertyChanged
    /// <summary>
    /// The PropertyChanged Event to raise to any UI object
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The PropertyChanged Event to raise to any UI object
    /// The event is only invoked if data binding is used
    /// </summary>
    /// <param name="propertyName">The property name that is changing</param>
    protected void RaisePropertyChanged(string propertyName)
    {
      // Grab a handler
      PropertyChangedEventHandler handler = this.PropertyChanged;
      // Only raise event if handler is connected
      if (handler != null) {
        PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

        // Raise the PropertyChanged event.
        handler(this, args);
      }
    }
    #endregion
         */
    }
}
