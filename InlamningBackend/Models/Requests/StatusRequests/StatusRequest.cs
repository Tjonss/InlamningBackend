using System.ComponentModel.DataAnnotations;

namespace InlamningBackend.Models.Requests.StatusRequests
{
    public class StatusRequest
    {
        [Required]
        public string Status { get; set; }
    }
}
