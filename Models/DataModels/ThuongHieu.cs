using Assigmemt_Thanh_C4.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigmentC4_TrinhHuuThanh.Models.DataModels
{
    public class ThuongHieu
    {
        [Key]
        public int ThuongHieuID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên thương hiệu")]
        public string TenThuongHieu { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        [StringLength(100)]
        [Display(Name = "Quốc gia")]
        public string? QuocGia { get; set; }

        [Display(Name = "Logo")]
        public byte[]? Logo { get; set; }

        public virtual ICollection<SanPham>? SanPhams { get; set; }
    }
}
