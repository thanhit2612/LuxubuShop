namespace LuxubuShop.Core.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "T�n menu")]
        public string Text { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public bool Status { get; set; }
    }
}
