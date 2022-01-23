using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LuxubuShop.Core.EF
{
	public partial class LuxubuShopDbContext : DbContext
	{
		public LuxubuShopDbContext()
			: base("name=LuxubuShopDbContext")
		{
		}

		public virtual DbSet<About> Abouts { get; set; }
		public virtual DbSet<Banner> Banners { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Content> Contents { get; set; }
		public virtual DbSet<ContentTag> ContentTags { get; set; }
		public virtual DbSet<Menu> Menus { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductCategory> ProductCategories { get; set; }
		public virtual DbSet<Slide> Slides { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<UserGroup> UserGroups { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<About>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<About>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<About>()
				.Property(e => e.MetaDescriptions)
				.IsFixedLength();

			modelBuilder.Entity<Banner>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Brand>()
				.Property(e => e.Name)
				.IsFixedLength();

			modelBuilder.Entity<Brand>()
				.Property(e => e.Image)
				.IsFixedLength();

			modelBuilder.Entity<Brand>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Brand>()
				.Property(e => e.Link)
				.IsFixedLength();

			modelBuilder.Entity<Content>()
				.Property(e => e.ProductLink)
				.IsFixedLength();

			modelBuilder.Entity<Content>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Content>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<Content>()
				.Property(e => e.MetaDescription)
				.IsFixedLength();

			modelBuilder.Entity<ContentTag>()
				.Property(e => e.TagID)
				.IsUnicode(false);

			modelBuilder.Entity<Product>()
				.Property(e => e.Price)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Product>()
				.Property(e => e.PromotionPrice)
				.HasPrecision(18, 0);

			modelBuilder.Entity<Product>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Product>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<Product>()
				.Property(e => e.MetaDescription)
				.IsFixedLength();

			modelBuilder.Entity<Product>()
				.Property(e => e.ProductLink)
				.IsFixedLength();

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<ProductCategory>()
				.Property(e => e.MetaTitle)
				.IsUnicode(false);

			modelBuilder.Entity<Slide>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<Tag>()
				.Property(e => e.ID)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.UserName)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.GroupID)
				.IsUnicode(false);

			modelBuilder.Entity<User>()
				.Property(e => e.CreatedBy)
				.IsUnicode(false);

			modelBuilder.Entity<UserGroup>()
				.Property(e => e.ID)
				.IsUnicode(false);
		}
	}
}
