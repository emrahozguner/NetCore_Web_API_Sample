namespace API.Domain.Repositories
{
    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}