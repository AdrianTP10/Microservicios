using Microsoft.AspNetCore.Mvc;
using UserMicroservice.ApplicationServices;
using UserMicroservice.Core.Users;
using UserMicroservice.Users.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersAppService _usersAppService;
        readonly ILogger<UsersController> _logger;
        public UsersController(IUsersAppService usersAppService, ILogger<UsersController> logger)
        {
            _usersAppService = usersAppService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await _usersAppService.GetUsersAsync();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            return await _usersAppService.GetUserByIdAsync(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<Int32> Insert([FromBody] UserDto userdto)
        {
            var result = await _usersAppService.AddUserAsync(userdto);
            return result;

        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task Edit([FromBody] UserDto userdto)
        {
            await _usersAppService.EditUserAsync(userdto);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _usersAppService.DeleteUserAsync(id);
            
        }
    }
}
