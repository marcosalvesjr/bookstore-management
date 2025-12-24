using System.ComponentModel.DataAnnotations;

namespace BookstoreManeger.Request;

public class BookRequestJson
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Author {  get; set;  } = string.Empty;
    [Required]
    public string Genre {  get; set; } = string.Empty;
    [Required]
    public decimal Price {get; set;  }
    [Required]
    public int Stock { get; set; }
}
