namespace SystemManagement.Shared.Models.Output.SystemManager
{
    public class OutputGetFormEventByFormId
    {
        public long Id { get; set; }
        public long FormId { get; set; }
        public string EventName { get; set; }
        public string EventCode { get; set; }
        public bool IsActive { get; set; }
    }
}
