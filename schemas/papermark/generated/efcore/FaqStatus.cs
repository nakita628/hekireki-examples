#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum FaqStatus
{
    [PgName("DRAFT")]
    Draft,

    [PgName("PUBLISHED")]
    Published,

    [PgName("ARCHIVED")]
    Archived,
}
