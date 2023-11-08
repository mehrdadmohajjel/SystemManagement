using System.Security.AccessControl;

namespace SystemManagement.Shared.Models.Output.SystemManager
{
    public class OutputGetSystemFormsBySystemId
    {
        public long Id { get; set; }
        public long SystemId { get; set; }
        public string FormName { get; set; }
        public string FormCode { get; set; }
        public bool IsActive { get; set; }


    }
}
