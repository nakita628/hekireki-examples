#nullable enable

namespace Postiz.Models;

public partial class OrderItems
{
    public string Id { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public int Quantity { get; set; }

    public int Price { get; set; }

    public virtual Integration Integration { get; set; } = null!;

    public virtual Orders Order { get; set; } = null!;
}
