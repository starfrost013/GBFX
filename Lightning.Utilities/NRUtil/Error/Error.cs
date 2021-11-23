using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NuRender
{

    /// <summary>
    /// This is a custom error handler that can be used by an error.
    /// 
    /// It currently takes an Error object as its sole parameter and returns nothing (void).
    /// 
    /// THIS WILL OVERRIDE ALL BEHAVIOUR! 
    /// </summary>
    /// <param name="err"></param>
    /// <returns></returns>
    public delegate void CustomErrorHandler(Error err);

    /// <summary>
    /// Lightning
    /// 
    /// Error
    /// 
    /// Non-instanceable object (not part of the DataModel)
    /// </summary>

    public class Error
    {
        /// <summary>
        /// A base .NET exception, if it exists
        /// </summary>
        public Exception BaseException { get; set; } // putting this back in. 

        /// <summary>
        /// An optional custom error handler to run on this error being thrown.
        /// </summary>
        public CustomErrorHandler CustomErrHandler { get; set; }

        /// <summary>
        /// An optional detailed description of the error message.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A numerical ID used to uniquely identify this error message.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// A name describing the basic issue that this error message defines.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if this error is a NuRender internal error. If so, an extra prompt will be displayed.
        /// </summary>
        public bool NuRenderInternal { get; set; }

        /// <summary>
        /// The severity of this error - see <see cref="MessageSeverity"/>.
        /// </summary>
        public MessageSeverity Severity { get; set; }

    }
}
