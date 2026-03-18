using Assigmemt_Thanh_C4.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigmentC4_TrinhHuuThanh.Models.DataModels
{
    public class MauSacSanPham
    {
        [Key]
        public int MauSacID { get; set; }

        [Required]
        public int ChiTietSanPhamID { get; set; }   // ← FK vào ChiTietSanPham

        [ForeignKey("ChiTietSanPhamID")]
        public virtual ChiTietSanPham? ChiTietSanPham { get; set; }

        [Required(ErrorMessage = "Tên màu là bắt buộc")]
        [StringLength(50)]
        [Display(Name = "Màu sắc")]
        public string TenMau { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        public int SoLuong { get; set; }

        [Display(Name = "Hình ảnh")]
        public byte[]? HinhAnh { get; set; }
    }
}
