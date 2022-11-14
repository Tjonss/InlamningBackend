using InlamningBackend.Contexts;
using InlamningBackend.Models.Entities;
using InlamningBackend.Models.Requests.UserRequests;
using InlamningBackend.Models.Responses.UserResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InlamningBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest req)
        {
            try
            {
                if (!await _context.Users.AnyAsync(x => x.Email == req.Email))
                {
                    var user = new UserEntity 
                    { 
                        FirstName = req.FirstName,
                        LastName = req.LastName, 
                        Email = req.Email, 
                        PhoneNumber = req.PhoneNumber
                    };
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(new UserResponse
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message);}
            return new BadRequestResult();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = new List<UserResponse>();
                foreach (var user in await _context.Users.ToListAsync())
                    users.Add(new UserResponse
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
                return new OkObjectResult(users);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                    return new OkObjectResult(new UserResponse
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message);}
   
            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEntity(Guid id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
