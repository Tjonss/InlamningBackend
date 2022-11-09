using InlamningBackend.Contexts;
using InlamningBackend.Models.Entities;
using InlamningBackend.Models.Requests.StatusRequests;
using InlamningBackend.Models.Responses.StatusResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InlamningBackend.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatusesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(StatusRequest req)
        {
            try
            {
                var status = new StatusEntity { Status = req.Status };
                _context.Add(status);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex){ Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {
            try
            {
                var statusList = new List<StatusResponse>();

                foreach (var status in await _context.Status.ToListAsync())
                    statusList.Add(new StatusResponse { Id = status.Id, Status = status.Status });
                return new OkObjectResult(statusList);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}
