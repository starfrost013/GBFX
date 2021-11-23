using System;
using System.Collections; 
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NuRender
{
    /// <summary>
    /// ErrorCollection
    /// 
    /// Non-DataModel 
    /// 
    /// A collection of errors,
    /// </summary>
    /// 
    [XmlRoot("Errors")]
    public class ErrorCollection : IEnumerable
    {
        // each error
        // not sure if this is right
        [XmlElement("Errors")]
        public List<Error> ErrorList { get; set; }

        public ErrorCollection()
        {
            ErrorList = new List<Error>();
        }

        public ErrorCollection(List<Error> NewErrors)
        {

            if (NewErrors == null)
            {
                if (NewErrors.Count == 0)
                {
                    ErrorManager.ThrowError("Error Serialiser & Loader", new Error { Id = 0xDEAD5555, Name = "InvalidErrorException", Description = "Unknown error in serialisation: attempted to instantiate ErrorCollection with empty list of errors!", Severity = MessageSeverity.FatalError });
                }
                else
                {
                    // safety

                    ErrorList = new List<Error>();

                    foreach (Error Err in NewErrors)
                    {
                        ErrorList.Add(Err);
                    }
                }
            }
            else
            {
                ErrorManager.ThrowError("Error Serialiser & Loader", new Error { Id = 0xDEAD1111, Name = "InvalidErrorException", Description = "Unknown error in serialisation: attempted to instantiate ErrorCollection with invalid list of errors!", Severity = MessageSeverity.FatalError });
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ErrorCollectionEnumerator GetEnumerator()
        {
            return new ErrorCollectionEnumerator(ErrorList);
        }

        /// <summary>
        /// Required for XML serialisation (2021/04/01)
        /// </summary>
        /// <param name="Obj"></param>
        public void Add(object Obj)
        {

            // This adds an object to this errorcollection if it is an error.
            // we also do this for instances
            Type ObjectType = Obj.GetType();

            Type AType = typeof(Error);

            if (ObjectType != AType)
            {
                if (ObjectType.IsSubclassOf(AType))
                {
                    Add_OnSuccess(Obj);
                }
                else
                {
                    string OTypeName = ObjectType.Name;
                    ErrorManager.ThrowError("Error Manager", "AttemptedToAddNonErrorToErrorsException", $"{OTypeName} does not inherit from Error, cannot add it to an ErrorCollection!");
                    return; 
                }
            }
            else
            {
                Add_OnSuccess(Obj);
                return;
            }
        }

        private void Add_OnSuccess(object Obj)
        {
            ErrorList.Add((Error)Obj);
        }

        public int Count => ErrorList.Count;

    }

    public class ErrorCollectionEnumerator : IEnumerator
    { 
        private List<Error> Errors { get; set; }

        public int Position = -1;
        public Error Current
        {
            get
            {
                try
                {
                    return Errors[Position];
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (object)Current;
            }
        }

        public void Reset() => Position = -1; 
        public bool MoveNext()
        {
            Position++;
            return (Position < Errors.Count);
        }

        public ErrorCollectionEnumerator(List<Error> NewErrors)
        {
            Errors = new List<Error>();

            foreach (Error CurErr in NewErrors)
            {
                Errors.Add(CurErr);
            }
        }


    }

}
