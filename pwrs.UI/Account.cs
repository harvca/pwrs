using System;
using System.DirectoryServices;
using System.Security.Principal;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace pwrs.UI
{
	public class Account
	{
		public static void ResetPassword(string userDn, string password,bool resetAtNextLogon)
		{
			 DirectoryEntry uEntry = new DirectoryEntry(userDn);
            uEntry.Invoke("SetPassword", new object[] { password });
            //unlock account
            uEntry.Properties["LockOutTime"].Value = 0;
            if (resetAtNextLogon == true)
            {
                uEntry.Properties["pwdLastSet"].Value = 0; 
            }
            uEntry.CommitChanges();
            uEntry.Close();
		}

        public static string GetUserDn(string userName, string ldapPath)
        {
            DirectoryEntry entry = new DirectoryEntry(string.Concat(new string[] { ldapPath }), null, null, AuthenticationTypes.Secure);
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = entry;
            searcher.Filter = ("(&(objectClass=user)(SAMAccountName=" + userName + "))");
            searcher.SearchScope = SearchScope.Subtree;
            SearchResult result = searcher.FindOne();
            if (result != null)
            {
                return result.Path;
            }
            return null;
        }
	}
}