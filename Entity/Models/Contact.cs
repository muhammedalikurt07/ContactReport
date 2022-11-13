using Entity.Base;
using System.Collections.Generic;

namespace Entity.Models
{
    public class Contact : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
