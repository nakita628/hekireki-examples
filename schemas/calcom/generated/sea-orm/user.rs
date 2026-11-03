use super::creation_source::CreationSource;
use super::identity_provider::IdentityProvider;
use super::smslock_state::SMSLockState;
use super::user_permission_role::UserPermissionRole;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_type = "Uuid")]
    pub uuid: String,
    pub username: Option<String>,
    pub name: Option<String>,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "emailVerified")]
    pub email_verified: Option<DateTime>,
    pub bio: Option<String>,
    #[sea_orm(column_name = "avatarUrl")]
    pub avatar_url: Option<String>,
    #[sea_orm(column_name = "timeZone", default_value = "Europe/London")]
    pub time_zone: String,
    #[sea_orm(column_name = "weekStart", default_value = "Sunday")]
    pub week_start: String,
    #[sea_orm(column_name = "bufferTime", default_value = 0)]
    pub buffer_time: i32,
    #[sea_orm(column_name = "hideBranding", default_value = false)]
    pub hide_branding: bool,
    pub theme: Option<String>,
    #[sea_orm(column_name = "appTheme")]
    pub app_theme: Option<String>,
    #[sea_orm(column_name = "created")]
    pub created_date: DateTime,
    #[sea_orm(column_name = "trialEndsAt")]
    pub trial_ends_at: Option<DateTime>,
    #[sea_orm(column_name = "lastActiveAt")]
    pub last_active_at: Option<DateTime>,
    #[sea_orm(column_name = "defaultScheduleId")]
    pub default_schedule_id: Option<i32>,
    #[sea_orm(column_name = "completedOnboarding", default_value = false)]
    pub completed_onboarding: bool,
    pub locale: Option<String>,
    #[sea_orm(column_name = "timeFormat", default_value = 12)]
    pub time_format: Option<i32>,
    #[sea_orm(column_name = "twoFactorSecret")]
    pub two_factor_secret: Option<String>,
    #[sea_orm(column_name = "twoFactorEnabled", default_value = false)]
    pub two_factor_enabled: bool,
    #[sea_orm(column_name = "backupCodes")]
    pub backup_codes: Option<String>,
    #[sea_orm(column_name = "identityProvider", default_value = "CAL")]
    pub identity_provider: IdentityProvider,
    #[sea_orm(column_name = "identityProviderId")]
    pub identity_provider_id: Option<String>,
    #[sea_orm(column_name = "invitedTo")]
    pub invited_to: Option<i32>,
    #[sea_orm(column_name = "brandColor")]
    pub brand_color: Option<String>,
    #[sea_orm(column_name = "darkBrandColor")]
    pub dark_brand_color: Option<String>,
    #[sea_orm(column_name = "allowDynamicBooking", default_value = true)]
    pub allow_dynamic_booking: Option<bool>,
    #[sea_orm(column_name = "allowSEOIndexing", default_value = true)]
    pub allow_seoindexing: Option<bool>,
    #[sea_orm(column_name = "receiveMonthlyDigestEmail", default_value = true)]
    pub receive_monthly_digest_email: Option<bool>,
    #[sea_orm(column_name = "requiresBookerEmailVerification", default_value = false)]
    pub requires_booker_email_verification: Option<bool>,
    pub metadata: Option<Json>,
    #[sea_orm(default_value = false)]
    pub verified: Option<bool>,
    #[sea_orm(default_value = "USER")]
    pub role: UserPermissionRole,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<i32>,
    #[sea_orm(default_value = false)]
    pub locked: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "movedToProfileId")]
    pub moved_to_profile_id: Option<i32>,
    #[sea_orm(column_name = "isPlatformManaged", default_value = false)]
    pub is_platform_managed: bool,
    #[sea_orm(column_name = "smsLockState", default_value = "UNLOCKED")]
    pub sms_lock_state: SMSLockState,
    #[sea_orm(column_name = "smsLockReviewedByAdmin", default_value = false)]
    pub sms_lock_reviewed_by_admin: bool,
    #[sea_orm(column_name = "referralLinkId")]
    pub referral_link_id: Option<String>,
    #[sea_orm(column_name = "creationSource")]
    pub creation_source: Option<CreationSource>,
    #[sea_orm(column_name = "autoOptInFeatures", default_value = false)]
    pub auto_opt_in_features: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::OrganizationId",
        to = "super::team::Column::Id",
        on_delete = "SetNull"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::profile::Entity",
        from = "Column::MovedToProfileId",
        to = "super::profile::Column::Id",
        on_delete = "SetNull"
    )]
    MovedToProfile,
    #[sea_orm(has_many = "super::travel_schedule::Entity")]
    TravelSchedules,
    #[sea_orm(has_many = "super::credential::Entity")]
    Credentials,
    #[sea_orm(has_many = "super::membership::Entity")]
    Teams,
    #[sea_orm(
        has_many = "super::booking::Entity",
        from = "Column::Id",
        to = "super::booking::Column::UserId"
    )]
    Bookings,
    #[sea_orm(has_many = "super::schedule::Entity")]
    Schedules,
    #[sea_orm(has_many = "super::selected_calendar::Entity")]
    SelectedCalendars,
    #[sea_orm(has_many = "super::availability::Entity")]
    Availability,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::feedback::Entity")]
    Feedback,
    #[sea_orm(has_many = "super::event_type::Entity")]
    OwnedEventTypes,
    #[sea_orm(has_many = "super::verified_number::Entity")]
    VerifiedNumbers,
    #[sea_orm(has_many = "super::verified_email::Entity")]
    VerifiedEmails,
    #[sea_orm(has_many = "super::host::Entity")]
    Hosts,
    #[sea_orm(has_many = "super::access_code::Entity")]
    AccessCodes,
    #[sea_orm(
        has_many = "super::out_of_office_entry::Entity",
        from = "Column::Id",
        to = "super::out_of_office_entry::Column::UserId"
    )]
    BookingRedirects,
    #[sea_orm(
        has_many = "super::out_of_office_entry::Entity",
        from = "Column::Id",
        to = "super::out_of_office_entry::Column::ToUserId"
    )]
    BookingRedirectsTo,
    #[sea_orm(has_many = "super::access_token::Entity")]
    AccessToken,
    #[sea_orm(has_many = "super::refresh_token::Entity")]
    RefreshToken,
    #[sea_orm(has_many = "super::platform_authorization_token::Entity")]
    PlatformAuthorizationToken,
    #[sea_orm(
        has_many = "super::profile::Entity",
        from = "Column::Id",
        to = "super::profile::Column::UserId"
    )]
    Profiles,
    #[sea_orm(has_many = "super::secondary_email::Entity")]
    SecondaryEmails,
    #[sea_orm(has_many = "super::out_of_office_reason::Entity")]
    OutOfOfficeReasons,
    #[sea_orm(has_many = "super::notifications_subscriptions::Entity")]
    NotificationsSubscriptions,
    #[sea_orm(has_many = "super::user_features::Entity")]
    Features,
    #[sea_orm(
        has_many = "super::booking::Entity",
        from = "Column::Id",
        to = "super::booking::Column::ReassignById"
    )]
    ReassignedBookings,
    #[sea_orm(
        has_many = "super::attribute_to_user::Entity",
        from = "Column::Id",
        to = "super::attribute_to_user::Column::CreatedById"
    )]
    CreatedAttributeToUsers,
    #[sea_orm(
        has_many = "super::attribute_to_user::Entity",
        from = "Column::Id",
        to = "super::attribute_to_user::Column::UpdatedById"
    )]
    UpdatedAttributeToUsers,
    #[sea_orm(
        has_many = "super::event_type_translation::Entity",
        from = "Column::Id",
        to = "super::event_type_translation::Column::CreatedBy"
    )]
    CreatedTranslations,
    #[sea_orm(
        has_many = "super::event_type_translation::Entity",
        from = "Column::Id",
        to = "super::event_type_translation::Column::UpdatedBy"
    )]
    UpdatedTranslations,
    #[sea_orm(has_many = "super::booking_report::Entity")]
    ReportedBookings,
    #[sea_orm(
        has_many = "super::wrong_assignment_report::Entity",
        from = "Column::Id",
        to = "super::wrong_assignment_report::Column::ReportedById"
    )]
    WrongAssignmentReports,
    #[sea_orm(
        has_many = "super::wrong_assignment_report::Entity",
        from = "Column::Id",
        to = "super::wrong_assignment_report::Column::ReviewedById"
    )]
    ReviewedWrongAssignmentReports,
    #[sea_orm(has_many = "super::booking_internal_note::Entity")]
    BookingInternalNote,
    #[sea_orm(has_many = "super::organization_onboarding::Entity")]
    CreatedOrganizationOnboardings,
    #[sea_orm(has_many = "super::filter_segment::Entity")]
    FilterSegments,
    #[sea_orm(has_many = "super::user_filter_segment_preference::Entity")]
    FilterSegmentPreferences,
    #[sea_orm(has_many = "super::cal_ai_phone_number::Entity")]
    CalAiPhoneNumbers,
    #[sea_orm(has_many = "super::agent::Entity")]
    Agents,
    #[sea_orm(has_many = "super::oauth_client::Entity")]
    OAuthClients,
    #[sea_orm(has_one = "super::user_password::Entity")]
    Password,
    #[sea_orm(has_one = "super::destination_calendar::Entity")]
    DestinationCalendar,
    #[sea_orm(has_one = "super::credit_balance::Entity")]
    CreditBalance,
    #[sea_orm(has_one = "super::user_holiday_settings::Entity")]
    HolidaySettings,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::profile::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MovedToProfile.def()
    }
}

