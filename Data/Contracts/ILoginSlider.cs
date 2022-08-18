using Data.Models;
using Data.Repositories;
using Entities.Media;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ILoginSlider : IRepository<LoginSlider>
    {
        Task AddLoginSlider(LoginSliderDto LoginsliderDto, List<IFormFile> Image, CancellationToken cancellationToken);

        List<Slider> GetLoginSliders();

        ListLoginSliderDto GetListLoginSliderDto(int PageNum = 1);

        Task Enable(int id, CancellationToken cancellationToken);
        Task Disable(int id, CancellationToken cancellationToken);
    }
}
