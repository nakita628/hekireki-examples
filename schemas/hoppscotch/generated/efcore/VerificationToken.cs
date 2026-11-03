#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class VerificationToken
{
    public string DeviceIdentifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string UserUid { get; set; } = null!;

    public DateTime ExpiresOn { get; set; }

    public virtual User User { get; set; } = null!;
}
