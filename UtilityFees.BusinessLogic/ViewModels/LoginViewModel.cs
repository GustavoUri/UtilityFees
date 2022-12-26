using System.ComponentModel.DataAnnotations;

namespace UtilityFees.BusinessLogic.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Логин")]
    public string Login { get; set; }
         
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
         
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
}