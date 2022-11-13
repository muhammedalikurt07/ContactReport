using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models
{
    public class CreateContractDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public List<CreateContractInfoDto> ContactInfoDto { get; set; }
    }

    public class CreateContractInfoDto
    {
        public string ContactInformation { get; set; }
        public Guid ContactTypeId { get; set; }
    }
}
