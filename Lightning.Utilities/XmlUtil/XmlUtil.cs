using System;
using System.Collections.Generic; 
using System.Text;
using System.Xml.Linq; 

namespace NuRender.Utilities
{
    public static class XmlUtil
    {
        public static bool CheckForValidXmlElementContent(XElement XmlElement)
        {
            // Set a string variable to the XML value.

            string XVal = XmlElement.Value;

            // If it's null, return false...
            if (XVal == null)
            {
                return false;
            }
            else
            {
                // if it's zero length, return false
                if (XVal.Length == 0)
                {
                    return false;
                }
                else // return true...
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Checks if a type is valid for instantiation. Type must be in the System (base only) namespace or the Lightning.* namespace.
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public static bool CheckIfValidTypeForInstantiation(string TypeName)
        {
            string[] TypeNameNamespaceDots = TypeName.Split('.');

            // If it is in the System namespace, but not any child namespace...
            if (TypeName.Contains("System") && TypeNameNamespaceDots.Length == 2)
            {
                return true;
            }
            else if (TypeName.Contains("Lightning")
                || TypeName.Contains("Polaris")) // If it's in the Lightning or Polaris namespaces...
            {
                return true; // allow it
            }
            else
            {
                return false;
            }
        }
    }
}
