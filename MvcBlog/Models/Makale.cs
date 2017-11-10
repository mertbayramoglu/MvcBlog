using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Makale
    {
        public Makale()
        {
            this.Yorums = new HashSet<Yorum>();
            this.Etikets = new HashSet<Etiket>();
        }
    
        public int MakaleId { get; set; }
        public string Baslik { get; set; }
        [UIHint("tinymce_full_compressed"),AllowHtml]
        public string Icerik { get; set; }
        public string Foto { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public Nullable<int> UyeId { get; set; }
        public Nullable<int> Okunma { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        public virtual UyeTablo UyeTablo { get; set; }
        public virtual ICollection<Yorum> Yorums { get; set; }
        public virtual ICollection<Etiket> Etikets { get; set; }
    }
}
