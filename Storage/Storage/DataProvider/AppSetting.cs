using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DataProvider
{
    internal class AppSetting
    {
        private Configuration Configuration;

        public AppSetting()
        {
            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public string GetConnectionString(string name)
        {
            return Configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }

        public void SetConnectionString(string name, string value)
        {
            Configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString = value;
            Configuration.ConnectionStrings.ConnectionStrings[name].ProviderName = "System.Data.SqlClient";
            Configuration.Save(ConfigurationSaveMode.Modified);
        }
    }
}
