using Microsoft.EntityFrameworkCore;

namespace DistantWorkCalendarService.Classes
{
    public class Context : DbContext
    {
        public DbContextOptions<Context> DbContextOptions;

        public Context(DbContextOptions<Context> options) : base(options)
        {
            DbContextOptions = options;
        }

        private readonly string SCHEMA = "calendar";

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("events", SCHEMA);
            modelBuilder.Entity<Event>().HasKey(x => x.Id);
            modelBuilder.Entity<Event>().Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Event>().Property(x => x.Title).HasColumnName("title").IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.Type).HasColumnName("type").IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.StartDate).HasColumnName("start_date").IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.EndDate).HasColumnName("end_date").IsRequired();


            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            int data = await base.SaveChangesAsync(cancellation);
            return data;
        }

        public override int SaveChanges()
        {
            int data = base.SaveChanges();
            return data;
        }
    }
}
