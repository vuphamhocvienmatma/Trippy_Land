using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    [Table("MonAn")]
    public class MonAn
    {
        [Key]
        [Display(Name = "Mã món ăn")]
        [Required(ErrorMessage = "Yêu cầu nhập mã món ăn")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [Display(Name = "Tên món ăn")]
        [Required(ErrorMessage = "Yêu cầu nhập tên món ăn")]
        public string TenMonAn { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(500)]
        public string PictureId { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Chi tiết")]
        public string ChiTiet { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Lịch sử món ăn")]
        public string LichSuaMonAn { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Nguyên liệu")]
        public string NguyenLieu { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Cách làm")]
        public string CachLam { get; set; }

        [Display(Name = "Tỉnh, thành")]
        public int idTinh { get; set; }

        [ForeignKey("idTinh")]
        public virtual Tinh Tinh { get; set; }
    }
}