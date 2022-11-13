using Bll.Services.Abstractions;
using Core.Definitions;
using Core.Response;
using Dal.Abstractions;
using Dto.Models;
using Entity.Models;
using System;
using System.Threading.Tasks;

namespace Bll.Services.Concretes
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly IRepository<ContactInfo> _ContactInfoRepo;
        private readonly IRepository<Contact> _ContactRepo;
        private readonly IRepository<ContactType> _ContactTypeRepo;
        private readonly IUnitOfWork _UnitOfWork;
        public ContactInformationService(IUnitOfWork _unitOfWork)
        {
            _UnitOfWork = _unitOfWork;
            _ContactInfoRepo = _unitOfWork.GetRepository<ContactInfo>();
            _ContactRepo = _unitOfWork.GetRepository<Contact>();
            _ContactTypeRepo = _unitOfWork.GetRepository<ContactType>();
        }

        public async Task<GeneralResponse> CreateContactInfo(CreateContactInfoDto _model)
        {
            var _result = new GeneralResponse(ResultCode.Error, "Unexpected error occurred");

            try
            {
                var _selectedContact = await _ContactRepo.SingleAsync(x => x.Id == _model.ContactId);

                var _selectedContactType = await _ContactTypeRepo.SingleAsync(x => x.Id == _model.ContactTypeId);

                if (_selectedContact == null)
                {
                    _result.Update(ResultCode.Warning, "Contact not found");
                    return _result;
                }

                if (_selectedContactType == null)
                {
                    _result.Update(ResultCode.Warning, "Contact type not found");
                    return _result;
                }

                ContactInfo _contactInfo = new ContactInfo();
                _contactInfo.ContactId = _model.ContactId;
                _contactInfo.ContactInformation = _model.ContactInformation;
                _contactInfo.ContactTypeId = _model.ContactTypeId;

                await _ContactInfoRepo.AddAsync(_contactInfo);
                await _UnitOfWork.SaveChangesAsync();

                _result.Update(ResultCode.Success, "Contact information successfully created", _contactInfo);

            }
            catch (Exception _ex)
            {
                _result.Update(ResultCode.Error, _ex.Message.ToString());
            }
            return _result;
        }

        public async Task<GeneralResponse> DeleteContactInformation(Guid _info_id)
        {
            var _result = new GeneralResponse(ResultCode.Error, "Unexpected error occurred");
            try
            {
                var _deletedContactInformation = await _ContactInfoRepo.SingleAsync(x => x.Id == _info_id);

                if (_deletedContactInformation == null)
                {
                    _result.Update(ResultCode.Warning, "Contact information not found");
                    return _result;
                }

                _deletedContactInformation.IsDeleted = true;
                _ContactInfoRepo.UpdateAsync(_deletedContactInformation);

                await _UnitOfWork.SaveChangesAsync();

                _result.Update(ResultCode.Success, "Contact info deleted successfully");

            }
            catch (Exception _ex)
            {
                _result.Update(ResultCode.Error, _ex.Message.ToString());
            }

            return _result;
        }

        public async Task<GeneralResponse> DeleteContactInformationByContact(Guid _contact_id)
        {
            var _result = new GeneralResponse(ResultCode.Error, "Unexpected error occurred");
            try
            {
                var _selectedContact = await _ContactRepo.SingleAsync(x => x.Id == _contact_id);

                if (_selectedContact == null)
                {
                    _result.Update(ResultCode.Warning, "Contact not found");
                    return _result;
                }

                var _deletedContactInformations = await _ContactInfoRepo.GetAllInclude(x => x.ContactId == _selectedContact.Id);
                _deletedContactInformations.ForEach(f =>
                {
                    f.IsDeleted = true;
                });

                _ContactInfoRepo.UpdateAsync(_deletedContactInformations);

                await _UnitOfWork.SaveChangesAsync();

                _result.Update(ResultCode.Success, "Contact info deleted successfully");

            }
            catch (Exception _ex)
            {
                _result.Update(ResultCode.Error, _ex.Message.ToString());
            }

            return _result;
        }

    }
}
