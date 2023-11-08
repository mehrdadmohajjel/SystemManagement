using System.Data;

namespace SystemManagement.Api.Models.Base
{
    public class QueryResult
    {
        public int SPCode { get; set; }

        public string Text { get; set; }

        public int ReturnCode { get; set; }

        public DataSet DataSet { get; set; }
    }
}