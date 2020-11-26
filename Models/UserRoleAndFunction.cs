using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    public class UserRoleAndFunction
    {
        [Key]
        public int Id { get; set; }

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