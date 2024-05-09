using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Users;

namespace GymManager.DataAccess.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        Task<User> GetAsync(int id);
        Task<User> AddAsync(User user);

        Task<User> UpdateAsync(User user);

        Task DeleteAsync(int id);
    }
}
