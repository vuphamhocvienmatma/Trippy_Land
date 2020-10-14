using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

        [Display(Name = "Tỉnh")]
        public int idTinh { get; set; }

        [ForeignKey("idTinh")]
        public virtual Tinh Tinh { get; set; }

        [Display(Name = "Tên người dùng")]
        public int idUser { get; set; }

        [ForeignKey("idUser")]
        public virtual User User { get; set; }
    }
}