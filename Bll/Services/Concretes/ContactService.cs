using Bll.Services.Abstractions;
using Core.Definitions;
using Core.Response;
using Dal.Abstractions;
using Dto.Models;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bll.Services.Concretes
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _ContactRepo;
        private readonly IRepository<ContactInfo> _ContactInfoRepo;
        private readonly IUnitOfWork _UnitOfWork;
        public ContactService(IUnitOfWork _unitOfWork)
        {
            _UnitOfWork = _unitOfWork;
            _ContactRepo = _unitOfWork.GetRepository<Contact>();
            _ContactInfoRepo = _unitOfWork.GetRepository<ContactInfo>();
        }

        public async Task<List<ContactDto>> GetAllContacts()
        {
            try
            {
                return _ContactRepo.GetAllInclude(x => !x.IsDeleted, x => x.Include(i => i.ContactInfos)).Result.Select(s => new ContactDto
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                }).ToList();

            }
            catch (Exception _ex)
            {
                throw;
            }

        }

        public async Task<GeneralResponse> GetContact(Guid _id)
        {
            var _result = new GeneralResponse(ResultCode.Error, "Unexpected error occurred");
            try
            {
                var _selectedContact = await _ContactRepo.SingleAsync(x => x.Id == _id && !x.IsDeleted, i => i.Include(ii => ii.ContactInfos).ThenInclude(ii => ii.ContactType));

                if (_selectedContact == null)
                {
                    _result.Update(ResultCode.Warning, "Contact is not found");
                    return _result;
                }

                _selectedContact.ContactInfos = _selectedContact.ContactInfos.Where(x => x.IsDeleted == false).ToList();

                _result.Update(ResultCode.Success, "Contact information successfully retrieved", _selectedContact);

                return _result;

            }
            catch (Exception _ex)
            {
                throw;
            }
        }

        public async Task<GeneralResponse> CreateContact(List<CreateContractDto> _model)
        {
            var _result = new GeneralResponse(ResultCode.Error, "Unexpected error occurred");
            List<ContactInfo> _contactInfoList = new List<ContactInfo>();
            try
            {
                foreach (var _contact in _model)
                {
                    Contact _contacts = new Contact();
                    _contacts.CompanyName = _contact.CompanyName;
                    _contacts.FirstName = _contact.FirstName;
                    _contacts.LastName = _contact.LastName;

                    var _addedContact = await _ContactRepo.AddAsync(_contacts);
                    await _UnitOfWork.SaveChangesAsync();

                    foreach (var _contactInfos in _contact.ContactInfoDto)
                    {
                        ContactInfo _contactInfo = new ContactInfo();
                        _contactInfo.ContactInformation = _contactInfos.ContactInformation;
                        _contactInfo.ContactTypeId = _contactInfos.ContactTypeId;
                        _contactInfo.ContactId = _addedContact.Entity.Id;
                        _contactInfoList.Add(_contactInfo);
                    }
                }

                await _ContactInfoRepo.AddAsync(_contactInfoList);

                await _UnitOfWork.SaveChangesAsync();

                _result.Update(ResultCode.Success, "Contact successfully created");

                return _result;

            }
            catch (Exception _ex)
            {
                return _result;
            }
        }

        public async Task<GeneralResponse> DeleteContact(Guid _id)
        {
            var _result = new GeneralResponse(ResultCode.Error, "Unexpected error occurred");
            try
            {
                var _deletedContact = await _ContactRepo.SingleAsync(x => x.Id == _id);

                if (_deletedContact == null)
                {
                    _result.Update(ResultCode.Warning, "Contact is not found");
                    return _result;
                }

                var _deletedContactInfos = await _ContactInfoRepo.GetAllInclude(x => x.ContactId == _id);

                if (_deletedContactInfos.Count > 0)
                {
                    _deletedContactInfos.ForEach(f =>
                    {
                        f.IsDeleted = true;
                        f.ModifiedAt = DateTime.UtcNow;
                    });
                    _ContactInfoRepo.UpdateAsync(_deletedContactInfos);
                }

                _deletedContact.IsDeleted = true;
                _deletedContact.ModifiedAt = DateTime.UtcNow;

                _ContactRepo.UpdateAsync(_deletedContact);

                await _UnitOfWork.SaveChangesAsync();

                _result.Update(ResultCode.Success, "Contacts deletion completed successfully");

                return _result;
            }
            catch (Exception _ex)
            {
                return _result;
            }
        }



    }
}
