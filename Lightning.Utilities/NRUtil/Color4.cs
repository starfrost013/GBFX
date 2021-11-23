using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization; 
using System.Text;

namespace NuRender
{
    /// <summary>
    /// Color4
    /// 
    /// March 9, 2021 (modified July 12, 2021: add constructor)
    /// 
    /// NuRender temp version of Color4 for bringup and early dev. DataModel stuff cut
    /// </summary>
    /// 
    public class Color4
    {
        /// <summary>
        /// The alpha component of this colour.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// The red component of this colour.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// The green component of this colour.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// The red component of this colour.
        /// </summary>
        public byte B { get; set; }

        // result class?

        /// <summary>
        /// Convert a relative colour string to a Color3 value.
        /// </summary>
        /// <param name="Colour"></param>
        /// <param name="AddToDataModel">If false, simply creates an object and returns. If true, adds to the DataModel</param>
        /// <returns></returns>
        public static Color4 FromRelative(string Colour, bool AddToDataModel = true)
        {
            string[] Spx = Colour.Split(',');

            if (Spx.Length != 4)
            {
                ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingRelativeColourException");
                return null; // do we use a result class for this?
            }
            else
            {
                try
                {
                    double R00 = Convert.ToDouble(Spx[0]);
                    double R01 = Convert.ToDouble(Spx[1]);
                    double R02 = Convert.ToDouble(Spx[2]);
                    double R03 = Convert.ToDouble(Spx[3]);

                    // Error check
                    if (R00 < 0 || R00 > 1
                     || R01 < 0 || R01 > 1
                     || R02 < 0 || R02 > 1
                     || R03 < 0 || R03 > 1)
                    {
                        ErrorManager.ThrowError("Color4 Converter", "RelativeColourOutOfRangeException");
                        return null; //result class?
                    }

                    Color4 C4 = new Color4();

                    C4.A = Convert.ToByte(R00 * 255);
                    C4.R = Convert.ToByte(R01 * 255);
                    C4.G = Convert.ToByte(R02 * 255);
                    C4.B = Convert.ToByte(R03 * 255);

                    return C4;

                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingRelativeColourFormatException", "Attempted to convert an invalid RelativeColour to a Color3!", err);
#else
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingRelativeColourFormatException", "Attempted to convert an invalid RelativeColour to a Color3!");
#endif
                    return null;
                }


            }
        }

        public static Color4 FromHex(string Colour, bool AddToDataModel = true)
        {
            // Remove the # that is sometimes used. 
            if (Colour.Contains("#"))
            {
                Colour = Colour.Replace("#", "");
            }

            if (Colour.Length != 8)
            {
                ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingHexadecimalColourException");
                return null;
            }
            else
            {
                // Split into the colour components of an ARGB colour. 
                string A = Colour.Substring(0, 2);
                string R = Colour.Substring(2, 2);
                string G = Colour.Substring(4, 2);
                string B = Colour.Substring(6, 2);

                try
                {
                    byte FA = byte.Parse(A, NumberStyles.HexNumber);
                    byte FR = byte.Parse(R, NumberStyles.HexNumber);
                    byte FG = byte.Parse(G, NumberStyles.HexNumber);
                    byte FB = byte.Parse(B, NumberStyles.HexNumber);

                    Color4 C4 = new Color4();

                    C4.A = FA;
                    C4.R = FR;
                    C4.G = FG;
                    C4.B = FB;

                    return C4;
                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingHexadecimalColourFormatException", "An error occurred when converting a hexadecimal colour string to a Color4", err);
#else
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingHexadecimalColourFormatException", "An error occurred when converting a hexadecimal colour string to a Color4");
#endif
                    // TEMP - ERRORS.XML
                    return null;
                }
            }
        }


        public static Color4 FromString(string Str, bool AddToDataModel = true)
        {
            string[] Str_Components = Str.Split(',');

            if (Str_Components.Length != 4)
            {
                ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingCommaColourFormatException");
                return null;
            }
            else
            {
                try
                {

                    Color4 C4 = new Color4();

                    string A = Str_Components[0];
                    string R = Str_Components[1];
                    string G = Str_Components[2];
                    string B = Str_Components[3];

                    byte CA = byte.Parse(A);
                    byte CR = byte.Parse(R);
                    byte CG = byte.Parse(G);
                    byte CB = byte.Parse(B);

                    C4.A = CA;
                    C4.R = CR;
                    C4.G = CG;
                    C4.B = CB;

                    return C4;
                }
                catch (FormatException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingCommaColourFormatException", "Attemped to load a Color4 with an invalid component!", err);
#else
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingCommaColourFormatException", "Attemped to load a Color4 with an invalid component!");
#endif
                    return null;
                }
                catch (OverflowException err)
                {
#if DEBUG
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingCommaColourFormatException", "Attempted to load a Color4 with a component that was not within the range [0-255]!", err);
#else
                    ErrorManager.ThrowError("Color4 Converter", "ErrorConvertingCommaColourFormatException", "Attempted to load a Color4 with a component that was not within the range [0-255]!", err);
#endif
                    return null;
                }
            }
        }

        public Color4() // old code compat (July 12, 2021)
        {

        }

        public Color4(byte CA, byte CR, byte CG, byte CB)
        {
            // use for non-DataModel ONLY
            A = CA;
            R = CR;
            G = CG;
            B = CB;
        }

        #region Not really colour blending
        public static Color4 operator +(Color4 A, Color4 B) => new Color4((byte)(A.A + B.A), (byte)(A.R + B.R), (byte)(A.G + B.G), (byte)(A.B + B.B));
        public static Color4 operator +(double A, Color4 B) => new Color4((byte)(A + B.A), (byte)(A + B.R), (byte)(A + B.G), (byte)(A + B.B));
        public static Color4 operator +(Color4 A, double B) => new Color4((byte)(A.A + B), (byte)(A.R + B), (byte)(A.G + B), (byte)(A.B + B));
        public static Color4 operator -(Color4 A, Color4 B) => new Color4((byte)(A.A - B.A), (byte)(A.R - B.R), (byte)(A.G - B.G), (byte)(A.B - B.B));
        public static Color4 operator -(double A, Color4 B) => new Color4((byte)(A - B.A), (byte)(A - B.R), (byte)(A - B.G), (byte)(A - B.B));
        public static Color4 operator -(Color4 A, double B) => new Color4((byte)(A.A - B), (byte)(A.R - B), (byte)(A.G - B), (byte)(A.B - B));
        public static Color4 operator *(Color4 A, Color4 B) => new Color4((byte)(A.A * B.A), (byte)(A.R * B.R), (byte)(A.G * B.G), (byte)(A.B * B.B));
        public static Color4 operator *(double A, Color4 B) => new Color4((byte)(A * B.A), (byte)(A * B.R), (byte)(A * B.G), (byte)(A * B.B));
        public static Color4 operator *(Color4 A, double B) => new Color4((byte)(A.A * B), (byte)(A.R * B), (byte)(A.G * B), (byte)(A.B * B));
        public static Color4 operator /(Color4 A, Color4 B) => new Color4((byte)(A.A / B.A), (byte)(A.R / B.R), (byte)(A.G / B.G), (byte)(A.B / B.B));
        public static Color4 operator /(double A, Color4 B) => new Color4((byte)(A / B.A), (byte)(A / B.R), (byte)(A / B.G), (byte)(A / B.B));
        public static Color4 operator /(Color4 A, double B) => new Color4((byte)(A.A / B), (byte)(A.R / B), (byte)(A.G / B), (byte)(A.B / B));


        #endregion
    }
}
