using System.ComponentModel.DataAnnotations;

namespace InlamningBackend.Models.Entities
{
    public class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public int IssueId { get; set; }

        [Required]
        public Guid UserId { get; set; }


        public IssueEntity Issue { get; set; }
        public UserEntity User { get; set; }
    }
}
