using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    [Table("Tinh")]
    public class Tinh
    {
        [Key]
        [Display(Name = "Mã tỉnh")]
        [Required(ErrorMessage = "Yêu cầu nhập mã tỉnh")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [Display(Name = "Tên tỉnh")]
        [Required(ErrorMessage = "Yêu cầu nhập tên tỉnh")]
        public string TenTinh { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(500)]
        public string PictureId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Chi tiết")]
        public string ChiTiet { get; set; }

        public virtual ICollection<DiaDiem> DiaDiem { get; set; }
        public virtual ICollection<KhachSan> KhachSan { get; set; }
        public virtual ICollection<MonAn> MonAn { get; set; }
        public virtual ICollection<DanhGia> DanhGia { get; set; }
    }
}