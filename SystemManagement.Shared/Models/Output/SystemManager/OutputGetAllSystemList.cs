namespace SystemManagement.Shared.Models.Output.SystemManager
{
    public class OutputGetAllSystemList
    {
        public long Id { get; set; }
        public string SystemName { get; set; }
        public string SystemCode { get; set; }
        public bool IsActive { get; set; }
    }
}
