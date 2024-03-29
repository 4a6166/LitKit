﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class TimeDateExtensions
    {
        public static bool Between(this DateTime dt, DateTime rangeBeg, DateTime rangeEnd)
        {
            return dt.Ticks >= rangeBeg.Ticks && dt.Ticks <= rangeEnd.Ticks;
        }

        /// <summary>
        /// Figure out how old something (or someone) is.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
                age--;
            return age;
        }

        /// <summary>
        /// Based on the time, it will display a readable sentence as to when that time happened (i.e. 'One second ago' or '2 months ago')
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToReadableTime(this DateTime value)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks);
            double delta = ts.TotalSeconds;
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        /// <summary>
        /// Determine if the date is a working day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool WorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
        /// <summary>
        /// Determine if the date is a weekend
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        /// <summary>
        /// Determine the next workday coming up
        /// BUG: if the date given in the argument is a weekday it will never check the next day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime NextWorkday(this DateTime date)
        {
            var nextDay = date.AddDays(1);
            while (!nextDay.WorkingDay())
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        /// <summary>
        /// Determine the Next date by passing in a DayOfWeek (i.e. From this date, when is the next Tuesday?)
        /// </summary>
        /// <param name="current"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            DateTime result = current.AddDays(offsetDays);
            return result;
        }
    }
}
