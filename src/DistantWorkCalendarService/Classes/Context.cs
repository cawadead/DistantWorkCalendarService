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
            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventStatuses)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.Id);

            modelBuilder.Entity<Event>().Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Event>().Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.ModifiedDate).HasColumnName("modified_date").IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired();

            modelBuilder.Entity<EventStatus>().ToTable("statuses", SCHEMA);
            modelBuilder.Entity<EventStatus>().HasKey(x => x.Id);
            modelBuilder.Entity<EventStatus>().Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<EventStatus>().Property(x => x.Title).HasColumnName("title").IsRequired();
            modelBuilder.Entity<EventStatus>().Property(x => x.EventType).HasColumnName("event_type").IsRequired();
            modelBuilder.Entity<EventStatus>().Property(x => x.StartDate).HasColumnName("start_date").IsRequired();
            modelBuilder.Entity<EventStatus>().Property(x => x.EndDate).HasColumnName("end_date").IsRequired();
            modelBuilder.Entity<EventStatus>().Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired();

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
