
using Data.Contracts;
using Data.Models;
using Entities.pharmacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisitorManagment.Core.Security;

namespace Data.Repositories
{
    public class PharmacyRepository : Repository<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddPharmacy(PharmacyDto pharmacyDto, CancellationToken cancellationToken)
        {
            Pharmacy pharmacy = new Pharmacy()
            {
                Name = pharmacyDto.Name,
                Address = pharmacyDto.Address,
                City = pharmacyDto.City,
                DrName = pharmacyDto.DrName,
                Mobile = pharmacyDto.Mobile,
                Password = PasswordHelper.EncodePasswordMd5(pharmacyDto.Password),
                PharmacyCode = pharmacyDto.PharmacyCode,
                Phone = pharmacyDto.Phone,
                Province = pharmacyDto.Province,
                Username = pharmacyDto.Username,
                IsActive = pharmacyDto.IsActive,
                Registerdate = DateTime.Now
            };
            await base.AddAsync(pharmacy, cancellationToken);
        }

        public async Task Disable(int id, CancellationToken cancellationToken)
        {
            var pharmacy = GetById(id);
            pharmacy.IsActive = false;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Enable(int id, CancellationToken cancellationToken)
        {
            var pharmacy = GetById(id);
            pharmacy.IsActive = true;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public PharmacyDto GetPharmacyById(int id)
        {
            var pharmacy = GetById(id);


            var res = new PharmacyDto()
            {
                Id = pharmacy.Id,
                Address = pharmacy.Address,
                City = pharmacy.City,
                DrName = pharmacy.DrName,
                IsActive = pharmacy.IsActive,
                Mobile = pharmacy.Mobile,
                Name = pharmacy.Name,
                Password = pharmacy.Password,
                PharmacyCode = pharmacy.PharmacyCode,
                Phone = pharmacy.Phone,
                Province = pharmacy.Province,
                Registerdate = pharmacy.Registerdate,
                Username = pharmacy.Username
            };

            return res;
        }

        public ListPharmacyDto GetListPharmacy(int PageNum = 1, int PageSize = 12)
        {
            var pharmacies = Table.OrderByDescending(a => a.Registerdate);
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListPharmacyDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = pharmacies.Count();
            list.PageCount = (int)Math.Ceiling(pharmacies.Count() / (double)take);
            list.pharmacies = pharmacies.Select(t => new PharmacyDto()
            {
                Id = t.Id,
                Name = t.Name,
                Address = t.Address,
                City = t.City,
                DrName = t.DrName,
                Mobile = t.Mobile,
                PharmacyCode = t.PharmacyCode,
                Phone = t.Phone,
                Province = t.Province,
                Username = t.Username,
                IsActive = t.IsActive,
                Registerdate = t.Registerdate

            }).OrderBy(p => p.Name).Skip(skip).Take(take).ToList();

            return list;
        }

        public List<Pharmacy> GetPharmacies()
        {
            return Table.ToList();
        }

        public async Task UpdateAsync(PharmacyDto dto, CancellationToken cancellationToken)
        {
            var pharmacy = await base.GetByIdAsync(cancellationToken, dto.Id);

            pharmacy.Name = dto.Name;
            pharmacy.Address = dto.Address;
            pharmacy.City = dto.City;
            pharmacy.DrName = dto.DrName;
            pharmacy.Mobile = dto.Mobile;
            pharmacy.PharmacyCode = dto.PharmacyCode;
            pharmacy.Phone = dto.Phone;
            pharmacy.Province = dto.Province;
            pharmacy.Username = dto.Username;
            pharmacy.IsActive = dto.IsActive;
            if (dto.Password != null)
            {
                pharmacy.Password = PasswordHelper.EncodePasswordMd5(dto.Password);
            }
            await base.UpdateAsync(pharmacy, cancellationToken);
        }
    }
}
