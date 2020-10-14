using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    [Table("KhachSan")]
    public class KhachSan
    {
        [Key]
        [Display(Name = "Mã khách sạn")]
        [Required(ErrorMessage = "Yêu cầu nhập mã khách sạn")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [Display(Name = "Tên địa điểm")]
        [Required(ErrorMessage = "Yêu cầu nhập tên khách sạn")]
        public string TenKhachSan { get; set; }

        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Yêu cầu nhập ảnh")]
        [StringLength(500)]
        public string PictureId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Chi tiết")]
        public string ChiTiet { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Lịch sử khách sạn")]
        public string LichSuKhachSan { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Địa điểm chi tiết")]
        public string DiaDiemChiTiet { get; set; }

        [Display(Name = "Tỉnh")]
        public int idTinh { get; set; }

        [ForeignKey("idTinh")]
        public virtual Tinh Tinh { get; set; }
    }
}