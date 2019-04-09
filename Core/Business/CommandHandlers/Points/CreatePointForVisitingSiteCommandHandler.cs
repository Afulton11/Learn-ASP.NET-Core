using DatabaseFactory.Data;
using Microsoft.Extensions.Logging;

namespace Core.Business.CommandHandlers.Points
{
    public class CreatePointForVisitingSiteCommandHandler : CreatePointCommandHandler
    {
        public CreatePointForVisitingSiteCommandHandler(
            ILogger<CreatePointForVisitingSiteCommandHandler> logger,
            Database database) : base(logger, database)
        {
        }

        protected override string ProcedureName => "Blog.CreatePointForVisitingSite";
    }
}
