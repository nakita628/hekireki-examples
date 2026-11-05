#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum ProjectScope
{
    [PgName("all")]
    All,

    [PgName("selected")]
    Selected,
}
