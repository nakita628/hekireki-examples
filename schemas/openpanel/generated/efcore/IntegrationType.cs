#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum IntegrationType
{
    [PgName("app")]
    App,

    [PgName("mail")]
    Mail,

    [PgName("custom")]
    Custom,
}
