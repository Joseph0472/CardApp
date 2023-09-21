namespace API;

public class Card
{
    public int Id { get; set; }
    public string CreditNumber { get; set; }
    // public string CVC { get; set; }
    public byte[] CVCHash { get; set; }
    public byte[] CVCSalt { get; set; }
    public string ExpiryDateYear { get; set; }
    public string ExpiryDateMonth { get; set; }
}
