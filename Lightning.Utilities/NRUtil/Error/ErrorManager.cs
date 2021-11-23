using NuRender.NativeInterop.Win32; 
using NuRender.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NuRender
{
    /// <summary>
    /// Global error manager for Lightning.
    /// </summary>
    public static class ErrorManager
    {
        /// <summary>
        /// Prevents the Error Manager from being re-initialised every single time the DataModel is re-initialised (eg: loading game using DDMS).
        /// </summary>
        public static bool ERRORMANAGER_LOADED { get; set; }

        /// <summary>
        /// The actual errors themselves.
        /// </summary>
        public static ErrorCollection Errors { get; set; }

        /// <summary>
        /// Path to the XML schema for Errors.
        /// </summary>

        public static string ERRORMANAGER_XSD_PATH = @"Content\Schema\Errors.xsd";

        /// <summary>
        /// Path to the Error XML used for loading errors.
        /// </summary>
        public static string ERRORMANAGER_XML_PATH = @"Content\EngineContent\Errors.xml";

        public static void Init()
        {
            Errors = new ErrorCollection();
            //pre-globalsettings

            ErrorRegistration.RegisterErrors();
//#if DEBUG Removed 2021-11-16 (NuRender 0.2.3)
//            ATest_CheckErrorSerialisedCorrectly();
//#endif
            ERRORMANAGER_LOADED = true; 
        }

        private static bool DoesErrorExist(Error Err) => Errors.ErrorList.Contains(Err);
#if DEBUG
        private static void ATest_CheckErrorSerialisedCorrectly()
        {
            foreach (Error Err in Errors.ErrorList)
            {
                string CompName = "Error Manager";

                Logging.Log("Error: ", CompName);
                Logging.Log($"Name: {Err.Name}", CompName);
                Logging.Log($"Description: {Err.Description}", CompName);
                Logging.Log($"Id: {Err.Id}", CompName);
                Logging.Log($"Severity: {Err.Severity}", CompName);
            }
        }
#endif
        /// <summary>
        /// temp: pre-result class
        /// 
        /// Get an error with the name ErrorName.
        /// </summary>
        /// <param name="ErrName"></param>
        /// <returns></returns>
        internal static GetErrorResult GetError(string ErrName)
        {
            if (Errors.Count == 0
                || Errors == null)
            {
                HandleError("Error Manager", new Error { Id = 0xDEA7BABE, Name = "AttemptingToThrowErrorBeforeErrorManagerInitialisedException", Description = "Attempted to throw an error when the error manager has not been initialised!", Severity = MessageSeverity.FatalError});
                return null; 
            }
            else
            {
                GetErrorResult ErrResult = new GetErrorResult();

                foreach (Error Err in Errors)
                {
                    if (Err.Name == ErrName)
                    {
                        // by default a bool is false
                        ErrResult.Successful = true;
                        ErrResult.Error = Err;
                        return ErrResult;
                    }

                }

                return ErrResult;
            }

        }

        /// <summary>
        /// Get an error with the name ErrorName.
        /// </summary>
        /// <param name="ErrName"></param>
        /// <returns></returns>
        private static GetErrorResult GetError(int ErrorId)
        {
            GetErrorResult ErrResult = new GetErrorResult();

            if (Errors.Count == 0
                || Errors == null)
            {
                HandleError("Error Manager", new Error { Id = 0xDEA7BABE, Name = "AttemptingToThrowErrorBeforeErrorManagerInitialisedException", Description = "Attempted to throw an error when the error manager has not been initialised!", Severity = MessageSeverity.FatalError });
                return null;
            }
            else
            {
                foreach (Error Err in Errors)
                {
                    if (Err.Id == ErrorId)
                    {
                        ErrResult.Successful = true;
                        ErrResult.Error = Err;
                        return ErrResult;
                    }
                }

                return ErrResult;
            }
            
        }

        /// <summary>
        /// Throw an error from Component <paramref name="Component"/> with the <see cref="Error"/> object <paramref name="Err"/>.
        /// </summary>
        /// <param name="Component">The component you wish to throw the error from. Optional </param>
        /// <param name="Err"></param>
        public static void ThrowError(string Component, Error Err) => HandleError(Component, Err);

        /// <summary>
        /// Throws the error <paramref name="ErrorName"/>, logging component <paramref name="Component"/>
        /// </summary>
        /// <param name="Component">The component</param>
        /// <param name="ErrorName"></param>
        public static void ThrowError(string Component, string ErrorName)
        {
            GetErrorResult ErrToThrow = GetError(ErrorName);

            if (ErrToThrow.Successful)
            {
                ThrowError(Component, ErrToThrow.Error);     
            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEAEBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error name {ErrorName}" });
            }
        }

        public static void ThrowError(string Component, string ErrorName, string ErrorDescription)
        {
            GetErrorResult ErrToThrow = GetError(ErrorName);
            
            if (ErrToThrow.Successful)
            {
                if (ErrorDescription != null
                    || ErrorDescription.Length > 0)
                {
                    Error Err = ErrToThrow.Error;
                    Err.Description = ErrorDescription;
                    ThrowError(Component, ErrToThrow.Error);
                    return; 
                }
                else
                {
                    ThrowError("Error Handler - InnerException", new Error { Id = 0x2222BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonexistentErrorDescriptionException", Description = "Cannot override an error's description with null or an empty string!" });
                }

               
            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEADBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error name {ErrorName}" });
            }
        }

        public static void ThrowError(string Component, string ErrorName, Exception BaseException)
        {
            GetErrorResult ErrToThrow = GetError(ErrorName);

            if (ErrToThrow.Successful)
            {
                if (BaseException != null)
                {
                    Error Err = ErrToThrow.Error;
                    Err.BaseException = BaseException;
                    ThrowError(Component, ErrToThrow.Error);
                    
                    return;

                }
                else
                {
                    ThrowError("Error Handler - InnerException", new Error { Id = 0x2222BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonexistentErrorDescriptionException", Description = "Cannot override an error's description with null or an empty string!" });
                }


            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEA8BABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error name {ErrorName}" });
            }
        }

        public static void ThrowError(string Component, string ErrorName, string ErrorDescription, Exception BaseException)
        {
            GetErrorResult ErrToThrow = GetError(ErrorName);

            if (ErrToThrow.Successful)
            {
                if (ErrorDescription != null
                    && ErrorDescription.Length > 0)
                {
                    if (BaseException == null)
                    {
                        ThrowError("Error Handler - InnerException", new Error { Id = 0x4747BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonExistentBaseExceptionException", Description = "Attempted to override the BaseException of an error with the BaseException overrides of ErrorManager.ThrowError(), but the Exception is null." });
                        return;
                    }
                    else
                    {
                        Error Err = ErrToThrow.Error;
                        Err.Description = ErrorDescription;
                        Err.BaseException = BaseException;
                        ThrowError(Component, ErrToThrow.Error);
                    }

                }
                else
                {
                    ThrowError("Error Handler - InnerException", new Error { Id = 0x2222BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonexistentErrorException", Description = "Cannot override an error's description with null!" });
                    return;
                }


            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEACBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error name {ErrorName}" });
            }
        }

        public static void ThrowError(string Component, int ErrorId)
        {
            GetErrorResult ErrToThrow = GetError(ErrorId);

            if (ErrToThrow.Successful)
            {
                ThrowError(Component, ErrToThrow.Error);
            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEABBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error ID {ErrorId}" });
            }

        }

        public static void ThrowError(string Component, int ErrorId, string ErrorDescription)
        {
            GetErrorResult ErrToThrow = GetError(ErrorId);

            if (ErrToThrow.Successful)
            {
                Error Err = ErrToThrow.Error;

                if (ErrorDescription != null
                    && ErrorDescription.Length > 0)
                {
                    Err.Description = ErrorDescription;
                    ThrowError(Component, Err);

                }
                else
                {
                    ThrowError("Error Handler - InnerException", new Error { Id = 0x2222BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonexistentErrorDescriptionException", Description = "Cannot override an error's description with null or an empty string!" });
                    return;
                }
                
            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEAFBABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error ID {ErrorId}" });
            }

        }

        public static void ThrowError(string Component, int ErrorId, string ErrorDescription, Exception BaseException)
        {
            GetErrorResult ErrToThrow = GetError(ErrorId);

            if (ErrToThrow.Successful)
            {
                Error Err = ErrToThrow.Error;

                if (ErrorDescription != null
                    && ErrorDescription.Length > 0)
                {

                    if (BaseException == null)
                    {
                        ThrowError("Error Handler - InnerException", new Error { Id = 0x4848BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonExistentBaseExceptionException", Description = "Attempted to override the BaseException of an error with the BaseException overrides of ErrorManager.ThrowError(), but the Exception is null." });
                    }
                    else
                    {
                        Err.Description = ErrorDescription;
                        ThrowError(Component, Err);
                    }


                }
                else
                {
                    ThrowError("Error Handler - InnerException", new Error { Id = 0x2222BABE, Severity = MessageSeverity.FatalError, Name = "CannotOverrideNonexistentErrorDescriptionException", Description = "Cannot override an error's description with null or an empty string!" });
                    return;
                }

            }
            else
            {
                ThrowError("Error Handler - InnerException", new Error { Id = 0xDEA9BABE, Severity = MessageSeverity.FatalError, Name = "AttemptedToThrowInvalidErrorException", Description = $"Internal error: Attempted to throw nonexistent error ID {ErrorId}" });
            }

        }

        private static void HandleError(string Component, Error Err)
        {
            if (Err.CustomErrHandler != null)
            {
                Err.CustomErrHandler(Err);
                return; 
            }
            else
            {
                if (Component != null)
                {
                    Logging.LogError(Err);
                }
                else
                {
                    Logging.LogError(Err, Component);
                }

                switch (Err.Severity)
                {
                    case MessageSeverity.Message:
                        
                        if (Err.NuRenderInternal)
                        {
                            MessageBox.Show($"NuRender: {Err.Description}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show($"{Err.Description}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        return;
                    case MessageSeverity.Warning:

                        if (Err.NuRenderInternal) // append LS for script errors
                        {
                            MessageBox.Show($"NuRender internal warning {Err.Id} ({Err.Name}): {Err.Description}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Warning ({Err.Id}; {Err.Name}): {Err.Description}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        return;
                    case MessageSeverity.Error:
                        if (Err.NuRenderInternal) // append LS for script errors
                        {
                            MessageBox.Show($"NuRender internal error {Err.Id} ({Err.Name}): {Err.Description}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Error ({Err.Id}; {Err.Name}): {Err.Description}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        return;
                    case MessageSeverity.FatalError:
                        string ErrorString = null;

                        if (!Err.NuRenderInternal)
                        {
                            ErrorString = $"Guru Meditation {Err.Id}\n\n{Err.Name}: {Err.Description}\n\nThe application must exit. Sorry!\nYou may wish to file a bug report with the application's developers.";
                        }
                        else
                        {
                            ErrorString = $"NuRender failure (Guru Meditation {Err.Id}\n\n{Err.Name}): {Err.Description}\n\nThe application must exit. Sorry!\nYou may wish to file a bug report with the application's developers.";
                        }

                        // Temporary - we don't have a clean shutdown method yet
                        MessageBox.Show(ErrorString, "Lightning Game Engine", MessageBoxButton.OK, MessageBoxImage.Error);

                        EmergencyQuit(Err, ErrorString);

                        return;

                }
            }
        }

        private static void EmergencyQuit(Error Err, string EmergencyString)
        {
            Logging.Log(EmergencyString);
            // Temporary Code (yeah this is dumb)
            Environment.Exit(0xDEAD * (int)Err.Id);
        }
       
        internal static void RegisterError(Error Err) => Errors.Add(Err);
    }
}
