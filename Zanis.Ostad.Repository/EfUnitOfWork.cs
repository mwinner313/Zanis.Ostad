using System.Transactions;
using Zanis.Ostad.Core.Contracts;

namespace Zanis.Ostad.Repository
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private TransactionScope _transactionScope;

        public void Begin()
        {
            _transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted},
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Commit()
        {
            _transactionScope.Complete();
            _transactionScope.Dispose();
        }

        public void RollBack()
        {
            _transactionScope?.Dispose();
        }
    }
}