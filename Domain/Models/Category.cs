using System;
using System.Collections.Generic;
using API.Domain.Repositories;

namespace API.Domain.Models
{
    public class Category : BaseEntity<int>, ISoftDeletableEntity, ITrackableEntity
    {
        public string Name { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}