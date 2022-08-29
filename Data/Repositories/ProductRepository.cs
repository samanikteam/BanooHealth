using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisitorManagment.Core.Convertors;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly IProductArticleRepository _productArticleRepository;

        private readonly IProCategoriesRepository _proCategoryRepository;
        public ProductRepository(ApplicationDbContext dbContext, IProCategoriesRepository proCategoryRepository, IProductArticleRepository productArticleRepository) : base(dbContext)
        {
            _proCategoryRepository = proCategoryRepository;
            _productArticleRepository = productArticleRepository;
        }
        public async Task<int> AddAsync(ProductDto productDto, string RegisterUserId, List<IFormFile> Image1, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Title = productDto.Title,
                Description = productDto.Description,
                AvatarTitle1 = productDto.AvatarTitle1,
                AvatarAlt1 = productDto.AvatarAlt1,
                Text = productDto.Text,
                Slug = productDto.Slug,
                KeyWords = productDto.KeyWords,
                Country = productDto.Country,
                RegisterDate = DateTime.Now,
                RegisterUserId = RegisterUserId,
                IsDelete = false,
                BrandName = productDto.BrandName,
                popular = productDto.popular
            };

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image1)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        product.Avatar1 = stream.ToArray();
                    }
                }
            }


            #endregion

            await base.AddAsync(product, cancellationToken);

            return product.Id;
        }

        public ListProductDto GetListProduct(int PageNum = 1)
        {
            var product = Table.Include(x => x.PharmacyProducts).ThenInclude(x => x.Pharmacy);
            var take = 8;
            var skip = (PageNum - 1) * take;
            var list = new ListProductDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = product.Count();
            list.PageCount = (int)Math.Ceiling(product.Count() / (double)take);

            list.Products = product.Select(t => new ProductDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description.Substring(0, 100),
                //ProductArticles = t.ProductArticles.Select(x => x.ArticleId).ToList(),
                //CategoryId = t.Product,
                Avatar1 = t.Avatar1,
                AvatarTitle1 = t.AvatarTitle1,
                AvatarAlt1 = t.AvatarAlt1,
                Slug = t.Slug,
                KeyWords = t.KeyWords,
                Country = t.Country,
                RegisterDate = t.RegisterDate,
                RegisterDateFa = t.RegisterDate.ToShamsi(),
                RegisterUserId = t.RegisterUserId,
                IsDelete = t.IsDelete,
                MinimumPrice = t.PharmacyProducts.Where(x => x.productId == t.Id).Select(x => x.Price).Min().ToString(),
                //Table.Where(x=>x.productId==productId && x.Inventory!=0).Include(x=>x.Pharmacy).ToList();
                CountPharmacyExistProduct = t.PharmacyProducts.Where(x => x.productId == t.Id && x.Inventory != 0).Count(),
                BrandName = t.BrandName,
                popular = t.popular
            }).OrderBy(u => u.Title).Skip(skip).Take(take).ToList();

            return list;
        }


        public async Task<bool> IsExistProduct(string title)
        {
            return await TableNoTracking.AnyAsync(p => p.Title == title);
        }

        public ProductDto GetProductByProductId(int id)
        {
            var result = Table.Where(x => x.Id == id).Include(x => x.ProductArticles)
                .ThenInclude(x => x.Article)
                .Include(x => x.ProductArticles).ThenInclude(x => x.Product)
                .SingleOrDefault();

            var listCategoryId = _proCategoryRepository.GetListCategoryWithProductId(id);

            var listArticleId = _productArticleRepository.GetListArticleByProductId(id);


            var product = new ProductDto
            {
                Id = result.Id,
                Title = result.Title,
                CategoryId = listCategoryId,
                ListArticleId = listArticleId,
                Description = result.Description,
                Text = result.Text,
                Avatar1 = result.Avatar1,
                AvatarTitle1 = result.AvatarTitle1,
                AvatarAlt1 = result.AvatarAlt1,
                Slug = result.Slug,
                KeyWords = result.KeyWords,
                Country = result.Country,
                RegisterDate = result.RegisterDate,
                ListArticleLinked = result.ProductArticles.Select(x => x.Article).ToList(),
                ListProductLinked = result.ProductArticles.Select(x => x.Product).ToList(),
                IsDelete = result.IsDelete,
                BrandName = result.BrandName,
                popular = result.popular
                //Price = result.PharmacyProducts.Select(x => x.Price).SingleOrDefault().ToString(),
                //MinimumPrice = result.PharmacyProducts.Select(x => x.Price).Min().ToString(),

            };
            return product;
        }


        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var product = GetById(id);
            product.Active();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var product = GetById(id);
            product.Deactive();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public List<Product> searchProduct(string title)
        {
            List<Product> products = Table.Where(c => c.Title.Contains(title)).ToList();

            return products;
        }

        public List<Product> GetProducts()
        {
            var result = Table.ToList();
            return result;
        }

        public async Task<int> UpdateAsync(ProductDto productDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            var product = await base.GetByIdAsync(cancellationToken, productDto.Id);

            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.AvatarTitle1 = productDto.AvatarTitle1;
            product.AvatarAlt1 = productDto.AvatarAlt1;
            product.Text = productDto.Text;
            product.Slug = productDto.Slug;
            product.KeyWords = productDto.KeyWords;
            product.Country = productDto.Country;
            product.LastUpdateDate = DateTime.Now;
            product.LastUpdateUserId = RegisterUserId;
            product.IsDelete = false;
            product.BrandName = productDto.BrandName;
            product.popular = productDto.popular;

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        product.Avatar1 = stream.ToArray();
                    }
                }
            }

            await base.UpdateAsync(product, cancellationToken);
            return productDto.Id;
        }

        public ListProductDto GetListProductsByProductCategoryId(int productCategoryId, int PageNum = 1)
        {
            var listProductId = _proCategoryRepository.GetListProductIdWithProductCategoryId(productCategoryId);

            var products = new List<Product>();

            foreach (var item in listProductId)
            {
                var product = Table.Where(x => x.Id == item).FirstOrDefault();

                products.Add(product);
            }
            var take = 8;
            var skip = (PageNum - 1) * take;
            var list = new ListProductDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = products.Count();
            list.PageCount = (int)Math.Ceiling(products.Count() / (double)take);

            list.Products = products.Select(t => new ProductDto
            {
                Id = t.Id,
                Title = t.Title,
                BrandName = t.BrandName,
                Description = t.Description,
                Avatar1 = t.Avatar1,
                AvatarTitle1 = t.AvatarTitle1,
                AvatarAlt1 = t.AvatarAlt1,
                Slug = t.Slug,
                KeyWords = t.KeyWords,
                Country = t.Country,
                RegisterDate = t.RegisterDate,
                RegisterUserId = t.RegisterUserId,
                IsDelete = t.IsDelete,
                popular = t.popular
            }).OrderBy(u => u.Title).Skip(skip).Take(take).ToList();

            return list;
        }
    }
}

