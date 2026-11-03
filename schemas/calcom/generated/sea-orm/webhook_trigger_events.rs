use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "WebhookTriggerEvents"
)]
pub enum WebhookTriggerEvents {
    #[sea_orm(string_value = "BOOKING_CREATED")]
    BookingCreated,
    #[sea_orm(string_value = "BOOKING_PAYMENT_INITIATED")]
    BookingPaymentInitiated,
    #[sea_orm(string_value = "BOOKING_PAID")]
    BookingPaid,
    #[sea_orm(string_value = "BOOKING_RESCHEDULED")]
    BookingRescheduled,
    #[sea_orm(string_value = "BOOKING_REQUESTED")]
    BookingRequested,
    #[sea_orm(string_value = "BOOKING_CANCELLED")]
    BookingCancelled,
    #[sea_orm(string_value = "BOOKING_REJECTED")]
    BookingRejected,
    #[sea_orm(string_value = "BOOKING_NO_SHOW_UPDATED")]
    BookingNoShowUpdated,
    #[sea_orm(string_value = "FORM_SUBMITTED")]
    FormSubmitted,
    #[sea_orm(string_value = "MEETING_ENDED")]
    MeetingEnded,
    #[sea_orm(string_value = "MEETING_STARTED")]
    MeetingStarted,
    #[sea_orm(string_value = "RECORDING_READY")]
    RecordingReady,
    #[sea_orm(string_value = "RECORDING_TRANSCRIPTION_GENERATED")]
    RecordingTranscriptionGenerated,
    #[sea_orm(string_value = "OOO_CREATED")]
    OooCreated,
    #[sea_orm(string_value = "AFTER_HOSTS_CAL_VIDEO_NO_SHOW")]
    AfterHostsCalVideoNoShow,
    #[sea_orm(string_value = "AFTER_GUESTS_CAL_VIDEO_NO_SHOW")]
    AfterGuestsCalVideoNoShow,
    #[sea_orm(string_value = "FORM_SUBMITTED_NO_EVENT")]
    FormSubmittedNoEvent,
    #[sea_orm(string_value = "DELEGATION_CREDENTIAL_ERROR")]
    DelegationCredentialError,
    #[sea_orm(string_value = "WRONG_ASSIGNMENT_REPORT")]
    WrongAssignmentReport,
}
