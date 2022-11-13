using Core.Response;
using Dto.Models;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Services.Abstractions
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllContacts();
        Task<GeneralResponse> CreateContact(List<CreateContractDto> _model);
        Task<GeneralResponse> DeleteContact(Guid _id);
        Task<GeneralResponse> GetContact(Guid _id);
    }
}
