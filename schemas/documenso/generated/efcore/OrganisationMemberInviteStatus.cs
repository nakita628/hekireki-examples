#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum OrganisationMemberInviteStatus
{
    [PgName("ACCEPTED")]
    Accepted,

    [PgName("PENDING")]
    Pending,

    [PgName("DECLINED")]
    Declined,
}
