#nullable enable

using System;
using System.Collections.Generic;

namespace Blinko.Models;

public partial class Notes
{
    public int Id { get; set; }

    public int Type { get; set; } = 0;

    public string Content { get; set; } = "";

    public bool IsArchived { get; set; } = false;

    public bool IsRecycle { get; set; } = false;

    public bool IsShare { get; set; } = false;

    public bool IsTop { get; set; } = false;

    public bool IsReviewed { get; set; } = false;

    public string SharePassword { get; set; } = "";

    public string? ShareEncryptedUrl { get; set; }

    public DateTime? ShareExpiryDate { get; set; }

    public int? ShareMaxView { get; set; } = 0;

    public int? ShareViewCount { get; set; } = 0;

    public string? Metadata { get; set; }

    public int? AccountId { get; set; }

    public int SortOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Attachments> Attachments { get; set; } = new List<Attachments>();

    public virtual ICollection<TagsToNote> Tags { get; set; } = new List<TagsToNote>();

    public virtual Accounts? Account { get; set; }

    public virtual ICollection<NoteReference> ReferencedBy { get; set; } = new List<NoteReference>();

    public virtual ICollection<NoteReference> References { get; set; } = new List<NoteReference>();

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual ICollection<NoteHistory> Histories { get; set; } = new List<NoteHistory>();

    public virtual ICollection<NoteInternalShare> InternalShares { get; set; } = new List<NoteInternalShare>();
}
