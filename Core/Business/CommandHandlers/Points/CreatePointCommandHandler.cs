using Core.Data.Commands;
using DatabaseFactory.Data;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandHandlers.Points
{
    public abstract class CreatePointCommandHandler : CommandHandler<CreatePointCommand>
    {
        protected CreatePointCommandHandler(ILogger logger, Database database) : base(logger)
        {
            Database = database;
        }

        protected abstract string ProcedureName { get; }
        protected Database Database { get; }

        public override void Handle(CreatePointCommand command)
        {
            Database.TryExecuteTransaction((transaction) =>
            {
                var procedure = Database.CreateStoredProcCommand(ProcedureName, transaction);
                var userIdParameter = Database.CreateParameter("userId", command.UserId);

                procedure.Parameters.Add(userIdParameter);

                Database.Execute(procedure);
            });
            Logger.LogInformation($"{this.GetType().Name} has been executed successfully.");
        }

    }
}
