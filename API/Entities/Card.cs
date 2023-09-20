namespace API;

public class Card
{
    public int Id { get; set; }
    public string CreditNumber { get; set; }
    public string CVC { get; set; }
    public ExpiryDate ExpiryDate { get; set; }
}
