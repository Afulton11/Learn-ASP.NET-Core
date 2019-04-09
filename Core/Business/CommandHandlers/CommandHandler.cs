using DatabaseFactory.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandHandlers
{
    public abstract class CommandHandler<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        protected CommandHandler(ILogger logger)
        {
            Logger = logger;
        }

        protected ILogger Logger { get; }
        public abstract void Handle(TCommand command);
    }
}
