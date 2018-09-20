using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Kostagram_1._1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class LogovanjeModel
    {
        [Required(ErrorMessage = "Niste uneli korisnicko ime")]
        [Display(Name = "Korisnicko ime")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Niste uneli lozinku")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool ZapamtiMe { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegistracijaModel
    {
        [Required(ErrorMessage = "Morate uneti korisnicko ime")]
        [Display(Name = "Korisnicko ime *")]
        [StringLength(20, ErrorMessage = "Korisnicko ime sadrzi najvise 20 karaktera")]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Morate uneti ime")]
        [Display(Name = "Email *")]

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime")]
        [Display(Name = "Ime *")]
        [StringLength(20, ErrorMessage = ("Ime sadrzi najvise 20 karaktera"))]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Morate uneti prezime")]
        [StringLength(20, ErrorMessage = "Prezime sadrzi najvise 20 karaktera")]
        [Display(Name = "Prezime *")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Morate uneti lozinku")]
        [Display(Name = "Lozinka *")]
        [StringLength(50, ErrorMessage = "Polje {0} mora imati bar {2} karaktera", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Morate potvrditi lozinku")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku *")]
        [Compare("Lozinka", ErrorMessage = "Potvrda ne odgovara lozinki")]
        public string PotvrdaLozinke { get; set; }

        [Display(Name = "Datum rodjenja")]
        [DisplayFormat(DataFormatString ="dd.MM.yyy", ApplyFormatInEditMode =true)]
        public DateTime DatumRodjenja { get; set; }

        public DateTime DatumRegistracije { get; set; }

        public bool Pol { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
