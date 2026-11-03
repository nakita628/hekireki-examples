#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum EventTypeAutoTranslatedField
{
    [PgName("DESCRIPTION")]
    Description,

    [PgName("TITLE")]
    Title,
}
