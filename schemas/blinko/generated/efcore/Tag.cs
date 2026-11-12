#nullable enable

using System;
using System.Collections.Generic;

namespace Blinko.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Icon { get; set; } = "";

    public int Parent { get; set; } = 0;

    public int? AccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<TagsToNote> TagsToNote { get; set; } = new List<TagsToNote>();

    public int SortOrder { get; set; } = 0;

    public virtual Accounts? Account { get; set; }
}
