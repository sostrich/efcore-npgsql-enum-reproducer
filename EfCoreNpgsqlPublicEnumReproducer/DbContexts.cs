using EnumBugReproducer.Models.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace EnumBugReproducer.DAL
{


    public class MyContext : DbContext
    {
        private const string SchemaName = "myschema";

        public DbSet<DbEntity> Entities { get; set; }

        public DbSet<DbEntitySubclassOne> SubOneEntities { get; set; }
        public DbSet<DbEntitySubclassTwo> SubTwoEntities { get; set; }

        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        {
        }

        static MyContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.HasPostgresEnum<MyEnum>();
            var entityModelBuilder = modelBuilder.Entity<DbEntity>();
            entityModelBuilder.Metadata.SetSchema(SchemaName);
            entityModelBuilder.HasDiscriminator<MyEnum>("my_discriminator")
        .HasValue<DbEntitySubclassOne>(MyEnum.ValueOne)
        .HasValue<DbEntitySubclassTwo>(MyEnum.Value2);
            entityModelBuilder.HasKey(e => e.Id);
            entityModelBuilder.ToTable("entities");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
