namespace HttpWebRequestHostHeader
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CheckResponse : DbContext
    {
        public CheckResponse()
            : base("name=ICheckResponse")
        {
        }

        public virtual DbSet<IpCheck> IpCheck { get; set; }
        public virtual DbSet<CheckType> CheckType { get; set; }
        public virtual DbSet<Params> Params { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IpCheck>()
                .Property(e => e.IpAddr)
                .IsUnicode(false);

            modelBuilder.Entity<IpCheck>()
                .Property(e => e.CheckType)
                .IsUnicode(false);

            modelBuilder.Entity<IpCheck>()
                .Property(e => e.ResCode)
                .IsUnicode(false);
            modelBuilder.Entity<IpCheck>()
                .Property(e => e.ErrorMessage)
                .IsUnicode(false);

            modelBuilder.Entity<CheckType>()
                .Property(e => e.CheckType1)
                .IsUnicode(false);

            modelBuilder.Entity<CheckType>()
                .Property(e => e.CheckName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckType>()
                .Property(e => e.CheckDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Params>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Params>()
                .Property(e => e.Value)
                .IsUnicode(false);
        }
    }
}
