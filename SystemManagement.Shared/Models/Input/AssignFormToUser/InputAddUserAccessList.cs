namespace SystemManagement.Shared.Models.Input.AssignFormToUser
{
    public class InputAddUserAccessList
    {
        public long UserId { get; set; }
        public long EventId { get; set; }
        public long RequesterUserId { get; set; }
        public long VerifyUserId { get; set; }
    }
}
