#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MeetingRecordingStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("SCHEDULED")]
    Scheduled,

    [PgName("CANCELLING")]
    Cancelling,

    [PgName("JOINING")]
    Joining,

    [PgName("IN_WAITING_ROOM")]
    InWaitingRoom,

    [PgName("IN_CALL")]
    InCall,

    [PgName("RECORDING")]
    Recording,

    [PgName("CALL_ENDED")]
    CallEnded,

    [PgName("DONE")]
    Done,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
