#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BookingAuditType
{
    [PgName("record_created")]
    RecordCreated,

    [PgName("record_updated")]
    RecordUpdated,

    [PgName("record_deleted")]
    RecordDeleted,
}
