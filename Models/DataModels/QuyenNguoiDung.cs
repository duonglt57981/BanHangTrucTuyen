using System.ComponentModel.DataAnnotations;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class QuyenNguoiDung
    {
        [Key]
        public int QuyenID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên quyền")]
        public string TenQuyen { get; set; }
    }
}
