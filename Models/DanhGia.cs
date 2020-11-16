using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    [Table("DanhGia")]
    public class DanhGia
    {
        [Key]
        [Display(Name = "Mã đánh giá")]
        [Required(ErrorMessage = "Yêu cầu nhập mã đánh giá")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Display(Name = "Nội dung đánh giá")]
        public string NoiDungDanhGia { get; set; }


        //Nối đến bảng User
        [Display(Name = "Người viết")]
        public int idUser { get; set; }

        [ForeignKey("idUser")]
        public virtual User User { get; set; }

        //Nối đến bảng đánh giá bài viết
        public virtual ICollection<DanhGiaBaiViet> DanhGiaBaiViet { get; set; }
    }
}