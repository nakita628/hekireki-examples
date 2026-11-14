#nullable enable

using System;

namespace LangWatch.Models;

public partial class SystemMigrationEnrollment
{
    public string OrganizationId { get; set; } = null!;

    public string MigrationName { get; set; } = null!;

    public string EnrolledByUserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