impl Related<super::travel_schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TravelSchedules.def()
    }
}

impl Related<super::credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credentials.def()
    }
}

impl Related<super::membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Bookings.def()
    }
}

impl Related<super::schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Schedules.def()
    }
}

impl Related<super::selected_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SelectedCalendars.def()
    }
}

impl Related<super::availability::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Availability.def()
    }
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::feedback::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Feedback.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OwnedEventTypes.def()
    }
}

impl Related<super::verified_number::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerifiedNumbers.def()
    }
}

impl Related<super::verified_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerifiedEmails.def()
    }
}

impl Related<super::host::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Hosts.def()
    }
}

impl Related<super::access_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessCodes.def()
    }
}

impl Related<super::out_of_office_entry::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingRedirects.def()
    }
}

impl Related<super::access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessToken.def()
    }
}

impl Related<super::refresh_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RefreshToken.def()
    }
}

impl Related<super::platform_authorization_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlatformAuthorizationToken.def()
    }
}

impl Related<super::secondary_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SecondaryEmails.def()
    }
}

impl Related<super::out_of_office_reason::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OutOfOfficeReasons.def()
    }
}

impl Related<super::notifications_subscriptions::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationsSubscriptions.def()
    }
}

impl Related<super::user_features::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Features.def()
    }
}

