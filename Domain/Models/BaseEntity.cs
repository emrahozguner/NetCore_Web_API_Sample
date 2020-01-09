namespace API.Domain.Models
{
    public abstract class BaseEntity<T>
    {
        public T Id { set; get; }
    }
}