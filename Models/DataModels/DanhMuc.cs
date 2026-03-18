using System.ComponentModel.DataAnnotations;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class DanhMuc
    {
        [Key]
        public int DanhMucID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        public string TenDanhMuc { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
