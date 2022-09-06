using Data.Contracts;
using Data.Models;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ContactUsRepository : Repository<Contactus>, IContactUsRepository
    {
        public ContactUsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public ContactusDto GetContactus()
        {
            var contactus = Table.FirstOrDefault();

            var res = new ContactusDto()
            {
                Description = contactus.Description,
                Address = contactus.Address,
                Email = contactus.Email,
                Phone = contactus.Phone,
                Id = contactus.Id
            };

            return res;
        }

        public async Task UpdateAsync(ContactusDto contactusDto, CancellationToken cancellationToken)
        {
            var contactus = await base.GetByIdAsync(cancellationToken, contactusDto.Id);

            contactus.Description = contactusDto.Description;
            contactus.Address = contactusDto.Address;
            contactus.Email = contactusDto.Email;
            contactus.Phone = contactusDto.Phone;

            await base.UpdateAsync(contactus, cancellationToken);

        }
    }
}
