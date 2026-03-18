using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class ChiTietGioHang
    {
        [Key]
        public int ChiTietGioHangID { get; set; }

        [Required]
        public int GioHangID { get; set; }

        [Required]
        public int SanPhamID { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [ForeignKey("GioHangID")]
        public virtual GioHang? GioHang { get; set; }

        [ForeignKey("SanPhamID")]
        public virtual SanPham? SanPham { get; set; }
    }
}
