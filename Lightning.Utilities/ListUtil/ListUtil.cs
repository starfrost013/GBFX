using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// ListUtil
    ///
    /// July 2, 2021
    ///
    /// Provides list and array utility services
    /// </summary>
    public static class ListUtil
    {
        public static T[] GetRange<T>(this T[] Array, int StartIndex, int EndIndex = -1)
        {

            if (EndIndex > StartIndex) throw new IndexOutOfRangeException("EndIndex is more than StartIndex!");
            if (StartIndex == 0
                || EndIndex == 0) throw new IndexOutOfRangeException("StartIndex and EndIndex == 0");

            T[] New = new T[EndIndex - StartIndex];

            for (int i = 0; i < Array.Length; i++)
            {
                T ObjI = Array[i];

                if (EndIndex != -1)
                {

                    if (i > StartIndex || i < EndIndex)
                    {
                        New[i - StartIndex] = ObjI;
                    }
                }
                else
                {
                    if (i > StartIndex)
                    {
                        New[i - StartIndex] = ObjI;
                    }
                }

            }

            return New; 
        }

        public static List<T> GetRange<T>(this List<T> Array, int StartIndex, int EndIndex = -1)
        {

            if (EndIndex > StartIndex) throw new IndexOutOfRangeException("EndIndex is more than StartIndex!");
            if (StartIndex == 0
                || EndIndex == 0) throw new IndexOutOfRangeException("StartIndex and EndIndex == 0");

            List<T> New = new List<T>();

            for (int i = 0; i < Array.Count; i++)
            {
                T ObjI = Array[i];

                if (EndIndex != -1)
                {

                    if (i > StartIndex || i < EndIndex) New.Add(ObjI);
                }
                else
                {
                    if (i > StartIndex) New.Add(ObjI);

                }

            }

            return New;
        }
    }
}
