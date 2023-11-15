using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemManagement.Shared.Models.Output.AssignFormToUser
{
    public class OutputGetAllMainDepartment
    {
        public long DepartmentId { get; set; }
        public string UnitCode { get; set; }
        public string Title { get; set; }
        public string ParentUnitCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}
