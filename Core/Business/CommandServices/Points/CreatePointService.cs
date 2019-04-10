using Core.Data.Commands;
using DatabaseFactory.Data;
using DatabaseFactory.Data.Contracts;
using EnsureThat;

namespace Core.Business.CommandServices.Points
{
    public abstract class CreatePointService : ICommandService<CreatePointCommand>
    {
        protected CreatePointService(IDatabase database)
        {
            EnsureArg.IsNotNull(database, nameof(database));

            Database = database;
        }

        protected abstract string ProcedureName { get; }
        protected IDatabase Database { get; }

        public void Execute(CreatePointCommand command)
        {
            Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand(ProcedureName, transaction);
                var userIdParameter = Database.CreateParameter("UserId", command.UserId);

                procedure.Parameters.Add(userIdParameter);

                Database.Execute(procedure);
            });
        }

    }
}
