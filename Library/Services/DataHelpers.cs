using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Services
{
    public class DataHelpers
    {
        /// <summary>
        /// We want to represent the business hours into human readable strings
        /// </summary>
        /// <param name="branchHours"></param>
        /// <returns></returns>
        public static IEnumerable<string> HumanizeBusinessHours(IEnumerable<BranchHour> branchHours)
        {
            var hours = new List<string>();

            foreach (BranchHour time in branchHours)
            {
                var day = HumanizeDay((int)time.DayOfWeek);
                var openTime = HumanizeTime(time.OpenTime);
                var closeTime = HumanizeTime(time.CloseTime);

                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            }
            return hours;
        }

        private static string HumanizeTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }

        private static string HumanizeDay(int number)
        {
            return Enum.GetName(typeof(DayOfWeek), number);
        }
    }
}
