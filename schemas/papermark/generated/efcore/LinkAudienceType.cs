#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum LinkAudienceType
{
    [PgName("GENERAL")]
    General,

    [PgName("GROUP")]
    Group,

    [PgName("TEAM")]
    Team,
}
