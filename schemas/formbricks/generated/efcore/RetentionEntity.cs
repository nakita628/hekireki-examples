#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionEntity
{
    [PgName("responses")]
    Responses,

    [PgName("surveys")]
    Surveys,

    [PgName("members")]
    Members,
}
