using SystemManagement.Shared.Attributes;

namespace SystemManagement.Shared.Models.Input.Sample
{
    public class InputAddWithReturnMessage
    {
        public int Id { get; set; }

        [StoredProcedureParameter(Size = 1000)]
        public string Description { get; set; }      // Has size limit
    }
}
