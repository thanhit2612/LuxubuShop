﻿namespace LuxubuShop.Core.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Chọn ảnh")]
        public string Image { get; set; }

        public long DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name = "Liên kết")]
        public string Link { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public bool Status { get; set; }
    }
}
