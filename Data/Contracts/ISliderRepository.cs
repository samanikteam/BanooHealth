using Data.Models;
using Data.Repositories;
using Entities.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ISliderRepository: IRepository<Slider>
    {
        Task AddSlider(SliderDto sliderDto, CancellationToken cancellationToken);

        List<Slider> GetSliders();

        ListSliderDto GetListSliderDto();

        Task Enable(int id, CancellationToken cancellationToken);
        Task Disable(int id, CancellationToken cancellationToken);

        SliderDto GetSliderById(int id);

        Task UpdateAsync(SliderDto dto, CancellationToken cancellationToken);
    }
}
