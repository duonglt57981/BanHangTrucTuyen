using Assigmemt_Thanh_C4.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssigmentC4_TrinhHuuThanh.Models.DataModels
{
    public class TheoDoiDonHang
    {
        [Key]
        public int TheoDoiID { get; set; }

        [Required]
        public int DonHangID { get; set; }

        [Required]
        [Display(Name = "Thời gian")]
        public DateTime ThoiGian { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Chi tiết hoạt động")]
        public string MoTa { get; set; } // Ví dụ: "Đơn hàng đã đến kho BN Hub"

        [StringLength(100)]
        [Display(Name = "Vị trí")]
        public string? ViTri { get; set; } // Ví dụ: "Bắc Ninh"

        [ForeignKey("DonHangID")]
        public virtual DonHang? DonHang { get; set; }
    }
}
