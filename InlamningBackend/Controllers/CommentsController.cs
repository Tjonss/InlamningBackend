using InlamningBackend.Contexts;
using InlamningBackend.Models.Entities;
using InlamningBackend.Models.Requests.CommentRequests;
using InlamningBackend.Models.Responses.CommentResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InlamningBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentRequest req) 
        {
            try
            {
                var comment = new CommentEntity
                {
                    Message = req.Message,
                    Created = DateTime.Now,
                    IssueId = req.IssueId,
                    UserId = req.UserId,
                };
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return new OkObjectResult(new CommentResponse {
                    Id = comment.Id,
                    Comment = comment.Message,
                    UserId = comment.UserId,
                    Created = comment.Created
                });
            }
            catch (Exception ex ) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
            }
    }
}
