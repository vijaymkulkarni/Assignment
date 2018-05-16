using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmtSerivce
{
    /// <summary>
    /// structure to hod
    /// </summary>
    public struct AuditData
    {

        /// <summary>
        /// Data Field Change
        /// </summary>
        public string FieldName;

        /// <summary>
        /// stores old values
        /// </summary>
        public string OldValue;

        /// <summary>
        /// stores new Values.
        /// </summary>
        public string NewValue;
    }

    public class AuditHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        public AuditHelper(string operationType)
        {
            OperationType = operationType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="Id"></param>
        public AuditHelper(string operationType, object Id)
        {
            OperationType = operationType;
            PrimaryId = Convert.ToString(Id);
        }

        private string _operationType = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string OperationType
        {
            get
            {
                return _operationType;

            }
            set
            {
                _operationType = value;  
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _primaryID = String.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string PrimaryId
        {
            get
            {
                return _primaryID;

            }
            set
            {
                _primaryID = value;
            }
        }

        private List<AuditData> _auditData = new List<AuditData>();
        /// <summary>
        /// 
        /// </summary>
        public List<AuditData> AuditList
        {
            get
            {
                return _auditData;

            }
            set
            {
                if (AuditList == null) AuditList = new List<AuditData>();
                _auditData = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fldValue"></param>
        /// <param name="oValue"></param>
        /// <param name="nValue"></param>
        public void AuditColumn(string fldValue, object oValue, object nValue)
        {
            AuditList.Add(new AuditData
            {
                FieldName = fldValue,
                OldValue = Convert.ToString(oValue),
                NewValue = Convert.ToString(nValue)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        public void EndAuditing()
        {
            object _lock = new object();
            lock (_lock)
            {
                using (FileStream file = new FileStream("DataFiles\\AuditLog.txt", FileMode.Append, FileAccess.Write,
                    FileShare.None))
                {

                    // Write the string array to a new file named "WriteLines.txt".
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.WriteLine("<Field Type=\"{0}\" PrimaryId=\"{1}\">", OperationType, PrimaryId);
                        foreach (AuditData line in AuditList)
                        {
                            if (String.Compare(line.OldValue, line.NewValue) != 0)
                            {
                                writer.WriteLine("<Field Name=\"{0}\" OldValue=\"{1}\" NewValue=\"{2}\"/>",
                                    line.FieldName,
                                    line.OldValue,
                                    line.NewValue);
                            }
                        }

                        writer.WriteLine("</{0}>", OperationType);
                        file.Flush();
                    }

                }
            }
        }    
    }
}
