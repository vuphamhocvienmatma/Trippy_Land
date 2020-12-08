using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        public string TenBaiViet { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Tóm tắt bài viết")]
        public string TomTatBaiViet { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        [Column(TypeName = "NVARCHAR(MAX)")]
        [MaxLength]
        [Display(Name = "Nội dung bài viết")]
        public string NoiDungBaiViet { get; set; }

        [Display(Name = "Địa điểm")]
        public int idDiaDiem { get; set; }

        [Display(Name = "Ảnh bài viết")]
        [StringLength(500)]
        public string PictureId { get; set; }

        [Display(Name = "Đã duyệt")]
        public bool DaDuyet { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Thời gian viết bài")]
        public DateTime DataCreated { get; set; }

        [ForeignKey("idDiaDiem")]
        public virtual DiaDiem DiaDiem { get; set; }

        [Display(Name = "Chủ đề")]
        public int IdChude { get; set; }

        [ForeignKey("IdChude")]
        public virtual ChuDeBaiVietVeDiaDiem ChuDeBaiVietVeDiaDiem { get; set; }


    }
}