using Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class ContactInfoContact : EntityBase
    {
        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        [ForeignKey("ContactInfo")]
        public Guid? ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
