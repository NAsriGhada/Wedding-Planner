#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    //-
    [Key]
    public int UserId { get; set; }

    //-
    [Required(ErrorMessage = "First Name is Required ❌❌❌")]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters")]
    public string FirstName { get; set; }


    //-
    [Required(ErrorMessage = "Last Name is Required ❌❌❌")]
    [MinLength(2, ErrorMessage = "Last Name  must be at least 2 characters")]
    public string LastName { get; set; }

    //-
    [Required(ErrorMessage = "Email is Required ❌❌❌")]
    [EmailAddress]
    public string Email { get; set; }

    //-
    [Required(ErrorMessage = "Password is very Required !!!!")]
    [MinLength(8, ErrorMessage = "Password must be more than 8 characters !!!!!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    //--
    [NotMapped]
    [Required(ErrorMessage = "Confirm Password is Reaquired !!!!")]
    [Compare("Password", ErrorMessage = "Donsen't match the Password !!!!!")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Wedding> WeddingPlanned {get; set;} = new List<Wedding>(); //many to one
}  