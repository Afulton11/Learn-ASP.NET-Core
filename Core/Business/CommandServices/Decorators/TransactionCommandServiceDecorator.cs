using DatabaseFactory.Data.Contracts;
using System.Transactions;

namespace Core.Business.CommandServices.Decorators
{
    /// <summary>
    /// This decorator allows all commands that it wraps to perform on the same dbTransaction.
    /// See <a href="!:https://docs.microsoft.com/en-us/dotnet/api/system.transactions.transactionscope?view=netcore-2.2">TransactionScope</a>
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class TransactionCommandServiceDecorator<TCommand> : ICommandService<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandService<TCommand> decoratee;

        public TransactionCommandServiceDecorator(ICommandService<TCommand> decoratee)
        {
            this.decoratee = decoratee;
        }

        public void Execute(TCommand command)
        {
            // TransactionScope allows and DbTransaction created during the lifetime of this
            // scope is automatically enlisted into the same transaction.
            using (var transactionScope = new TransactionScope())
            {
                this.decoratee.Execute(command);

                transactionScope.Complete();
            }
        }
    }
}
