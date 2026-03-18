using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class ChiTietDonHang
    {
        [Key]
        public int ChiTietDonHangID { get; set; }

        [Required]
        public int DonHangID { get; set; }

        [Required]
        public int SanPhamID { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá")]
        public decimal Gia { get; set; }

        [ForeignKey("DonHangID")]
        public virtual DonHang? DonHang { get; set; }

        [ForeignKey("SanPhamID")]
        public virtual SanPham? SanPham { get; set; }
    }
}
