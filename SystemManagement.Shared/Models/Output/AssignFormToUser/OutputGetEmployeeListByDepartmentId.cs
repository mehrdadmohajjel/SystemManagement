namespace SystemManagement.Shared.Models.Output.AssignFormToUser
{
    public class OutputGetEmployeeListByDepartmentId
    {
        public long UserId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int EId { get; set; }
        public string ChartName { get; set; }
        public bool IsMainPost { get; set; }
    }
}
