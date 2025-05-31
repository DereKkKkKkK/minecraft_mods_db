using System.ComponentModel.DataAnnotations;

namespace DTO.User;

public class LoginUserDto
{
    [Required(ErrorMessage = "Поле Nickname является обязательным")]
    public string Nickname { get; set; }
    [Required(ErrorMessage = "Поле Password является обязательным")]
    public string Password { get; set; }
}