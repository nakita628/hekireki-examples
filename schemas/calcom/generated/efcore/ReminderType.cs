#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum ReminderType
{
    [PgName("PENDING_BOOKING_CONFIRMATION")]
    PendingBookingConfirmation,
}
