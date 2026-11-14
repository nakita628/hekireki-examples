#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum TriggerKind
{
    [PgName("AUTOMATION")]
    Automation,

    [PgName("ALERT")]
    Alert,

    [PgName("REPORT")]
    Report,
}
