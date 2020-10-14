﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trippy_Land.Models
{
    public class Trippy_Land_Context : DbContext
    {
        public Trippy_Land_Context() : base("Trippy_Land_Connect")
        {
        }

        public DbSet<BaiVietVeDiaDiem> BaiVietVeDiaDiems { get; set; }
        public DbSet<DiaDiem> DiaDiems { get; set; }
        public DbSet<KhachSan> KhachSans { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<Tinh> Tinhs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
    }
}