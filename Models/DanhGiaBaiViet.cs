using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy_Land.Models
{
    public class DanhGiaBaiViet
    {
        [Key]
        [Column(Order = 0)]
        [DisplayName("Bài viết")]
        public int IdBaiBiet { get; set; }

        [Key]
        [Column(Order = 1)]
        [DisplayName("Đánh giá")]
        public int IdDanhGia { get; set; }

        [ForeignKey("IdBaiBiet")]
        public virtual BaiVietVeDiaDiem BaiVietVeDiaDiem { get; set; }

        [ForeignKey("IdDanhGia")]
        public virtual DanhGia DanhGia { get; set; }
    }
}