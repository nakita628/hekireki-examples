#nullable enable

using System;

namespace Typebot.Models;

public partial class BannedIp
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Ip { get; set; } = null!;

    public virtual Typebot ResponsibleTypebot { get; set; } = null!;

    public string ResponsibleTypebotId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
