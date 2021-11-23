using System;
using System.Collections.Generic;
using System.Reflection; 
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// PathUtil
    /// 
    /// April 10, 2021
    /// 
    /// Provides utilities surrounding paths.
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// Prepends the Lightning installation directory to <paramref name="Path"/>.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string GetLightningPath(string Path)
        {
            string LIPath = AppDomain.CurrentDomain.BaseDirectory;

            return @$"{LIPath}\{Path}";
        }

        public static string GetXmlPath(string XmlPath, string Path)
        {
            if (!XmlPath.Contains(':'))
            {
                return Path;
            }
            else
            {
                string[] XmlPathComponents = XmlPath.Split('\\');

                StringBuilder SB = new StringBuilder();
                
                if (XmlPathComponents.Length < 1)
                {
                    return Path; // obvious relative path
                }
                else
                {
                    // Exclude the last component (the filename)
                    for (int i = 0; i < XmlPathComponents.Length - 1; i++)
                    {
                        string XmlPathComponent = XmlPathComponents[i];
                        SB.Append(XmlPathComponent);
                    }
                }

                SB.Append($@"\{Path}");

                return SB.ToString(); 
            }
        }
    }
}
