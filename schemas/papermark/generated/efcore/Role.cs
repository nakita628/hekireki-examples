#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum Role
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MANAGER")]
    Manager,

    [PgName("MEMBER")]
    Member,

    [PgName("DATAROOM_MEMBER")]
    DataroomMember,
}
