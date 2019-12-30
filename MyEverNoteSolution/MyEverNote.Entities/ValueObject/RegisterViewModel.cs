using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEverNote.Entities.ValueObject
{
    public class RegisterViewModel
    {
        
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]    
        public string UserName { get; set; }



        [DisplayName("E-posta"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(150, ErrorMessage = "{0} max. {1} karakter olmalı."),EmailAddress(ErrorMessage ="{0} geçerli bir adres giriniz.")]
        public string Email { get; set; }


        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. karakter olmalı.")]
        public string Password { get; set; }



        [DisplayName("Şifre Tekrarı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. karakter olmalı."),Compare("Password",ErrorMessage ="{0} ile {1} uyuşmuyor.")]
        public string RePassword { get; set; }



    }
}