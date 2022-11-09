namespace InlamningBackend.Models.Responses.IssueResponses
{
    public class IssuePostResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Guid UserId { get; set; }
    }
}
