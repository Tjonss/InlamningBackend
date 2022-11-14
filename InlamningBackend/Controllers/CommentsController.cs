using InlamningBackend.Contexts;
using InlamningBackend.Models.Entities;
using InlamningBackend.Models.Requests.CommentRequests;
using InlamningBackend.Models.Responses.CommentResponses;
using InlamningBackend.Models.Responses.UserResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InlamningBackend.Controllers
{
    [Route("api/comments")]
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var comments = new List<CommentEntity>();
                foreach (var comment in await _context.Comments.ToListAsync())
                comments.Add(comment);
                return new OkObjectResult(comments);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentEntity(int id)
        {
            var commentEntity = await _context.Comments.FindAsync(id);
            if (commentEntity == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
