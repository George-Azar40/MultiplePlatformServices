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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductResponse?>> GetAllAsync()
        {
            var products =await _productRepository.GetAllAsync(includes: [
                     nameof( Product.Store),
                     nameof( Product.ProductCategory)
                    ]);
            return products.Adapt<List<ProductResponse>>();
        }


        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetOne(
                p => p.Id == id,
                includes: [
                     nameof( Product.Store),
                     nameof( Product.ProductCategory)
                    ]
                );
            if(product == null) return null;

            return product.Adapt<ProductResponse>();
        }


        public async Task<ProductResponse> CreateAsync(ProductRequest request)
        {
            var addedProduct = request.Adapt<Product>();
            var result = await _productRepository.CreateAsync(addedProduct);
            return result.Adapt<ProductResponse>();
        }

        public async Task<bool> UpdateAsync(int id, ProductRequest request)
        {
            var updatedProduct = await _productRepository.GetOne(p=>p.Id == id);
            if (updatedProduct == null) return false;

           
            updatedProduct.Image = request.Image;
            updatedProduct.IsActive = request.IsActive;
            updatedProduct.Price = request.Price;
            updatedProduct.Description = request.Description;
            updatedProduct.StockQuantity = request.StockQuantity;
            updatedProduct.StoreId = request.StoreId;
            updatedProduct.ProductCategoryId = request.ProductCategoryId;

            return await _productRepository.UpdateAsync(updatedProduct);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var deletedProduct = await _productRepository.GetOne(p=> p.Id ==id);    
            if (deletedProduct == null) return false;

            return await _productRepository.DeleteAsync(deletedProduct);

        }

    }
}
