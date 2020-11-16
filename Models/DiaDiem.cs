using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    [Table("DiaDiem")]
    public class DiaDiem
    {
        [Key]
        [Display(Name = "Mã địa điểm")]
        [Required(ErrorMessage = "Yêu cầu nhập mã địa điểm")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [Display(Name = "Tên địa điểm")]
        [Required(ErrorMessage = "Yêu cầu nhập tên địa điểm")]
        public string TenDiaDiem { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(500)]
        public string PictureId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Hoạt động")]
        [Required(ErrorMessage = "Yêu cầu nhập hoạt động")]
        public string HoatDongChinh { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Kinh nghiệm khi đi")]
        [Required(ErrorMessage = "Yêu cầu nhập kinh nghiệm đi")]
        public string KinhNghiem { get; set; }


        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Tiêu đề")]
        [Required(ErrorMessage = "Yêu cầu nhập kinh nghiệm")]
        public string TieuDe { get; set; }
        public virtual ICollection<BaiVietVeDiaDiem> BaiVietVeDiaDiem { get; set; }

        [Display(Name = "Tỉnh")]
        public int idTinh { get; set; }

        [ForeignKey("idTinh")]
        public virtual Tinh Tinh { get; set; }
    }
}