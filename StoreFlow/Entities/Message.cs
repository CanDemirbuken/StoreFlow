namespace StoreFlow.Entities;

public class Message
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string Sender { get; set; }
    public string SenderImageUrl { get; set; }
    public DateTime DateTime { get; set; }
    public bool IsRead { get; set; }
}
