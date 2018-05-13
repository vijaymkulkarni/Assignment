using System.Runtime.Serialization;

namespace ContactMgmtCommon
{
    [DataContract]
    public class CustomException
    {
        [DataMember] public string ExceptionMessage;

        [DataMember] public string InnerException;

        [DataMember] public string StackTrace;

        [DataMember] public string Title;
    }
}