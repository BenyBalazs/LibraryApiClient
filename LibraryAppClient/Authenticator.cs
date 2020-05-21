using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;

namespace LibraryAppClient
{
    public static class Authenticator
    {
       public static bool LogIn(string username, string password)
        {
            if (username == Username() && password == Password())
                return true;
            else
                return false;
        }

        private static string Username()
        {
            return "admin";
        }

        private static string Password()
        {
            return "admin";
        }
    }
}
