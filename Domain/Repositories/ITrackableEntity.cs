using System;

namespace API.Domain.Repositories
{
    public interface ITrackableEntity
    {
        DateTime CreatedAt { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifiedAt { get; set; }

        string ModifiedBy { get; set; }
    }
}