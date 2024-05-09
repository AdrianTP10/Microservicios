using AutoMapper;
using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Users;
using UserMicroservice.Users.Dto;

namespace UserMicroservice.ApplicationServices
{
    public class UsersAppService : IUsersAppService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UsersAppService(IUserRepository repository, IMapper mapper) 
        { 
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<int> AddUserAsync(UserDto userdto)
        {
            var user = _mapper.Map<User>(userdto);
            await _repository.AddAsync(user);
            return userdto.Id;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _repository.GetAsync(userId);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _repository.DeleteAsync(userId);
        }

        public async Task EditUserAsync(UserDto userdto)
        {
            var user = _mapper.Map<User>(userdto);
            await _repository.UpdateAsync(user);
        }

        
    }
}
