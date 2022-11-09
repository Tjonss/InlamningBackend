using System.ComponentModel.DataAnnotations;

namespace InlamningBackend.Models.Entities
{
    public class StatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }

        public ICollection<IssueEntity> Issues { get; set; }
    }
}
