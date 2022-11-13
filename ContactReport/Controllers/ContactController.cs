using Bll.Services.Abstractions;
using Core.Response;
using Dto.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _ContactService;
        public ContactController(IContactService _contactService)
        {
            _ContactService = _contactService;
        }

        #region Get All Contacts
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ContactDto>>> GetAllContacts()
        {
            var _result = await _ContactService.GetAllContacts();

            return _result;
        }
        #endregion

        #region Create Contact
        [HttpPost("[action]")]
        public async Task<ActionResult<GeneralResponse>> AddContact([FromBody] List<CreateContractDto> _model)
        {
            var _result = await _ContactService.CreateContact(_model);

            return _result;
        }
        #endregion

        #region Delete Contact
        [HttpDelete("[action]")]
        public async Task<ActionResult<GeneralResponse>> DeleteContact(Guid _id)
        {
            var _result = await _ContactService.DeleteContact(_id);

            return _result;
        }
        #endregion

        #region Get Contact
        [HttpGet("[action]")]
        public async Task<ActionResult<GeneralResponse>> GetContact(Guid _id)
        {
            var _result = await _ContactService.GetContact(_id);

            return _result;
        }
        #endregion

    }
}
