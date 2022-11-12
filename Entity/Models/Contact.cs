using Entity.Base;

namespace Entity.Models
{
    public class Contact : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
