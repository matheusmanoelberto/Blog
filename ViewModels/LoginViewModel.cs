using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O E-mail é Obrigatório")]
    [EmailAddress(ErrorMessage ="O E-mail é Inválido")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Informe a senha")]
    public string Password { get; set; }
}