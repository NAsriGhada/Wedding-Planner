#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace weddingPlannerZied.Models;
public class LoginUser
{

    [Required(ErrorMessage = "Email is Required ❌❌❌")]
    [EmailAddress]
    public string LoginEmail { get; set; }

    //-
    [Required(ErrorMessage = "Password is Reaquired !!!!")]
    [MinLength(8, ErrorMessage = "Password must be more than 8 characters !!!!!!")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }

}