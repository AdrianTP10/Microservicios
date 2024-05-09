using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Cities
{
    public interface ICityAppServices
    {
        Task<City> GetCityAsync(int cityId);
        Task<bool> ExistCityAsync(int cityId);
        Task<List<City>> GetCitiesAsync();
    }
}
