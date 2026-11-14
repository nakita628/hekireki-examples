#nullable enable

using System;

namespace LangWatch.Models;

public partial class WebPushVapidKey
{
    public string Id { get; set; } = "self";

    public string PublicKey { get; set; } = null!;

    public string PrivateKeyEncrypted { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
