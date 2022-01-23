namespace LuxubuShop.Core.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Chọn ảnh")]
        public string Image { get; set; }

        [Display(Name = "Giá gốc")]
        public decimal? Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Danh mục")]
        public long? CategoryID { get; set; }

        [Display(Name = "Trang liên kết")]
        public long? BrandID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Từ khóa")]
        public string Keywords { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        public bool TopHot { get; set; }

        public bool Status { get; set; }

        public int ViewCount { get; set; }

        public int Rating { get; set; }

        [StringLength(250)]
        [Display(Name = "Link sản phẩm")]
        public string ProductLink { get; set; }
    }
}
