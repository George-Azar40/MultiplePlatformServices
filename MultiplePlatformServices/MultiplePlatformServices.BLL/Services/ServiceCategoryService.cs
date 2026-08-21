using Mapster;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository;
using MultiplePlatformServices.DAL.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository;
        public ServiceCategoryService(IServiceCategoryRepository serviceCategoryRepository)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
        }


        public async Task<List<ServiceCategoryResponse?>> GetAllAsync()
        {

            var categories = await _serviceCategoryRepository.GetAllAsync();
            if(categories is null) return null;
            return categories.Adapt<List<ServiceCategoryResponse>>();

        }
        public async Task<ServiceCategoryResponse?> GetById(int id)
        {
            var category = await _serviceCategoryRepository.GetOne(c=>c.Id == id);
            if(category == null) return null;
            return category.Adapt<ServiceCategoryResponse>();

        }

        public async Task<ServiceCategoryResponse?> GetByName(string name)
        {
            var category = await _serviceCategoryRepository.GetOne(c=> c.Name == name);
            if (category == null) return null;

            return category.Adapt<ServiceCategoryResponse>();

        }


        public async Task<ServiceCategoryResponse> CreateAsync(ServiceCategoryRequest request)
        {
            var category = request.Adapt<ServiceCategory>();

            var result = await _serviceCategoryRepository.CreateAsync(category);
            return result.Adapt<ServiceCategoryResponse>();
        }





        public async Task<bool> UpdateAsync(int id, ServiceCategoryRequest request)
        {
            var updatedCategory = await _serviceCategoryRepository.GetOne(c=> c.Id == id);
            if (updatedCategory is null) return false;

            updatedCategory.Name = request.Name;
            updatedCategory.Description = request.Description;
            updatedCategory.Image = request.Image;
            updatedCategory.IsActive = request.IsActive;
            return await _serviceCategoryRepository.UpdateAsync(updatedCategory);
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deletedCategory = await _serviceCategoryRepository.GetOne(c => c.Id == id);
            if (deletedCategory is null) return false;
            return await _serviceCategoryRepository.DeleteAsync(deletedCategory);
        }

       
       

    }
}
