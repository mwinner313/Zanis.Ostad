namespace Zanis.Ostad.Core.Infrastucture
{
    public interface IBaseEntity<T>
    {
         T Id { get; set; }
    }
}