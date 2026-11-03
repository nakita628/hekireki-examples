#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum IntegrationType
{
    [PgName("googleSheets")]
    GoogleSheets,

    [PgName("notion")]
    Notion,

    [PgName("airtable")]
    Airtable,

    [PgName("slack")]
    Slack,
}
