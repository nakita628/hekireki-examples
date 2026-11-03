#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum WebhookTriggerEvents
{
    [PgName("BOOKING_CREATED")]
    BookingCreated,

    [PgName("BOOKING_PAYMENT_INITIATED")]
    BookingPaymentInitiated,

    [PgName("BOOKING_PAID")]
    BookingPaid,

    [PgName("BOOKING_RESCHEDULED")]
    BookingRescheduled,

    [PgName("BOOKING_REQUESTED")]
    BookingRequested,

    [PgName("BOOKING_CANCELLED")]
    BookingCancelled,

    [PgName("BOOKING_REJECTED")]
    BookingRejected,

    [PgName("BOOKING_NO_SHOW_UPDATED")]
    BookingNoShowUpdated,

    [PgName("FORM_SUBMITTED")]
    FormSubmitted,

    [PgName("MEETING_ENDED")]
    MeetingEnded,

    [PgName("MEETING_STARTED")]
    MeetingStarted,

    [PgName("RECORDING_READY")]
    RecordingReady,

    [PgName("RECORDING_TRANSCRIPTION_GENERATED")]
    RecordingTranscriptionGenerated,

    [PgName("OOO_CREATED")]
    OooCreated,

    [PgName("AFTER_HOSTS_CAL_VIDEO_NO_SHOW")]
    AfterHostsCalVideoNoShow,

    [PgName("AFTER_GUESTS_CAL_VIDEO_NO_SHOW")]
    AfterGuestsCalVideoNoShow,

    [PgName("FORM_SUBMITTED_NO_EVENT")]
    FormSubmittedNoEvent,

    [PgName("DELEGATION_CREDENTIAL_ERROR")]
    DelegationCredentialError,

    [PgName("WRONG_ASSIGNMENT_REPORT")]
    WrongAssignmentReport,
}
