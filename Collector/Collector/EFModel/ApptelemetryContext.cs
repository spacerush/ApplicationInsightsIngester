using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Collector.EFModel
{
    public partial class ApptelemetryContext : DbContext
    {
        public ApptelemetryContext()
        {
        }

        public ApptelemetryContext(DbContextOptions<ApptelemetryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TelemetryLog> TelemetryLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}