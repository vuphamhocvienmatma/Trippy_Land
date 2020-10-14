using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Display(Name = "Mã người dùng")]
        [Required(ErrorMessage = "Yêu cầu nhập mã người dùng")]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string MatKhau { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Yêu cầu nhập tên người dùng")]
        public string TenNguoiDung { get; set; }

        [Display(Name = "Số điện thoại")]
        public int PhoneNumber { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Giới tính")]
        [Column(TypeName = "NVARCHAR")]
        public string GioiTinh { get; set; }

        [Display(Name = "Ảnh")]
        [StringLength(500)]
        public string PictureId { get; set; }

        public virtual ICollection<DanhGia> DanhGia { get; set; }
    }
}