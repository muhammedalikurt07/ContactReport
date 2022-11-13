using Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class ContactInfo : EntityBase
    {
        public string ContactInformation { get; set; }
        [ForeignKey("ContactType")]
        public Guid ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
