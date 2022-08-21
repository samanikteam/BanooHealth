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
        public async Task AddEmail(string Email, CancellationToken cancellationToken)
        {
            News news = new News()
            {
                Email = Email,
                //RegisterDate = da
            };

            await base.AddAsync(news, cancellationToken);
        }
    }
}
