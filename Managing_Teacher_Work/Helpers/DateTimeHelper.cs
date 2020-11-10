using Managing_Teacher_Work.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetDateTime(int month, int year)
        {
            return new DateTime(year, month, 1);
        }

        public static int SetActivityStatus(DateTime startDate, DateTime endDate)
        {
            var currentDateTime = GetCurrentDateTime();

            if (currentDateTime > endDate)
            {
                return (int)ActivityStatus.Done;
            }

            if(currentDateTime < startDate)
            {
                return (int)ActivityStatus.Ready;
            }

            return (int)ActivityStatus.OnGoing;
        }
    }
}