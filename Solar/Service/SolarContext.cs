using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Solar.Service.Models;
using Vidyano.Service.RavenDB;

namespace Solar.Service
{
    public partial class SolarContext : TargetRavenDBContext
    {
        public SolarContext(IDocumentSession session)
            : base(session)
        {
        }

        public IRavenQueryable<FusionSolarCredential> FusionSolarCredentials => Query<FusionSolarCredential>();
    }
}