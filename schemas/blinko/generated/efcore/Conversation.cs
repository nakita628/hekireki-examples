#nullable enable

using System;
using System.Collections.Generic;

namespace Blinko.Models;

public partial class Conversation
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public bool IsShare { get; set; } = false;

    public int AccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Accounts Account { get; set; } = null!;
}
