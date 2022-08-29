using Data.Models;
using Data.Repositories;
using Entities.Pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IPharmacyProduct: IRepository<PharmacyProducts>
    {
        Task AddPharmacyProduct(PharmacyProductDto pharmacyProductDto, CancellationToken cancellationToken);

        Task EditPharmacyProduct(PharmacyProductDto pharmacyProductDto, CancellationToken cancellationToken);

        PharmacyProductDto GetProductPharmactById(int id);

        List<PharmacyProducts> GetPharmacyProducts();

        ListPharmacyProductDto GetListPharmacyProducts(int PageNum = 1, int PageSize = 0);

        Task Confirm(int id, CancellationToken cancellationToken);
        Task Cancel(int id, CancellationToken cancellationToken);
        // Find the minimum price of theproduct
        long FindMinimumPriceOfProduct(int productId);

        ListPharmacyWithProductDto FindListPharmacyProductWhenExistProduct(int productId);



    }
}
