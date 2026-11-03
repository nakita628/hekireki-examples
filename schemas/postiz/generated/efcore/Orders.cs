#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Orders
{
    public string Id { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public OrderStatus Status { get; set; }

    public string MessageGroupId { get; set; } = null!;

    public string? CaptureId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<OrderItems> OrdersItems { get; set; } = new List<OrderItems>();

    public virtual User Buyer { get; set; } = null!;

    public virtual MessagesGroup MessageGroup { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;

    public virtual ICollection<PayoutProblems> PayoutProblems { get; set; } = new List<PayoutProblems>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
