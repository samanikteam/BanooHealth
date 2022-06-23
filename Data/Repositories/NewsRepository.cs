using Data.Contracts;
using Data.Models;
using Entities.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task AddEmail(NewsDto newsDto, CancellationToken cancellationToken)
        {
            News news = new News()
            {
                Email = newsDto.Email,
                RegisterDate = newsDto.RegisterDate
            };

            await base.AddAsync(news, cancellationToken);
        }
    }
}
