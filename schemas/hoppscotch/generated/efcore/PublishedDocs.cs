#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class PublishedDocs
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string CollectionID { get; set; } = null!;

    public string CreatorUid { get; set; } = null!;

    public string Version { get; set; } = null!;

    public bool AutoSync { get; set; }

    public string? DocumentTree { get; set; }

    public WorkspaceType WorkspaceType { get; set; }

    public string WorkspaceID { get; set; } = null!;

    public string? EnvironmentID { get; set; }

    public string? EnvironmentName { get; set; }

    public string? EnvironmentVariables { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}
