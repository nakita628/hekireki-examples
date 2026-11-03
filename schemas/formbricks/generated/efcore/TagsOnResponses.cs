#nullable enable

namespace Formbricks.Models;

public partial class TagsOnResponses
{
    public string ResponseId { get; set; } = null!;

    public virtual Response Response { get; set; } = null!;

    public string TagId { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
