using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NuRender.Utilities
{
    public static class StringUtil
    {
        /// <summary>
        /// [DEPRECATED - DO NOT USE] 
        /// </summary>
        /// <param name="InnerXml"></param>
        /// <returns></returns>
        public static List<string> InnerXml_Parse(this String InnerXml)
        {
            // xml preprocessing
            string[] PreSplit = InnerXml.Split('<');

            List<string> FinalList = new List<string>();

            // Strip it to the name

            foreach (string SplitV1 in PreSplit)
            {
                // Skip entry strings
                if (SplitV1 == "") continue;

                string[] SplitGreaterThan = SplitV1.Split('>');

                // Split each node into its respective value
                foreach (string XMLNodeOrValue in SplitGreaterThan)
                {
                    // remove remaining XML-related characters (Version 515)
                    string XMLNodeOrValuePost = XMLNodeOrValue.Replace("/", "");
                    if (XMLNodeOrValuePost == "") continue; // skip the strings that are not like the other 

                    // only add once
                    if (!FinalList.Contains(XMLNodeOrValuePost)) FinalList.Add(XMLNodeOrValuePost);

                }
            }

            return FinalList;
        }

        /// <summary>
        /// [DEPRECATED] 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static double RoundNearest(double x, double amount) => Math.Round((x / amount) * amount); // swap signs for anti-retardation measures

        /// <summary>
        /// Converts an array to a string, with an optional (redundant?) line ending.
        /// 
        /// [DEPRECATED - USE TEXTCHUNK]
        /// </summary>
        /// <param name="String">The string array to convert to a string.</param>
        /// <param name="UseNewline">If true, a newline will be appended to every string.</param>
        /// <param name="LE">The <see cref="LineEnding"/> to use.</param>
        /// <returns></returns>
        public static string ConvertArrayToString(this string[] String, bool UseNewline = false, LineEnding LE = LineEnding.Windows) // UseNewline and LineEnding added on conversion to UL5
        {
            StringBuilder SB = new StringBuilder();

            foreach (string StringComponent in String)
            {

                if (UseNewline)
                {
                    SB.Append(StringComponent);
                    continue;
                }
                else
                {
                    switch (LE)
                    {
                        case LineEnding.Windows:
                            SB.Append($"{StringComponent}\r\n");
                            continue;
                        case LineEnding.Unix:
                            SB.Append($"{StringComponent}\n");
                            continue;
                    }
                }


            }

            return SB.ToString();
        }

        /// <summary>
        /// Gets the number of months between two dates.
        /// </summary>
        /// <param name="Initial">The initial date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <returns>An int value representing the number of months between two dates.</returns>
        public static int GetMonthsBetweenTwoDates(DateTime Initial, DateTime EndDate) // DO NOT DO THE ABSOLUTE!
        {
            return 12 * (Initial.Year - EndDate.Year) - Initial.Month + EndDate.Month;
        }

        /// <summary>
        /// Pads the passed value with the number of zeros passed.
        /// </summary>
        /// <param name="Value">The value to be padded.</param>
        /// <param name="Zeros">The number of zeros </param>
        /// <returns>A string containing <see cref="Value"/> appended with the number of zeros passed with the <see cref="Zeros"/> parameter.</returns>
        public static string PadZero(int Value, int Zeros = 1)
        {
            // we don't do this for >10
            if (Value > 9) return Value.ToString();

            StringBuilder SB = new StringBuilder();

            for (int i = 0; i < Zeros; i++) //AAA
            {
                SB.Append("0");
            }

            SB.Append(Value.ToString());

            return SB.ToString();
        }

        public static bool ContainsCaseInsensitive(this string Text, string Value, StringComparison SC = StringComparison.CurrentCultureIgnoreCase) => Text.IndexOf(Value, SC) >= 0;

        /// <summary>
        /// Checks if a string contains numerical characters.
        /// </summary>
        /// <param name="Text">The string you wish to check for numerical characters.</param>
        /// <returns>A boolean value that determines if the string contains numeric values.</returns>
        public static bool ContainsNumeric(this string Text)
        {
            List<byte> TextByteArray = Text.ToByteList();
            // v3.0: pervasive result classes and user input validation
            if (TextByteArray == null) return false;

            foreach (byte TextByte in TextByteArray)
            {
                if (IsNumeric(TextByte)) return true;
            }

            return false;
        }


        private static bool IsNumeric(byte Byte) => (Byte >= 0x30 && Byte <= 0x3A); // May 4, 2021: Fix this entire hting being fucking broken

        /// <summary>
        /// Checks if a string contains numerical characters exclusively.
        /// </summary>
        /// <param name="Text">The string you wish to check for numerical characters.</param>
        /// <returns>A boolean value determining if the string passed exclusively contains numerical characters.</returns>
        public static bool ExclusivelyContainsNumeric(this string Text)
        {
            if (!ContainsNumeric(Text))
            {
                return false;
            }
            else
            {
                List<byte> TextByteArray = Text.ToByteList(); 

                foreach (byte Byte in TextByteArray)
                {
                    if (!IsNumeric(Byte)) return false;
                }
            }

            return true; 
        }

        /// <summary>
        /// Gets the first index of a string that contains a numeric character.
        /// </summary>
        /// <param name="Text">The string you wish to pass</param>
        /// <returns>The index of the first numeric byte in the string, or -1 if there are no numerical bytes in the string.</returns>
        public static int GetFirstIndexOfNumeric(this string Text)
        {
            List<byte> TextByteList = Text.ToByteList();

            for (int i = 0; i < TextByteList.Count; i++)
            {
                byte TextByte = TextByteList[i];

                if (IsNumeric(TextByte)) return i;
            }

            return -1;
        }

        /// <summary>
        /// Converts a string to a <see cref="List{T}"/> of bytes.
        /// </summary>
        /// <param name="Text">Extension method - call on object</param>
        /// <returns>A <see cref="(List{byte})"/> containing each character of the string in a byte representation</returns>
        public static List<byte> ToByteList(this string Text)
        {
            List<byte> ByteList = new List<byte>();

            foreach (char TextCharacter in Text)
            {
                // ASCII :heart:?
                try
                {
                    byte TextByte = Convert.ToByte(TextCharacter);
                    ByteList.Add(TextByte);
                }
                catch (FormatException)
                {
                    return null;
                }
            }


            return ByteList;
        }

        /// <summary>
        /// Convert SGML-based (X(A)ML/HTML) formatted string to plaintext - replaces the most common mnemonics and NCEs with their character equivalents
        /// </summary>
        /// <param name="HTXAMLFormattedString">The string to process.</param>
        /// <returns>A processed string formatted for use in .NET framework or native code applications.</returns>
        public static string Xaml2Cs(this string HTXAMLFormattedString)
        {
            if (HTXAMLFormattedString == null)
            {
                return null;
            }

            string Fnl = HTXAMLFormattedString;

            if (Fnl.Contains("&amp;")) Fnl = Fnl.Replace("&amp;", "&");
            if (Fnl.Contains("&lt;")) Fnl = Fnl.Replace("&lt;", "<");
            if (Fnl.Contains("&gt;")) Fnl = Fnl.Replace("&gt;", ">");
            if (Fnl.Contains("&nbsp;")
                || Fnl.Contains("&#160;")) Fnl = Fnl.Replace("&nbsp;", 0xA0.ToString());
            if (Fnl.Contains("&#10;")) Fnl = Fnl.Replace("&#10;", "\n");
            if (Fnl.Contains("&#13;")) Fnl = Fnl.Replace("&#13;", "\r");

            return Fnl;

        }

        /// <summary>
        /// Convert .NET formatted strings to X(A)ML / SGML-based formatted ones. Replaces the most common characters with their XML mnemonic / NCE equivalents.
        /// </summary>
        /// <param name="NormalString">The string to process.</param>
        /// <returns>A processed string formatted for use in xML (SGML / HTML / XAML / XML).</returns>
        public static string Cs2Xaml(this string NormalString)
        {
            if (NormalString == null)
            {
                return null; 
            }

            string ProcessedString = NormalString;

            if (ProcessedString.Contains("&")) ProcessedString = ProcessedString.Replace("<", "&amp;");
            if (ProcessedString.Contains("<")) ProcessedString = ProcessedString.Replace("<", "&lt;");
            if (ProcessedString.Contains(">")) ProcessedString = ProcessedString.Replace(">", "&gt;");
            if (ProcessedString.Contains(0xA0.ToString())) ProcessedString = ProcessedString.Replace(0xA0.ToString(), "&nbsp;"); // Nonbreaking Space
            if (ProcessedString.Contains("&#10;")) ProcessedString = ProcessedString.Replace("&#10;", "\n");
            if (ProcessedString.Contains("&#13;")) ProcessedString = ProcessedString.Replace("&#13;", "\r");

            return ProcessedString;
        }

        /// <summary>
        /// Convert a byte array to a string.
        /// </summary>
        /// <param name="ByteArray"></param>
        /// <returns>The passed byte array converted to a string.</returns>

        public static string ByteArrayToString(this byte[] ByteArray)
        {
            List<byte> ByteList = ByteArray.ToList();
            return ByteListToString(ByteList);
        }

        /// <summary>
        /// Convert a byte array to a hex string. (v722.1)
        /// </summary>
        /// <param name="ByteArray"></param>
        /// <returns>The passed byte array converted to a hexadecimal format string.</returns>

        public static string ByteArrayToHexString(this byte[] ByteArray)
        {
            List<byte> ByteList = ByteArray.ToList();
            return ByteListToHexString(ByteList);
        }

        /// <summary>
        /// Convert a list of bytes to a string.
        /// </summary>
        /// <param name="ByteList"></param>
        /// <returns>The passed byte list converted to a hexadecimal format string.</returns>
        public static string ByteListToString(this List<byte> ByteList)
        {
            if (ByteList == null)
            {
                return null;
            }
            else
            {
                if (ByteList.Count == 0)
                {
                    return "";
                }
                else
                {
                    StringBuilder SB = new StringBuilder();

                    foreach (byte Byte in ByteList)
                    {
                        SB.Append(Byte);
                    }

                    return SB.ToString();
                }
            }

        }

        /// <summary>
        /// Convert a list of bytes to a hex string. TODO: Reduce code reuse
        /// </summary>
        /// <param name="ByteList"></param>
        /// <returns>A hexadecimal string.</returns>
        public static string ByteListToHexString(this List<byte> ByteList)
        {
            if (ByteList == null)
            {
                return null;
            }
            else
            {
                if (ByteList.Count == 0)
                {
                    return "";
                }
                else
                {
                    StringBuilder SB = new StringBuilder();

                    foreach (byte Byte in ByteList)
                    {
                        int NumericalByte = Convert.ToInt32(Byte);

                        SB.Append(NumericalByte.ToString("X"));
                    }

                    return SB.ToString();
                }
            }

        }

        /// <summary>
        /// Removes the day of a week from a datetime represented as a string.
        /// 
        /// This is required for appveyor builds.
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns>A string with the days of the week removed.</returns>
        public static string RemoveDaysOfWeek(this string DateTime)
        {
            string Monday = "Mon";
            string Tuesday = "Tue";
            string Wednesday = "Wed";
            string Thursday = "Thu";
            string Friday = "Fri";
            string Saturday = "Sat";
            string Sunday = "Sun";

            // If the string contains any of the strings we want to remove,
            // remove them. Use the case insensitive method for this
            if (DateTime.ContainsCaseInsensitive(Monday)) DateTime = DateTime.Replace(Monday, "");
            if (DateTime.ContainsCaseInsensitive(Tuesday)) DateTime = DateTime.Replace(Tuesday, "");
            if (DateTime.ContainsCaseInsensitive(Wednesday)) DateTime = DateTime.Replace(Wednesday, "");
            if (DateTime.ContainsCaseInsensitive(Thursday)) DateTime = DateTime.Replace(Thursday, "");
            if (DateTime.ContainsCaseInsensitive(Friday)) DateTime = DateTime.Replace(Friday, "");
            if (DateTime.ContainsCaseInsensitive(Saturday)) DateTime = DateTime.Replace(Saturday, "");
            if (DateTime.ContainsCaseInsensitive(Sunday)) DateTime = DateTime.Replace(Sunday, "");

            return DateTime;

             
        }

        /// <summary>
        /// Determines if a string contains alphabetical characters. Required for LightningScript tokenisation (April 17, 2021)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns>A boolean that determines if the string contains alpha characters</returns>
        public static bool ContainsAlpha(this string Str)
        {
            char[] StrCharacters = Str.ToCharArray();

            foreach (char StrCharacter in StrCharacters)
            {
                if (StrCharacter < 0x30
                    || (StrCharacter > 0x39) && (StrCharacter < 0x41)
                    || StrCharacter > 0x5A)
                {
                    return false;
                }
            }

            return true; 
        }

        /// <summary>
        /// Counts the number of newlines in the string <see cref="Str"/>.
        /// </summary>
        /// <param name="Str">Extension method, call on string objct</param>
        /// <returns>An int with the number of newlines. If the string is null, returns 0.</returns>
        public static int CountNewlines(this string Str)
        {
            if (Str == null) return 0;

            int NewLineCount = 0;

            foreach (char Character in Str)
            {
                if (Character == '\n') NewLineCount++;
            }

            return NewLineCount; 
        }

        /// <summary>
        /// Returns the string with ;ine ID <paramref name="LineID"/>
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static string GetLineWithId(this string Str, int LineID)
        {
            int LineCount = Str.CountNewlines(); 

            if (LineCount == 0)
            {
                if (LineID != 0) // zero-based for now
                {
                    return null;
                }
                else
                {
                    return Str; 
                }
            }
            else
            {
                if (LineID > LineCount)
                {
                    return null;
                }
                else
                {
                    string[] SplitLines = Str.Split('\n');

                    return SplitLines[LineID];
                }
            }
        }

        public static string Append(this string StringToAppendTo, char Character) => Append<char>(StringToAppendTo, Character);

        public static string Append<T>(this string StringToAppendTo, T StringToConvert)
        {
            Type TypeOfString = typeof(T); 
            
            // i'd check that the user isn't being an idiot
            // but i have more important things to do
            return $"{StringToAppendTo}{StringToConvert.ToString()}";
        }

    }
}
