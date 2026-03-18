using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class NguoiDung
    {
        [Key]
        public int NguoiDungID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [StringLength(100)]
        [Display(Name = "Họ tên")]
        public string? HoTen { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime NgayTao { get; set; }

        public int? QuyenID { get; set; }

        [ForeignKey("QuyenID")]
        public virtual QuyenNguoiDung? Quyen { get; set; }
    }
}
