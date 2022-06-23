using Data.Models;
using Data.Repositories;
using Entities.pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IPharmacyRepository : IRepository<Pharmacy>
    {

        Task AddPharmacy(PharmacyDto pharmacyDto, CancellationToken cancellationToken);

        List<Pharmacy> GetPharmacies();

        PharmacyDto GetPharmacyById(int id);

        ListPharmacyDto GetListPharmacy();

        Task UpdateAsync(PharmacyDto dto, CancellationToken cancellationToken);


        Task Enable(int id, CancellationToken cancellationToken);
        Task Disable(int id, CancellationToken cancellationToken);
    }
}
