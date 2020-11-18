using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    public class Function
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Display(Name = "Tên chức năng")]
        public string TenChucNang { get; set; }


        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }


        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        [Display(Name = "Tên form")]
        public string TenForm { get; set; }

        public virtual ICollection<UserRoleAndFunction> UserRoleAndFunctions { get; set; }
    }
}