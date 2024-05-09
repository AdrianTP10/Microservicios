using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Users;

namespace UserMicroservice.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserMicroserviceContext _context;
        public UserRepository(UserMicroserviceContext context) { _context = context; }

        public async Task<User> AddAsync(User user)
        {
            if (user == null) throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

            try
            {
                var city = await _context.Cities.FindAsync(user.CityId);
                user.City = null;

                await _context.Users.AddAsync(user);
                city.Users.Add(user);

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(user)} could not be saved: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.FindAsync<User>(id);
            _context.Remove<User>(user);
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            try
            {
                return _context.Set<User>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve users: {ex.Message}");
            }
        }

        public async Task<User> GetAsync(int id)
        {
            var user = await _context.FindAsync<User>(id);
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");

            try
            {
                var city = await _context.Cities.FindAsync(user.CityId);
                user.City = null;
                city.Users.Add(user);

                _context.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(user)} could not be updated: {ex.Message}");
            }
        }
    }
}
