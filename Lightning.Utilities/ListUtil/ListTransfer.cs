using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// ListTransfer
    /// 
    /// May 13, 2021
    /// 
    /// Utility to transfer lists between two types.
    /// </summary>
    /// <typeparam name="T">The type of the list you wish to transfer from.</typeparam>
    /// <typeparam name="T2">The type of the list you wish to transfer to.</typeparam>
    public class ListTransfer<T, T2> where T2 : T 
    {
        /// <summary>
        /// Transfers a list between two types.
        /// </summary>
        /// <param name="ListTo">The list to convert to <see cref="T2"/>.</param>
        /// <param name="RemoveObjectsThatAreNotOfType">If true, removes objects that cannot be converted to the type <see cref="T2"/>.</param>
        /// <returns>A list containing objects of type <see cref="T2"/>.</returns>
        public static List<T2> TransferBetweenTypes(List<T> ListTo, bool RemoveObjectsThatAreNotOfType = false)
        {
            if (!typeof(T).IsAssignableFrom(typeof(T2)))
            {
                return null;
            }
            else
            {

                List<T> NewListTo = CopyList(ListTo);

                List<T2> NewList = new List<T2>();

                for (int i = 0; i < NewListTo.Count; i++)
                {
                    T Item = NewListTo[i];

                    if (RemoveObjectsThatAreNotOfType)
                    {
                        // Remove objects that cannot be converted
                        Type TypeOfT2 = typeof(T2);

                        if (!TypeOfT2.IsAssignableFrom(typeof(T)))
                        {
                            NewListTo.Remove(Item);
                            continue; 
                        }
                    }

                    NewList.Add((T2)Item);
                }

                return NewList;
            }

        }

        /// <summary>
        /// Copies a list to a new object to ensure it is not a reference to an internal structure to prevent internal state trashing
        /// 
        /// May 27, 2021
        /// </summary>
        /// <param name="List1"></param>
        private static List<T> CopyList(List<T> List1)
        {
            List<T> List2 = new List<T>();

            foreach (T Item1 in List1)
            {
                List2.Add(Item1);    
            }

            return List2;
        }
    }
}
