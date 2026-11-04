#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class DocumentVectors
{
    public int Id { get; set; }

    public string DocId { get; set; } = null!;

    public string VectorId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
