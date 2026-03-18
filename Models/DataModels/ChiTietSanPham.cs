using Assigmemt_Thanh_C4.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigmentC4_TrinhHuuThanh.Models.DataModels
{
    public class ChiTietSanPham
    {
        [Key]
        public int ChiTietSanPhamID { get; set; }

        [Required]
        public int SanPhamID { get; set; }

        [ForeignKey("SanPhamID")]
        public virtual SanPham? SanPham { get; set; }

        [StringLength(100)]
        [Display(Name = "Độ phân giải")]
        public string? DoPhanGiai { get; set; }

        [StringLength(100)]
        [Display(Name = "Cảm biến")]
        public string? CamBien { get; set; }

        [StringLength(100)]
        [Display(Name = "ISO")]
        public string? ISO { get; set; }

        [StringLength(100)]
        [Display(Name = "Ống kính")]
        public string? OngKinh { get; set; }

        [StringLength(100)]
        [Display(Name = "Khẩu độ")]
        public string? KhauDo { get; set; }

        [StringLength(100)]
        [Display(Name = "Chống rung")]
        public string? ChongRung { get; set; }

        [StringLength(100)]
        [Display(Name = "Quay video")]
        public string? Video { get; set; }

        [StringLength(100)]
        [Display(Name = "Màn hình")]
        public string? ManHinh { get; set; }

        [StringLength(100)]
        [Display(Name = "Pin")]
        public string? Pin { get; set; }

        [StringLength(100)]
        [Display(Name = "Kết nối")]
        public string? KetNoi { get; set; }

        [StringLength(100)]
        [Display(Name = "Trọng lượng")]
        public string? TrongLuong { get; set; }

        [StringLength(100)]
        [Display(Name = "Kích thước")]
        public string? KichThuoc { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        [StringLength(100)]
        [Display(Name = "Tiêu cự")]
        public string? TieuCu { get; set; }

        [StringLength(100)]
        [Display(Name = "Ngàm lens")]
        public string? NgamLens { get; set; }

        [StringLength(100)]
        [Display(Name = "Khoảng cách lấy nét")]
        public string? KhoangCachLayNet { get; set; }

        [StringLength(100)]
        [Display(Name = "Số lá khẩu")]
        public string? SoLaKhau { get; set; }

        [StringLength(100)]
        [Display(Name = "Công suất")]
        public string? CongSuat { get; set; }

        [StringLength(100)]
        [Display(Name = "Nhiệt màu")]
        public string? NhietMau { get; set; }

        [StringLength(100)]
        [Display(Name = "CRI")]
        public string? CRI { get; set; }

        [StringLength(100)]
        [Display(Name = "Tải trọng tối đa")]
        public string? TaiTrongToiDa { get; set; }

        [StringLength(100)]
        [Display(Name = "Chiều cao tối đa")]
        public string? ChieuCaoToiDa { get; set; }

        [StringLength(100)]
        [Display(Name = "Chất liệu")]
        public string? ChatLieu { get; set; }

        public virtual ICollection<MauSacSanPham>? MauSacSanPhams { get; set; }
    }
}
