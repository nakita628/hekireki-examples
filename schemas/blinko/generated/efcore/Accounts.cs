#nullable enable

using System;
using System.Collections.Generic;

namespace Blinko.Models;

public partial class Accounts
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Nickname { get; set; } = "";

    public string Password { get; set; } = "";

    public string Image { get; set; } = "";

    public string ApiToken { get; set; } = "";

    public string Description { get; set; } = "";

    public int Note { get; set; } = 0;

    public string Role { get; set; } = "";

    public string LoginType { get; set; } = "";

    public int? LinkAccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Notes> Notes { get; set; } = new List<Notes>();

    public virtual ICollection<Config> Configs { get; set; } = new List<Config>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual ICollection<Attachments> Attachments { get; set; } = new List<Attachments>();

    public virtual ICollection<Follows> Follows { get; set; } = new List<Follows>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<NoteInternalShare> SharedNotes { get; set; } = new List<NoteInternalShare>();

    public virtual ICollection<AiScheduledTask> AiScheduledTasks { get; set; } = new List<AiScheduledTask>();
}
