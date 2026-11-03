#nullable enable

namespace Postiz.Models;

public partial class ExisingPlugData
{
    public string Id { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public string MethodName { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual Integration Integration { get; set; } = null!;
}
