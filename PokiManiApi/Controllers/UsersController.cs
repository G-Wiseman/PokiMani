using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Data;
using PokiMani.Models;
using PokiMani.Models.Entities;


namespace PokiMani.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }


        [HttpGet()]
        public IActionResult getAllUsers()
        {
           var allUsers =  dbContext.Users.ToList();

            return Ok(allUsers);
        }


        [HttpPost]
        public IActionResult AddUser(AddUserDto userDto)
        {
            var userEntity = new User() { 
                Email = userDto.Email,
                Name = userDto.Name,
                Phone = userDto.Phone
            };

            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
