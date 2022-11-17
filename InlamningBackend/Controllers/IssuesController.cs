using InlamningBackend.Contexts;
using InlamningBackend.Migrations;
using InlamningBackend.Models.Entities;
using InlamningBackend.Models.Requests.IssueRequests;
using InlamningBackend.Models.Responses.CommentResponses;
using InlamningBackend.Models.Responses.IssueResponses;
using InlamningBackend.Models.Responses.StatusResponses;
using InlamningBackend.Models.Responses.UserResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;
using System.Xml.Linq;

namespace InlamningBackend.Controllers
{
    [Route("api/issues")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly DataContext _context;

        public IssuesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(IssueRequest req)
        {
            try
            {
                var datetime = DateTime.Now;
                var issue = new IssueEntity()
                {
                    Title = req.Title,
                    Description = req.Description,
                    UserId = req.UserId,
                    Created = datetime,
                    Modified = datetime,
                    StatusId = 1
                };
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return new OkObjectResult(new IssuePostResponse
                {
                    Title = req.Title,
                    Description = issue.Description,
                    Created = issue.Created,
                    UserId = issue.UserId
                });
            }
            
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetIssues()
        {
            try
            {
                var issues = new List<IssueResponse>();
                    foreach (var issue in await _context.Issues.Include(x => x.Status).Include(x => x.User).Include(x => x.Comments).ToListAsync())
                    {
                        var comments = new List<CommentEntity>();
                        if (issue.Comments != null)
                        {

                            foreach (var comment in issue.Comments)
                                comments.Add(new CommentEntity
                                {
                                    Id = comment.Id,
                                    Created = comment.Created,
                                    Message = comment.Message,
                                    UserId = comment.UserId,
                                    IssueId = comment.IssueId
                                });
                        }
                        issues.Add(new IssueResponse
                        {
                            Id = issue.Id,
                            Created = issue.Created,
                            Title = issue.Title,
                            Description = issue.Description,
                            Status = new StatusResponse
                            {
                                Id = issue.Status.Id,
                                Status = issue.Status.Status
                            },
                            User = new UserResponse
                            {
                                Id = issue.User.Id,
                                FirstName = issue.User.FirstName,
                                LastName = issue.User.LastName,
                                Email = issue.User.Email,
                                PhoneNumber = issue.User.PhoneNumber
                            },
                            Comments = comments
                        });
                    }
                    return new OkObjectResult(issues);
                }
              
                
            
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var issueEntity = await _context.Issues.Include(x => x.User).Include(x => x.Status).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
                if (issueEntity != null)

                {
                    var comments = new List<CommentEntity>();
                    foreach (var comment in issueEntity.Comments)
                        comments.Add(new CommentEntity
                        {
                            Id = comment.Id,
                            Message = comment.Message,
                            Created = comment.Created,
                            UserId  = comment.UserId
                        });

                    return new OkObjectResult(new IssueResponse
                    {
                        Id = issueEntity.Id,
                        Title = issueEntity.Title,
                        Description = issueEntity.Description,
                        Created = issueEntity.Created,
                        Modified = issueEntity.Modified,
                        User = new UserResponse
                        {
                            Id = issueEntity.User.Id,
                            FirstName = issueEntity.User.FirstName,
                            LastName = issueEntity.User.LastName,
                            Email = issueEntity.User.Email,
                            PhoneNumber = issueEntity.User.PhoneNumber
                        },
                        Status = new StatusResponse
                        {
                            Id = issueEntity.Status.Id,
                            Status = issueEntity.Status.Status
                        },
                        Comments = comments
                    });
                }
               
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, IssueUpdateRequest req)
        {
            try
            {
                var _issue = await _context.Issues.FindAsync(id);
                _issue.StatusId = req.StatusId;
                _issue.Modified = DateTime.Now;


                _context.Entry(_issue).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var issue = await _context.Issues.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);
                return new OkObjectResult(new IssueUpdateResponse
                {
                    Status = issue.Status.Status,
                    Modified = issue.Modified
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new NotFoundResult();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueEntity(int id)
        {
            var issueEntity = await _context.Issues.FindAsync(id);
            if (issueEntity == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issueEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
