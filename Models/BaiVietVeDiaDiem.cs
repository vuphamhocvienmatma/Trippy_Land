using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    public class BaiVietVeDiaDiem
    {
        [Key]
        [Display(Name = "Mã bài viết")]
        [Required(ErrorMessage = "Yêu cầu nhập mã bài viết")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [Display(Name = "Tên bài viết")]
        [Required(ErrorMessage = "Yêu cầu nhập tên bài viết")]
        public string TenDiaDiem { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Tóm tắt bài viết")]
        public string TomTatBaiViet { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Nội dung bài viết")]
        public string NoiDungBaiViet { get; set; }

        [Display(Name = "Địa điểm")]
        public int idDiaDiem { get; set; }

        [Display(Name = "Ảnh bài viết")]
        [StringLength(500)]
        public string PictureId { get; set; }

        [ForeignKey("idDiaDiem")]
        public virtual DiaDiem DiaDiem { get; set; }
    }
}