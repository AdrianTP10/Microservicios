using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Users;
using UserMicroservice.Users.Dto;

namespace UserMicroservice.ApplicationServices
{
    public interface IUsersAppService
    {
        Task<List<User>> GetUsersAsync();

        Task<int> AddUserAsync(UserDto userdto);

        Task DeleteUserAsync(int userId);
        

        Task<User> GetUserByIdAsync(int userId);

        Task EditUserAsync(UserDto userdto);
    }
}
