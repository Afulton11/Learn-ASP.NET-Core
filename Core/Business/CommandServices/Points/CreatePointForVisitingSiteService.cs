using DatabaseFactory.Data.Contracts;

namespace Core.Business.CommandServices.Points
{
    public class CreatePointForVisitingSiteService : CreatePointService
    {
        public CreatePointForVisitingSiteService(IDatabase database) : base(database)
        {
        }

        protected override string ProcedureName => "Blog.CreatePointForVisitingSite";
    }
}
