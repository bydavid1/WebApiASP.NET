namespace Api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Client : DbContext
    {
        public Client()
            : base("name=Client")
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Apellido)
                .IsUnicode(false);
        }
    }
}
