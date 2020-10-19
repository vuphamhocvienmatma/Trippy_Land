using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    public class ChuDeBaiVietVeDiaDiem
    {
        [Key]
        [Display(Name = "Mã chủ đề bài viết")]
        [Required(ErrorMessage = "Yêu cầu nhập mã chủ đề bài viết")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [Display(Name = "Tên chủ đề")]
        [Required(ErrorMessage = "Yêu cầu nhập tên chủ đề")]
        public string TenChuDe { get; set; }

        public virtual ICollection<BaiVietVeDiaDiem> BaiVietVeDiaDiems { get; set; }
    }
}