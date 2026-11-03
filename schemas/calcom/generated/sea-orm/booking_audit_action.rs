use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "BookingAuditAction")]
pub enum BookingAuditAction {
    #[sea_orm(string_value = "created")]
    Created,
    #[sea_orm(string_value = "cancelled")]
    Cancelled,
    #[sea_orm(string_value = "accepted")]
    Accepted,
    #[sea_orm(string_value = "rejected")]
    Rejected,
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "awaiting_host")]
    AwaitingHost,
    #[sea_orm(string_value = "rescheduled")]
    Rescheduled,
    #[sea_orm(string_value = "attendee_added")]
    AttendeeAdded,
    #[sea_orm(string_value = "attendee_removed")]
    AttendeeRemoved,
    #[sea_orm(string_value = "reassignment")]
    Reassignment,
    #[sea_orm(string_value = "location_changed")]
    LocationChanged,
    #[sea_orm(string_value = "no_show_updated")]
    NoShowUpdated,
    #[sea_orm(string_value = "reschedule_requested")]
    RescheduleRequested,
    #[sea_orm(string_value = "seat_booked")]
    SeatBooked,
    #[sea_orm(string_value = "seat_rescheduled")]
    SeatRescheduled,
}
