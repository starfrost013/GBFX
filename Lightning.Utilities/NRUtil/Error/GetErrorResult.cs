using NuRender.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NuRender
{
    /// <summary>
    /// GetErrorResult
    /// 
    /// March 7, 2021 (modified July 18, 2021: Add comment block)
    /// 
    /// Defines a result class for errors.
    /// </summary>
    public class GetErrorResult : IResult
    {
        public Error Error { get; set; }
        public string FailureReason { get; set; }
        public bool Successful { get; set; }
    }
}
