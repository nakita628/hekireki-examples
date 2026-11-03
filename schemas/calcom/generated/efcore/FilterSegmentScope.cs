#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum FilterSegmentScope
{
    [PgName("USER")]
    User,

    [PgName("TEAM")]
    Team,
}
