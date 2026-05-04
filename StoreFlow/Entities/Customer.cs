namespace StoreFlow.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string City { get; set; }
    public string? District { get; set; }
    public decimal Balance { get; set; }
    public string? ImageUrl { get; set; }
}