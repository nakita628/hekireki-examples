use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationSettings")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: i32,
    #[sea_orm(column_name = "isOrganizationConfigured", default_value = false)]
    pub is_organization_configured: bool,
    #[sea_orm(column_name = "isOrganizationVerified", default_value = false)]
    pub is_organization_verified: bool,
    #[sea_orm(column_name = "orgAutoAcceptEmail")]
    pub org_auto_accept_email: String,
    #[sea_orm(column_name = "lockEventTypeCreationForUsers", default_value = false)]
    pub lock_event_type_creation_for_users: bool,
    #[sea_orm(column_name = "adminGetsNoSlotsNotification", default_value = false)]
    pub admin_gets_no_slots_notification: bool,
    #[sea_orm(column_name = "isAdminReviewed", default_value = false)]
    pub is_admin_reviewed: bool,
    #[sea_orm(column_name = "isAdminAPIEnabled", default_value = false)]
    pub is_admin_apienabled: bool,
    #[sea_orm(column_name = "allowSEOIndexing", default_value = false)]
    pub allow_seoindexing: bool,
    #[sea_orm(
        column_name = "orgProfileRedirectsToVerifiedDomain",
        default_value = false
    )]
    pub org_profile_redirects_to_verified_domain: bool,
    #[sea_orm(
        column_name = "disablePhoneOnlySMSNotifications",
        default_value = false
    )]
    pub disable_phone_only_smsnotifications: bool,
    #[sea_orm(column_name = "disableAutofillOnBookingPage", default_value = false)]
    pub disable_autofill_on_booking_page: bool,
    #[sea_orm(column_name = "orgAutoJoinOnSignup", default_value = true)]
    pub org_auto_join_on_signup: bool,
    #[sea_orm(
        column_name = "disableAttendeeConfirmationEmail",
        default_value = false
    )]
    pub disable_attendee_confirmation_email: bool,
    #[sea_orm(
        column_name = "disableAttendeeCancellationEmail",
        default_value = false
    )]
    pub disable_attendee_cancellation_email: bool,
    #[sea_orm(column_name = "disableAttendeeRescheduledEmail", default_value = false)]
    pub disable_attendee_rescheduled_email: bool,
    #[sea_orm(column_name = "disableAttendeeRequestEmail", default_value = false)]
    pub disable_attendee_request_email: bool,
    #[sea_orm(column_name = "disableAttendeeReassignedEmail", default_value = false)]
    pub disable_attendee_reassigned_email: bool,
    #[sea_orm(
        column_name = "disableAttendeeAwaitingPaymentEmail",
        default_value = false
    )]
    pub disable_attendee_awaiting_payment_email: bool,
    #[sea_orm(
        column_name = "disableAttendeeRescheduleRequestEmail",
        default_value = false
    )]
    pub disable_attendee_reschedule_request_email: bool,
    #[sea_orm(
        column_name = "disableAttendeeLocationChangeEmail",
        default_value = false
    )]
    pub disable_attendee_location_change_email: bool,
    #[sea_orm(column_name = "disableAttendeeNewEventEmail", default_value = false)]
    pub disable_attendee_new_event_email: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::OrganizationId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_one = "super::dsync_data::Entity")]
    DSyncData,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::dsync_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DSyncData.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
