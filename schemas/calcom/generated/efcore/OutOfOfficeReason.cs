#nullable enable

using System.Collections.Generic;

namespace Calcom.Models;

public partial class OutOfOfficeReason
{
    public int Id { get; set; }

    public string Emoji { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<OutOfOfficeEntry> Entries { get; set; } = new List<OutOfOfficeEntry>();
}
