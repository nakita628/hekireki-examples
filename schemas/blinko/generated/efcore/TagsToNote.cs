#nullable enable

namespace Blinko.Models;

public partial class TagsToNote
{
    public int Id { get; set; }

    public int NoteId { get; set; } = 0;

    public int TagId { get; set; } = 0;

    public virtual Notes Note { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
