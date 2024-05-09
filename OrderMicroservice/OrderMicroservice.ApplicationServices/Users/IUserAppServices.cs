using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Users
{
    public interface IUserAppServices
    {
        Task<User> GetUserAsync(int userId);
        Task<bool> ExistUserAsync(int userId);
    }
}
