using System.ComponentModel.DataAnnotations;

namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UyeTablo
    {
        public UyeTablo()
        {
            this.Makales = new HashSet<Makale>();
            this.Yorums = new HashSet<Yorum>();
        }
        public int UyeId { get; set; }
        [Required(ErrorMessage = "Bu alan boþ olamaz.")]
        public string KullaniciAdi { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen e-posta adresinizi geçerli bir formatta giriniz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan boþ olamaz.")]
        public string Sifre { get; set; }
        [Required(ErrorMessage = "Bu alan boþ olamaz.")]
        public string AdSoyad { get; set; }
        [Required(ErrorMessage = "Bu alan boþ olamaz.")]
        public string Foto { get; set; }
        public Nullable<int> YetkiId { get; set; }
    
        public virtual ICollection<Makale> Makales { get; set; }
        public virtual Yetki Yetki { get; set; }
        public virtual ICollection<Yorum> Yorums { get; set; }
    }
}
