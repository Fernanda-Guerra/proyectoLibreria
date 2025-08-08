using System.ComponentModel.DataAnnotations;

namespace PruebaBlazor.Models
{
    public class LoginModel 
    {
        [Required(ErrorMessage = "El correo electr�nico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es v�lido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contrase�a es obligatoria.")]
        public string Password { get; set; } = string.Empty;
    }
}
