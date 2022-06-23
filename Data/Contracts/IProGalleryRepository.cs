using Data.Models;
using Data.Repositories;
using Entities.Products;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProGalleryRepository : IRepository<ProGallery>
    {
        Task<long> AddAsync(ProGalleryDto proGalleryDto, string RegisterUserId, List<IFormFile> image, CancellationToken cancellationToken);

        ListProGalleryDto GetListPorGalleryByProductId(int productId);

        Task Activate(int id, CancellationToken cancellationToken);
    }
}
