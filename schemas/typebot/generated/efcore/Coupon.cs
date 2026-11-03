#nullable enable

using System;

namespace Typebot.Models;

public partial class Coupon
{
    public string UserPropertiesToUpdate { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime? DateRedeemed { get; set; }
}
