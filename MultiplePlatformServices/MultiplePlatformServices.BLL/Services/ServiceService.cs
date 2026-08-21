using Mapster;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class ServiceService : IServiceService
    {

        private readonly IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<ServiceResponse> CreateAsync(ServiceRequset request)
        {
            var service = request.Adapt<Service>();
            var result = await _serviceRepository.CreateAsync(service);

            var createdService = await _serviceRepository.GetOne(
                s => s.Id == result.Id,
                includes:
                [
                    nameof(Service.Freelancer),
                    nameof(Service.ServiceCategory)
                ]
            );

            return createdService.Adapt<ServiceResponse>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deletedService = await _serviceRepository.GetOne(s=>s.Id == id);
            if(deletedService is null) return false;

            var result = await _serviceRepository.DeleteAsync(deletedService);
            return result;
        }

        public async Task<List<ServiceResponse>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync(
                 includes:
                [
                    nameof(Service.Freelancer),
                    nameof(Service.ServiceCategory)
                ]
                );
            return services.Adapt<List<ServiceResponse>>();
        }

        public async Task<ServiceResponse?> GetByIdAsync(int id)
        {
            var service = await _serviceRepository.GetOne(s=>s.Id == id,
             includes:
                [
                    nameof(Service.Freelancer),
                    nameof(Service.ServiceCategory)
                ]
            );
            if(service is null) return null
                    ;
            return service.Adapt<ServiceResponse?>();
        }

        public async Task<ServiceResponse?> GetByTitle(string title)
        {
            var service = await _serviceRepository.GetOne(s=>s.Title == title,
             includes:
                [
                    nameof(Service.Freelancer),
                    nameof(Service.ServiceCategory)
                ]
            );
            if(service is null) return null;
            return service.Adapt<ServiceResponse?>();

        }

        public async Task<bool> UpdateAsync(int id, ServiceRequset requset)
        {
            
            var updatedService = await _serviceRepository.GetOne(s => s.Id == id);
            if (updatedService is null) return false;

            updatedService.Title = requset.Title;
            updatedService.Description = requset.Description;
            updatedService.Price = requset.Price;
            updatedService.DeliveryDays = requset.DeliveryDays;
            updatedService.Image = requset.Image;
            updatedService.IsActive = requset.IsActive;
            updatedService.ServiceCategoryId = requset.ServiceCategoryId;

            return await _serviceRepository.UpdateAsync(updatedService);

        }
    }
}
