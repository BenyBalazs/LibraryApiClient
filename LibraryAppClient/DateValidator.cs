using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAppClient
{
    public static class DateValidator
    {
        public static bool ValidateDate(DateTime borrow, DateTime deadline)
        {
            if (borrow >= deadline)
                return false;

            return true;
        }
    }
}
