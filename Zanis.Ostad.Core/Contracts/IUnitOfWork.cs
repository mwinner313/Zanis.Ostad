namespace Zanis.Ostad.Core.Contracts
{
    public interface IUnitOfWork
    {
        void Begin();
        void Commit();
        void RollBack();
    }
}