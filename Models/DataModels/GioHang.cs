using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigmemt_Thanh_C4.Models.DataModels
{
    public class GioHang
    {
        [Key]
        public int GioHangID { get; set; }

        [Required]
        public int NguoiDungID { get; set; }

        public DateTime NgayCapNhat { get; set; } 

        [ForeignKey("NguoiDungID")]
        public virtual NguoiDung? NguoiDung { get; set; }

        public virtual ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
    }
}
