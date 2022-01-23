namespace LuxubuShop.Core.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Chọn ảnh")]
        public string Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [StringLength(50)]
        [Display(Name = "Liên kết")]
        public string Link { get; set; }

        public bool Status { get; set; }
    }
}
