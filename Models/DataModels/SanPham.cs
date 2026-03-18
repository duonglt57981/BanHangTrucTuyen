using AssigmentC4_TrinhHuuThanh.Models.DataModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class SanPham
    {
        [Key]
        public int SanPhamID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(100)]
        [Display(Name = "Tên sản phẩm")]
        public string TenSanPham { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string? MoTa { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:N0} đ")]
        public decimal Gia { get; set; }

        [Required]
        [Display(Name = "Tồn kho")]
        public int TonKho { get; set; }

        [Display(Name = "Ngày thêm")]
        public DateTime NgayThem { get; set; } 

        [Display(Name = "Danh mục")]
        public int? DanhMucID { get; set; }

        [Display(Name = "Thương hiệu")]
        public int? ThuongHieuID { get; set; }

        [Display(Name = "Hình ảnh")]
        public byte[]? HinhAnh { get; set; }

        [ForeignKey("DanhMucID")]
        public virtual DanhMuc? DanhMuc { get; set; }

        [ForeignKey("ThuongHieuID")]
        public virtual ThuongHieu? ThuongHieu { get; set; }

        public virtual ChiTietSanPham? ChiTietSanPham { get; set; }
        public virtual ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}
