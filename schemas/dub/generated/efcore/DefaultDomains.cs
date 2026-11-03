#nullable enable

namespace Dub.Models;

public partial class DefaultDomains
{
    public string Id { get; set; } = null!;

    public bool Dubsh { get; set; } = true;

    public bool Chatgpt { get; set; } = true;

    public bool Sptifi { get; set; } = true;

    public bool Gitnew { get; set; } = true;

    public bool Callink { get; set; } = true;

    public bool Amznid { get; set; } = true;

    public bool Ggllink { get; set; } = true;

    public bool Figpage { get; set; } = true;

    public bool Loooooooong { get; set; } = false;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
