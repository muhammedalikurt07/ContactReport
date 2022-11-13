using Bll.Services.Abstractions;
using Core.Response;
using Dto.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactInformationController : Controller
    {
        private readonly IContactInformationService _ContactInfoService;
        public ContactInformationController(IContactInformationService _contactInfoService)
        {
            _ContactInfoService = _contactInfoService;
        }

        #region Create Contact Information By Contact
        [HttpPost("[action]")]
        public async Task<ActionResult<GeneralResponse>> AddContactInformation([FromBody] CreateContactInfoDto _model)
        {
            var _result = await _ContactInfoService.CreateContactInfo(_model);

            return _result;
        }
        #endregion
        #region Remove Contact Information
        [HttpDelete("[action]")]
        public async Task<ActionResult<GeneralResponse>> DeleteContactInformation(Guid _info_id)
        {
            var _result = await _ContactInfoService.DeleteContactInformation(_info_id);

            return _result;
        }
        #endregion

        #region Remove Contact Information By Contact
        [HttpDelete("[action]")]
        public async Task<ActionResult<GeneralResponse>> DeleteContactInformationByContact(Guid _contact_id)
        {
            var _result = await _ContactInfoService.DeleteContactInformationByContact(_contact_id);

            return _result;
        }
        #endregion

    }
}
