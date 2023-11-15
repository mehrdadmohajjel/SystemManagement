namespace SystemManagement.Shared.Models.Output.AssignFormToUser
{
    public class OutputGetSubDepartmentListByMainDepartmentId
    {
        public long DepartmentId { get; set; }
        public string Title { get; set; }
        public string ParentUnitCode { get; set; }
        public string UnitCode { get; set; }
    }
}
