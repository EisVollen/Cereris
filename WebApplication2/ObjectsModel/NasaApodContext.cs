using System.Data.Entity;

namespace Cereris.ObjectsModel
{
    public class NasaApodContext
    {
        public class NasaAPODContext : DbContext
        {
            public NasaAPODContext()
                : base("DBConnection")
            {}

            public DbSet<NasaAPOD> NasaAPODs { get; set; }

        }
    }
}