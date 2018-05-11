using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtCommon
{

    [DataContract()]
    public class CustomException
    {
        [DataMember()]
        public string Title;

        [DataMember()]
        public string ExceptionMessage;

        [DataMember()]
        public string InnerException;

        [DataMember()]
        public string StackTrace;
    }
}
