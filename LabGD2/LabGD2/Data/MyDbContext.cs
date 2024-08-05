using LabGD2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabGD2.Data
{
	public class MyDbContext : DbContext
	{
		public MyDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<SanPham> SanPhams { get; set; }
		public DbSet<NhaSanXuat> NhaSanXuats { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<NhaSanXuat>().HasKey(x => x.Id);
			modelBuilder.Entity<SanPham>().HasKey(x => x.Id);
			modelBuilder.Entity<SanPham>().HasOne(x => x.NhaSanXuat).WithMany(x=>x.SanPham).HasForeignKey(x=>x.IdNhaSanXuat);
			modelBuilder.Entity<NhaSanXuat>().HasData(
			new NhaSanXuat { Id = "Id1" ,DiaChi = "Hà Nội",Ten= "FPT"},
			new NhaSanXuat { Id = "Id2" ,DiaChi = "Hà Nội",Ten= "Viettel"},
			new NhaSanXuat { Id = "Id3" ,DiaChi = "Hà Nội",Ten= "Mobiphone"}
				);

			base.OnModelCreating(modelBuilder);
		}
	}
}
