using System;
using System.Data;
using SystemManagement.Shared.Attributes;

namespace SystemManagement.Shared.Models.Input.Sample
{
    public class InputGetByInput
    {
        [StoredProcedureParameter(ParameterName = "EID", SqlDbType = SqlDbType.BigInt)]
        public int Id { get; set; }                                     // Has different parameter name and sql type in stored procedure

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }                         // Has size limit

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public string StructuredDateTable { get; set; }                 // Will be converted to data table

        [IgnoreInStoredProcedureParameters]
        public string IgnoredProperty { get; set; }                     // Will be ignored from sending to stored procedure as a parameter
    }
}
