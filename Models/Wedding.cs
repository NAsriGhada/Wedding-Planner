#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Wedding
{
    [Key]
    public int WeddingId {get; set;}

    public int UserId {get; set;}

    [Required(ErrorMessage = "Wedder One is Required ❌❌❌")]
    [MinLength(2, ErrorMessage = "Wedder One  must be at least 2 characters")]
    public string WedderOne { get; set; }


    //
    [Required(ErrorMessage = "Wedder Two is Required ❌❌❌")]
    [MinLength(2, ErrorMessage = "Wedder Two  must be at least 2 characters")]
    public string WedderTwo { get; set; }

    //-
    [Required]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Address is Required ❌❌❌")]
    [MinLength(5, ErrorMessage = "Address  must be at least 5 characters")]
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User ? User {get; set;} // one to many
    
}