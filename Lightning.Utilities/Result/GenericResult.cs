using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender.Utilities
{
    /// <summary>
    /// A generic result class. Use this when you do not require any additional functionality in your result classes, in lieu of creaeting a useless result class.
    /// 
    /// March 27, 2021
    /// </summary>
    public class GenericResult : IResult
    {
        /// <summary>
        /// A reason for failure [optional]
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// Determines if the result was successful.
        /// </summary>
        public bool Successful { get; set; }
    }
}
