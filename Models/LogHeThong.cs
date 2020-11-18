using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    public class LogHeThong
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Mô tả")]
        [Column(TypeName = "NVARCHAR")]
        public string MoTa { get; set; }
        public int HanhDongId { get; set; }


        public string FormName { get; set; }

        [Display(Name = "Người tạo")]
        public string NguoiTao { get; set; }

        [Display(Name = "Địa chỉ máy tính")]
        public string DiaChiMayTinh { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}