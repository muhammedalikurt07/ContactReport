using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Models
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }
        public string ContactInformation { get; set; }
        public string ContactType { get; set; }
    }

    public class CreateContactInfoDto
    {
        public string ContactInformation { get; set; }
        public Guid ContactTypeId { get; set; }
        public Guid ContactId { get; set; }
    }
}
