#nullable enable

using System;
using System.Collections.Generic;

namespace OpenSaas.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public bool IsAdmin { get; set; } = false;

    public string? PaymentProcessorUserId { get; set; }

    public string? SubscriptionStatus { get; set; }

    public string? SubscriptionPlan { get; set; }

    public DateTime? DatePaid { get; set; }

    public int Credits { get; set; } = 3;

    public virtual ICollection<GptResponse> GptResponses { get; set; } = new List<GptResponse>();

    public virtual ICollection<ContactFormMessage> ContactFormMessages { get; set; } = new List<ContactFormMessage>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();
}
