#nullable enable

using NpgsqlTypes;

namespace Linkwarden.Models;

public enum AiTaggingMethod
{
    [PgName("DISABLED")]
    Disabled,

    [PgName("GENERATE")]
    Generate,

    [PgName("EXISTING")]
    Existing,

    [PgName("PREDEFINED")]
    Predefined,
}
