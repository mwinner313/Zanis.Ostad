namespace Zanis.Ostad.Core.Infrastucture
{
    public class BaseEntity<T>:IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}