public class ProductHistory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public required string PropertyName { get; set; }
    public required string OldValue { get; set; }
    public required string NewValue { get; set; }
    public DateTime ChangeDate { get; set; }

    public ProductEntity? Product { get; set; }
}