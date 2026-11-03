#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum ParticipantVisibility
{
    [PgName("full")]
    Full,

    [PgName("scoresOnly")]
    ScoresOnly,

    [PgName("limited")]
    Limited,
}
