namespace PurchaseGallery.Api.Models.Assets;
public class Currency
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public decimal ExchangeRate { get; set; }
}