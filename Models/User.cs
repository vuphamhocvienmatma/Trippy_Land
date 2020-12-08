using System;
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


        [Display(Name = "Là quản trị viên")]
        public bool IsSupper { get; set; }

        [Display(Name = "Email đã xác thực")]
        public bool EmailConfirm { get; set; }


        public Nullable<bool> IsValid { get; set; }

        [Display(Name = "Role")]
        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }




    }
}