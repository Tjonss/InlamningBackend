namespace InlamningBackend.Models.Responses.CommentResponses
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
    }
}
