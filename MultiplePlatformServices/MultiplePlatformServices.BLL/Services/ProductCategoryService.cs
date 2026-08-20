using Azure.Messaging;
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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        public async Task<List<ProductCategoryResponse>> GetAllAsync()
        {
            var results = await _productCategoryRepository.GetAllAsync();
            return results.Adapt<List<ProductCategoryResponse>>();
        }

        public async Task<ProductCategoryResponse> GetByIdAsync(int id)
        {
            var result = await _productCategoryRepository.GetOne(c => c.Id == id, []);
            return result.Adapt<ProductCategoryResponse>();
        }


        public async Task<ProductCategoryResponse> CreateAsync(ProductCategoryRequest request)
        {
            var result = request.Adapt<ProductCategory>();
            var category = await _productCategoryRepository.CreateAsync(result);
            if (category != null)
                return new ProductCategoryResponse()
                {
                    Name = request.Name,
                    Description = request.Description,
                };
            return null;
        }

        public async Task<ProductCategoryResponse?> UpdateAsync(int id, ProductCategoryRequest request)
        {
            var result = request.Adapt<ProductCategory>();
            var category = await _productCategoryRepository.GetOne(c=>c.Id == id);
            if (category == null) return null;
            category.Name = request.Name;
            category.Description = request.Description;

            await _productCategoryRepository.UpdateAsync(category);
            return new ProductCategoryResponse
            {
                Name = request.Name,
                Description = request.Description,
            };   
        }


        public async Task<ProductCategoryResponse?> DeleteAsync(int id)
        {
            var result = await _productCategoryRepository.GetOne(c=> c.Id == id);
            if (result == null) return null;

            await _productCategoryRepository.DeleteAsync(result);
            return new ProductCategoryResponse
            {
                Name = result.Name,
                Description = "Deleted Successfully"
            };
        }

        

       

        
    }
}
