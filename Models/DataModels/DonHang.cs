using AssigmentC4_TrinhHuuThanh.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class DonHang
    {
        [Key]
        public int DonHangID { get; set; }

        [Required]
        public int NguoiDungID { get; set; }

        [Display(Name = "Ngày đặt")]
        public DateTime NgayDat { get; set; }

        [StringLength(50)]
        [Display(Name = "Trạng thái")]
        public string TrangThai { get; set; } = "Chờ xử lý";

        [StringLength(50)]
        [Display(Name = "Phương thức thanh toán")]
        public string? PhuongThucThanhToan { get; set; }

        [ForeignKey("NguoiDungID")]
        public virtual NguoiDung? NguoiDung { get; set; }
        public string? DiaChiGiaoHang { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDienThoai { get; set; }
        public string? GhiChu { get; set; }
        public bool DaThanhToan { get; set; } = false;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; }

        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
        public virtual ICollection<TheoDoiDonHang>? TheoDoiDonHangs { get; set; }
    }
}
