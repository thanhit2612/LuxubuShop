namespace LuxubuShop.Core.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
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

        public long? BrandID { get; set; }

        [Display(Name = "Lựa chọn sản phẩm")]
        public long ProductID { get; set; }

        [StringLength(250)]
        public string ProductLink { get; set; }

        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }

        [StringLength(250)]
        [Display(Name = "Từ khóa")]
        public string Keywords { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        public bool TopHot { get; set; }

        public bool Status { get; set; }

        public int ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }
    }
}
