using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Core.Users;
using OrderMicroservice.DataAccess.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Users
{
    public class UserAppServices : IUserAppServices
    {
        private readonly IRepository<int, User> _repository;
        public UserAppServices(IRepository<int, User> repository)
        {
            _repository = repository;
        }
        public async Task<bool> ExistUserAsync(int userId)
        {
            // Registro del evento
            Log.Information("The method was executed: ExistUserAsync");

            User user = await _repository.GetAsync(userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<User> GetUserAsync(int userId)
        {
            // Registro del evento
            Log.Information("The method was executed: GetUserAsync");

            return await _repository.GetAll().Include(u => u.City).FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
