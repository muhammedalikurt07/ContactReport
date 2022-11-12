using System;

namespace Entity.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
