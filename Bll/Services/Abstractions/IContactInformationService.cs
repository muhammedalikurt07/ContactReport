using Core.Response;
using Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services.Abstractions
{
    public interface IContactInformationService
    {
        Task<GeneralResponse> CreateContactInfo(CreateContactInfoDto _model);
        Task<GeneralResponse> DeleteContactInformation(Guid _info_id);
        Task<GeneralResponse> DeleteContactInformationByContact(Guid _contact_id);
    }
}
