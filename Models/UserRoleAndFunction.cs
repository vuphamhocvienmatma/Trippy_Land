using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    public class UserRoleAndFunction
    {
        [Key]
        public int Id { get; set; }

        public bool Xem { get; set; }
        public bool Them { get; set; }
        public bool Sua { get; set; }
        public bool Xoa { get; set; }

        //RoleId
        [Display(Name = "Role")]
        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }

        //functionsId
        [Display(Name = "Function")]
        public int FuctionId { get; set; }

        [ForeignKey("FuctionId")]
        public virtual Function Function { get; set; }
    }
}