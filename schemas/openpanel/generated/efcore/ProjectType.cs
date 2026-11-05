#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum ProjectType
{
    [PgName("website")]
    Website,

    [PgName("app")]
    App,

    [PgName("backend")]
    Backend,
}
