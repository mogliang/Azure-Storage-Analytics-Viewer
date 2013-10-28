using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization;

namespace AzureStorageAnalyticsViewer
{
    public class IsolatedStorageSettings
    {
        const string _isfname = "appsettings";
        Dictionary<string, string> _dict = new Dictionary<string, string>();
         
        public IsolatedStorageSettings()
        {
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            var stream = isf.OpenFile(_isfname, System.IO.FileMode.OpenOrCreate);
            DataContractSerializer dcs = new DataContractSerializer(_dict.GetType());
            _dict = dcs.ReadObject(stream) as Dictionary<string, string>;
            stream.Close();
        }

        public void Save()
        {
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            var stream = isf.OpenFile(_isfname, System.IO.FileMode.OpenOrCreate);
            DataContractSerializer dcs = new DataContractSerializer(_dict.GetType());
            dcs.WriteObject(stream, dcs);
            stream.Close();
        }

        public bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        public string this[string idx]
        {
            get
            {
                return _dict[idx];
            }
            set
            {
                if (_dict.ContainsKey(idx))
                    _dict[idx] = value;
                else
                    _dict.Add(idx, value);
            }
        }
    }
}
