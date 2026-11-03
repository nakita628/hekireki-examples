#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class MessagesGroup
{
    public string Id { get; set; } = null!;

    public string BuyerOrganizationId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Messages> Messages { get; set; } = new List<Messages>();

    public virtual User Buyer { get; set; } = null!;

    public virtual Organization BuyerOrganization { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
