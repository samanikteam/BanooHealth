﻿using Data.Models;
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
    public interface IProductCategoryRepository:IRepository<ProductCategory>
    {
        Task AddProductCategory(ProductCategoryDto ProductCategoryDto, List<IFormFile> Image, CancellationToken cancellationToken);
        Task<bool> IsExistProductCategory(string title, int id);
        List<ProductCategory> GetProductCategories();
        ListProductCategoryDto GetListProductCategory();

        Task Active(int id, CancellationToken cancellationToken);
        Task Deactive(int id, CancellationToken cancellationToken);
        public ListProductCategoryDto GetListProductCategoryByCategoryId(int categoryId);
    }
}
