using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// TimeUtil
    /// 
    /// September 1, 2021
    /// 
    /// Provides time utility services
    /// </summary>
    public static class TimeUtil
    {
        
        /// <summary>
        /// Gets the current Unix time (64-bit.)
        /// </summary>
        /// <returns>A <see cref="long"/> containing the current 64-bit Unix time.</returns>
        public static long GetUnixTime() => (long)GetEpochTime(new TimeEpoch(new DateTime(1970, 1, 1, 1, 1, 1)));

        /// <summary>
        /// Gets the current time relative to the time epoch <see cref="TimeEpoch"/>.
        /// 
        /// I WISH I COULD USE GENERIC PARAMETERS BUT THE CONSTRAINTS AREN'T FLEXIBLE ENOUGH DAMMIT.
        /// </summary>
        /// <param name="Epoch"></param>
        /// <returns></returns>
        public static object GetEpochTime(TimeEpoch Epoch) // GENERIC PARAMETERS WON'T WORK HERE FUCK
        {
            if (Epoch == null) 
            {
                throw new ArgumentNullException($"Attempted to pass invalid epoch to GetEpochTime!");
            }
            else
            {
                DateTime Now = DateTime.UtcNow;

                if (Epoch.ReturnAsDateTime)
                {
                    TimeSpan TS = (Now - Epoch.Epoch);

                    return TS;
                }
                else
                {
                    switch (Epoch.Mode)
                    {
                        case TimeEpochMode.Default: // seconds will also be picked up
                            long Ticks = (long)(Now - Epoch.Epoch).TotalSeconds;
                            return Ticks;
                        case TimeEpochMode.Milliseconds:
                            long TicksM = (long)(Now - Epoch.Epoch).TotalMilliseconds;
                            return TicksM;
                        case TimeEpochMode.Minutes:
                            long TicksMI = (long)(Now - Epoch.Epoch).TotalMinutes;
                            return TicksMI;
                        case TimeEpochMode.Hours:
                            long TicksH = (long)(Now - Epoch.Epoch).TotalHours;
                            return TicksH;
                        case TimeEpochMode.Days:
                            long TicksD = (long)(Now - Epoch.Epoch).TotalDays;
                            return TicksD;
                        case TimeEpochMode.Weeks:
                            long TicksW = (long)(Now - Epoch.Epoch).TotalDays;

                            TicksW /= 7;

                            return TicksW;
                        case TimeEpochMode.Months:
                            long TicksMO = (long)(Now - Epoch.Epoch).TotalDays;

                            // I cba to deal with bullshit february
                            TicksMO /= 30;

                            return TicksMO;

                        case TimeEpochMode.Years:
                            long TicksYE = (long)(Now - Epoch.Epoch).TotalDays;

                            // supposed to be .2425!!!!!
                            TicksYE /= 365;

                            return TicksYE;

                    }


                }

                return null; 
            }   


            
            

        }
    }
}