impl Related<super::attribute_to_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedAttributeToUsers.def()
    }
}

impl Related<super::event_type_translation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedTranslations.def()
    }
}

impl Related<super::booking_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReportedBookings.def()
    }
}

impl Related<super::wrong_assignment_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WrongAssignmentReports.def()
    }
}

impl Related<super::booking_internal_note::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingInternalNote.def()
    }
}

impl Related<super::organization_onboarding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedOrganizationOnboardings.def()
    }
}

impl Related<super::filter_segment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FilterSegments.def()
    }
}

impl Related<super::user_filter_segment_preference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FilterSegmentPreferences.def()
    }
}

impl Related<super::cal_ai_phone_number::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CalAiPhoneNumbers.def()
    }
}

impl Related<super::agent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agents.def()
    }
}

impl Related<super::oauth_client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OAuthClients.def()
    }
}

impl Related<super::user_password::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Password.def()
    }
}

impl Related<super::destination_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DestinationCalendar.def()
    }
}

impl Related<super::credit_balance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreditBalance.def()
    }
}

impl Related<super::user_holiday_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HolidaySettings.def()
    }
}

impl Related<super::platform_oauth_client::Entity> for Entity {
    fn to() -> RelationDef {
        super::platform_oauth_client_to_user::Relation::PlatformOAuthClient.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::platform_oauth_client_to_user::Relation::User.def().rev())
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            uuid: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_date.is_not_set() {
            self.created_date = Set(now.naive_utc());
        }
        Ok(self)
    }
}
