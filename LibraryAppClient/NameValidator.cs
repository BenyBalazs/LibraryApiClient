using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAppClient
{
    public static class NameValidator
    {
        private static char[] IllegalChars = { '$', '{', '}', '@', '&', '#','%' };
        private static bool NameChacker(string Name)
        {
            if (Name == null)
                return false;

            for (int i = 0; i < IllegalChars.Length; i++)
                if (Name.Contains(IllegalChars[i]))
                    return false;

            if (Name.Any(char.IsDigit))
                return false;

            return true;
        }

        public static string ValidateName(string name)
        {
            if (NameChacker(name))
                return name;
            throw new ArgumentException("Invalid Name");
        }
    }
}
