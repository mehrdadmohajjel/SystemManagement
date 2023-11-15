using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemManagement.Shared.Models.Output.AssignFormToUser
{
    public class OutputGetUserAccessList
    {
        public long Id { get; set; }
        public int  UserId { get; set; }
        public long EventId { get; set; }
        public int RequesterUserId { get; set; }
        public string RequesterUserFullName { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsNewRequested { get; set; }
        public int VerifyUserId { get; set; }
        public DateTime VerifyDate { get; set; }
        public string PersianVerifyDate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public int DeactivatorUserId { get; set; }
        public string DeactivatorUserFullName { get; set; }
        public DateTime DeactiveDate { get; set; }
        public string PersianDeactiveDate { get; set; }
        public string UserFullName { get; set; }
        public string EventName { get; set; }
        public string EventCode { get; set; }
        public string FormName { get; set; }
        public string FormCode { get; set; }
        public string SystemName { get; set; }
        public string SystemCode { get; set; }


    }
}
