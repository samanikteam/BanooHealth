using Data.Contracts;
using Data.Models;
using Entities.Pharmacies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PharmacyProductsRepository : Repository<PharmacyProducts>, IPharmacyProduct
    {
        public IPharmacyProduct _pharmacyProductRepository;
        public PharmacyProductsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddPharmacyProduct(PharmacyProductDto Dto, CancellationToken cancellationToken)
        {
            PharmacyProducts pharmacyProduct = new PharmacyProducts()
            {
                PharmacyId = Dto.PharmacyId,
                productId = Dto.productId,
                Inventory = Dto.Inventory,
                Price = Dto.Price,
                SortId = 1,
                Discount = Dto.Discount,
                RegisterUserId = "Admin",
                LastUpdateDate = Dto.LastUpdateDate,
                LastUpdateUserId = "Admin",
                RegisterDate = DateTime.Now,
                IsDelete = false,
                LinkAddress = Dto.LinkAddress,
            };

            await base.AddAsync(pharmacyProduct, cancellationToken);
        }

        public async Task Cancel(int id, CancellationToken cancellationToken)
        {
            var pharmacyProduct = GetById(id);
            pharmacyProduct.IsDelete = true;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Confirm(int id, CancellationToken cancellationToken)
        {
            var pharmacyProduct = GetById(id);
            pharmacyProduct.IsDelete = false;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }



        public ListPharmacyProductDto GetListPharmacyProducts(int PageNum = 1, int PageSize = 0)
        {
            var pharmacyProduct = Table.Include(x => x.Product).Include(x => x.Pharmacy).OrderByDescending(a => a.RegisterDate);

            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListPharmacyProductDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = pharmacyProduct.Count();
            list.PageCount = (int)Math.Ceiling(pharmacyProduct.Count() / (double)take);

            list.PharmacyProducts = pharmacyProduct.Select(t => new PharmacyProductDto()
            {
                Id = t.Id,
                Discount = t.Discount,
                IsDelete = t.IsDelete,
                Inventory = t.Inventory,
                LastUpdateDate = t.LastUpdateDate,
                LastUpdateUserId = t.LastUpdateUserId,
                productId = t.productId,
                ProductName = t.Product.Title,
                PharmacyId = t.PharmacyId,
                PharmacyName = t.Pharmacy.Name,
                Price = t.Price,
                RegisterDate = t.RegisterDate,
                RegisterUserId = t.RegisterUserId,
                SortId = 1,
                LinkAddress = t.LinkAddress
            }).OrderBy(a=>a.PharmacyName).Skip(skip).Take(take).ToList();

            return list;
        }

        public List<PharmacyProducts> GetPharmacyProducts()
        {
            return Table.ToList();
        }

        public long FindMinimumPriceOfProduct(int productId)
        {
            var result = Table.Where(x => x.productId == productId).Select(x => x.Price).ToList();
            var MinimumAmount = result.Min();

            return MinimumAmount;
        }

        public ListPharmacyWithProductDto FindListPharmacyProductWhenExistProduct(int productId)
        {
            var result = Table.Where(x => x.productId == productId).Include(x => x.Pharmacy).Include(x => x.Product).AsSplitQuery();

            var list = new ListPharmacyWithProductDto();

            list.pharmaciesWithProduct = result.Select(t => new PharmacyWithProductDto()
            {
                Id = t.Id,
                Name = t.Pharmacy.Name,
                Address = t.Pharmacy.Address,
                City = t.Pharmacy.City,
                DrName = t.Pharmacy.DrName,
                Mobile = t.Pharmacy.Mobile,
                PharmacyCode = t.Pharmacy.PharmacyCode,
                Phone = t.Pharmacy.Phone,
                Province = t.Pharmacy.Province,
                Username = t.Pharmacy.Username,
                IsActive = t.Pharmacy.IsActive,
                Registerdate = t.Pharmacy.Registerdate,
                //product
                ProductId = t.Product.Id,
                ProductTitle = t.Product.Title,
                ProductPrice = t.Price,
                LinkAddress=t.LinkAddress
                

            }).ToList();


            return list;
        }
        public async Task EditPharmacyProduct(PharmacyProductDto pharmacyProductDto, CancellationToken cancellationToken)
        {
            var pharmacyproduct = await base.GetByIdAsync(cancellationToken, pharmacyProductDto.Id);
            pharmacyproduct.Inventory = pharmacyProductDto.Inventory;
            pharmacyproduct.LastUpdateDate = DateTime.Now;
            pharmacyproduct.Price = pharmacyProductDto.Price;
            pharmacyproduct.Discount = pharmacyProductDto.Discount;
            pharmacyproduct.SortId = pharmacyProductDto.SortId;
            pharmacyproduct.LinkAddress = pharmacyProductDto.LinkAddress;

            await base.UpdateAsync(pharmacyproduct, cancellationToken);

        }

        public PharmacyProductDto GetProductPharmactById(int id)
        {
            var pharmacyProduct = GetById(id);

            var res = new PharmacyProductDto()
            {
                Id = pharmacyProduct.Id,
                Discount = pharmacyProduct.Discount,
                SortId = pharmacyProduct.SortId,
                Price = pharmacyProduct.Price,
                IsDelete = pharmacyProduct.IsDelete,
                Inventory = pharmacyProduct.Inventory,
                LinkAddress = pharmacyProduct.LinkAddress

            };

            return res;
        }
    }
}
