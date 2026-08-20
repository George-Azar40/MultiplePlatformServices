using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategoryResponse>> GetAllAsync();
        Task<ProductCategoryResponse> GetByIdAsync(int id);
        Task<ProductCategoryResponse> CreateAsync(ProductCategoryRequest request);
        Task<ProductCategoryResponse?> UpdateAsync(int id,ProductCategoryRequest request);
        Task<ProductCategoryResponse?> DeleteAsync(int id);
    }
}
