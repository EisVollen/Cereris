using System.Data.Entity;


namespace Cereris.ObjectsModel
{
    public class CommentsContext : DbContext
    {
        public CommentsContext() : base("DBConnection")
        {}

        public DbSet<Comments> Comments { get; set; }
    }
}