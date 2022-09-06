using Data.Models;
using Data.Repositories;
using Entities.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<int> AddAsync(ProductDto productDto, string RegisterUserId, List<IFormFile> Image1, CancellationToken cancellationToken);
        Task<int> UpdateAsync(ProductDto productDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken);

        ListProductDto GetListProduct(int PageNum = 1, int PageSize = 12);

        Task<bool> IsExistProduct(string title);

        ProductDto GetProductByProductId(int id );
       

        Task Active(int id, CancellationToken cancellationToken);
        Task Deactive(int id, CancellationToken cancellationToken);
        //search
        public List<Product> searchProduct(string title);
        List<Product> GetProducts();

        ListProductDto GetListProductsByProductCategoryId(int productCategoryId, int PageNum = 1, int PageSize = 0);

    }
}
