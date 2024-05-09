
using AutoMapper;
using GymManager.DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserMicroservice.Api.Controllers;
using UserMicroservice.ApplicationServices;
using UserMicroservice.Core.Users;
using UserMicroservice.DataAccess;
using UserMicroservice.DataAccess.Repositories;
using UserMicroservice.Users.Dto;

namespace UserMicroservice.UnitTest
{
    [TestFixture]
    public class UsersTests
    {
        protected  TestServer _server;
        [OneTimeSetUp]
        public void Setup()
        {
            this._server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            
        }

        [Test]
        public void InsertUser_Test()
        {
            var _usersAppService = _server.Host.Services.GetService<IUsersAppService>();
            var _context = _server.Host.Services.GetService<UserMicroserviceContext>();

            //Inserting a new city for testing
            _context.Cities.AddAsync(new City { Id = 1, Name="México City" });
            _context.SaveChangesAsync();

            var newUserDto = new UserDto
            {
                FirstName =  "HEctor",
                LastName = "Perez",
                Role = "Admin",
                CityId = 1,
                Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
            };

            var createdUser = _usersAppService.AddUserAsync(newUserDto);

            // Verifying if the user is saved in memory.
            var retrievedUser = _context.Users.FirstOrDefault(u => u.FirstName == newUserDto.FirstName);
            Assert.IsNotNull(retrievedUser);
        }

        [Test]
        public async Task GetAllUsers_Test()
        {
            var _usersAppService = _server.Host.Services.GetService<IUsersAppService>();
            var _context = _server.Host.Services.GetService<UserMicroserviceContext>();

            // Adding some users to the db
            var users = new List<User>
            {
                new User {
                     FirstName =  "HEctor",
                     LastName = "Perez",
                     Role = "Admin",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                     FirstName =  "Irving",
                     LastName = "Torres",
                     Role = "Manager",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                     FirstName =  "Jaime",
                     LastName = "Hernandez",
                     Role = "CEO",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },

            };
            _context.Users.AddRange(users);
            _context.SaveChanges();


            var userList = await _usersAppService.GetUsersAsync();
            Assert.That(users.Count, Is.EqualTo(userList.Count));
        }

        [Test]
        public async Task GetUserById_Test()
        {
            var _usersAppService = _server.Host.Services.GetService<IUsersAppService>();
            var _context = _server.Host.Services.GetService<UserMicroserviceContext>();

            // Adding some users to the db
            var users = new List<User>
            {
                new User {
                    Id = 1,
                     FirstName =  "HEctor",
                     LastName = "Perez",
                     Role = "Admin",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                    Id = 2,
                     FirstName =  "Irving",
                     LastName = "Torres",
                     Role = "Manager",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                    Id=3,
                     FirstName =  "Jaime",
                     LastName = "Hernandez",
                     Role = "CEO",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },

            };
            _context.Users.AddRange(users);
            _context.SaveChanges();



            var user = await _usersAppService.GetUserByIdAsync(3);
           
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task EditUser_Test()
        {
            var _usersAppService = _server.Host.Services.GetService<IUsersAppService>();
            var _context = _server.Host.Services.GetService<UserMicroserviceContext>();
            var _mapper = _server.Host.Services.GetService<IMapper>();


            await _context.Cities.AddAsync(new City { Id = 1, Name = "México City" });
            // Adding some users to the db
            var users = new List<User>
            {
                new User {
                    Id = 1,
                     FirstName =  "HEctor",
                     LastName = "Perez",
                     Role = "Admin",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                    Id = 2,
                     FirstName =  "Irving",
                     LastName = "Torres",
                     Role = "Manager",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                    Id=3,
                     FirstName =  "Jaime",
                     LastName = "Hernandez",
                     Role = "CEO",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },

            };

            //Adding user to be modified
            var user4 = new User
            {
                Id = 4,
                FirstName = "Carlos",
                LastName = "Hernandez",
                Role = "CEO",
                CityId = 1,
                Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
            };
            await _context.Users.AddRangeAsync(users);
            await _context.Users.AddAsync(user4);
            await _context.SaveChangesAsync();

            // Detaching the entity context tracking
            _context.Entry<User>(user4).State = EntityState.Detached;

            user4.FirstName = "New Name";
            await _usersAppService.EditUserAsync(_mapper.Map<UserDto>(user4));
        

            // Searching for the user
            var updatedUser = await _context.Users.FindAsync(4);
            Assert.That(updatedUser.FirstName, Is.EqualTo("New Name"));

        }

        [Test]
        public void DeleteUser_Test()
        {
            var _usersAppService = _server.Host.Services.GetService<IUsersAppService>();
            var _context = _server.Host.Services.GetService<UserMicroserviceContext>();

            // Adding some users to the db
            var users = new List<User>
            {
                new User {
                    Id = 1,
                     FirstName =  "HEctor",
                     LastName = "Perez",
                     Role = "Admin",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                    Id = 2,
                     FirstName =  "Irving",
                     LastName = "Torres",
                     Role = "Manager",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },
                new User {
                    Id=3,
                     FirstName =  "Jaime",
                     LastName = "Hernandez",
                     Role = "CEO",
                     CityId = 1,
                     Birthday = new DateTime(2001, 8, 3, 10, 30, 0)
                },

            };
            _context.Users.AddRange(users);
            _context.SaveChanges();



            _usersAppService.DeleteUserAsync(3);
            var nullUser = _context.Users.FirstOrDefault(u => u.Id == 3);
            Assert.IsNull(nullUser);
        }
    }
}