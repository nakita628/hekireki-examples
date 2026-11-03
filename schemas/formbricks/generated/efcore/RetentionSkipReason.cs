#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionSkipReason
{
    [PgName("exempt")]
    Exempt,

    [PgName("lastOwner")]
    LastOwner,

    [PgName("otherOrganization")]
    OtherOrganization,

    [PgName("noRecipient")]
    NoRecipient,
}
