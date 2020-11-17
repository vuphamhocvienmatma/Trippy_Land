using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    [Table("UserRoles")]
    public class UserRole
    {
        [Key]
        [Display(Name = "Mã Role")]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Display(Name = "Tên Role")]
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string TenRole { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}