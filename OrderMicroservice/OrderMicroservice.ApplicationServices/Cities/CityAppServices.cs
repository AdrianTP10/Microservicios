using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Core.Users;
using OrderMicroservice.DataAccess.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Cities
{
    public class CityAppServices : ICityAppServices
    {
        private readonly IRepository<int, City> _repository;
        public CityAppServices(IRepository<int, City> repository)
        {
            _repository = repository;
        }
        public async Task<bool> ExistCityAsync(int cityId)
        {
            // Registro del evento
            Log.Information("The method was executed: ExistCityAsync");

            City city = await _repository.GetAsync(cityId);
            if (city == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<City> GetCityAsync(int cityId)
        {
            // Registro del evento
            Log.Information("The method was executed: GetCityAsync");

            return await _repository.GetAsync(cityId);
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            // Registro del evento
            Log.Information("The method was executed: GetCitiesAsync");

            return await _repository.GetAll().ToListAsync();
        }
    }
}
