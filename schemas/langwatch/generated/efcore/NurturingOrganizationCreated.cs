#nullable enable

using System;

namespace LangWatch.Models;

public partial class NurturingOrganizationCreated
{
    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
