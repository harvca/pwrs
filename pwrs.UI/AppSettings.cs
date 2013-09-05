using System;

namespace pwrs.UI
{
	public class AppSettings
	{
	        public static string LDAPServer
        {
            get
            {
                string tmp = System.Configuration.ConfigurationManager.AppSettings["LDAPServer"];
                if (string.IsNullOrWhiteSpace(tmp)) { throw new Exception("LDAPServer is NULL"); }
                return tmp;
            }
        }

		        public static string UserLDAPPath
        {
            get
            {
                string tmp = System.Configuration.ConfigurationManager.AppSettings["UserLDAPPath"];
                if (string.IsNullOrWhiteSpace(tmp)) { throw new Exception("UserLDAPPath is NULL"); }
                return tmp;
            }
        }
	}
}

