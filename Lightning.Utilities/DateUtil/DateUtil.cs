using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    public static class DateUtil
    {
        /// <summary>
        /// Determines if a particular year is a leap year.
        /// </summary>
        /// <param name="Year">An Int32 containing the year you wish to check.</param>
        /// <returns></returns>
        public static bool IsYearLeapYear(this int Year)
        {
            if (Year % 4 == 0)
            {
                if (Year % 100 == 0)
                {
                    if (Year % 400 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true; 
            }
            else
            {
                return false; 
            }
        }
    }
}
