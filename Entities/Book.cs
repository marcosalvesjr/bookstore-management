namespace BookstoreManeger.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stoke { get; set; }
    public DateTime Created_At { get; set; } 
    public DateTime Updated_At { get; set; }
}
