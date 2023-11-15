using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemManagement.Shared.Attributes;

namespace SystemManagement.Shared.Models.Input.AssignFormToUser
{
    public class InputGetUserAccessList
    {
      
        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public EventIdList[] EventList { get; set; }
    }
    public class EventIdList
    {
        public long EventId { get; set; }
    }
}
