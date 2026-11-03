use super::rrreset_interval::RRResetInterval;
use super::rrtimestamp_basis::RRTimestampBasis;
use super::smslock_state::SMSLockState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Team")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub name: String,
    pub slug: Option<String>,
    #[sea_orm(column_name = "logoUrl")]
    pub logo_url: Option<String>,
    #[sea_orm(column_name = "calVideoLogo")]
    pub cal_video_logo: Option<String>,
    #[sea_orm(column_name = "appLogo")]
    pub app_logo: Option<String>,
    #[sea_orm(column_name = "appIconLogo")]
    pub app_icon_logo: Option<String>,
    pub bio: Option<String>,
    #[sea_orm(column_name = "hideBranding", default_value = false)]
    pub hide_branding: bool,
    #[sea_orm(column_name = "hideTeamProfileLink", default_value = false)]
    pub hide_team_profile_link: bool,
    #[sea_orm(column_name = "isPrivate", default_value = false)]
    pub is_private: bool,
    #[sea_orm(column_name = "hideBookATeamMember", default_value = false)]
    pub hide_book_ateam_member: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    pub metadata: Option<Json>,
    pub theme: Option<String>,
    #[sea_orm(column_name = "rrResetInterval", default_value = "MONTH")]
    pub rr_reset_interval: Option<RRResetInterval>,
    #[sea_orm(column_name = "rrTimestampBasis", default_value = "CREATED_AT")]
    pub rr_timestamp_basis: RRTimestampBasis,
    #[sea_orm(column_name = "brandColor")]
    pub brand_color: Option<String>,
    #[sea_orm(column_name = "darkBrandColor")]
    pub dark_brand_color: Option<String>,
    #[sea_orm(column_name = "bannerUrl")]
    pub banner_url: Option<String>,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<i32>,
    #[sea_orm(column_name = "timeFormat")]
    pub time_format: Option<i32>,
    #[sea_orm(column_name = "timeZone", default_value = "Europe/London")]
    pub time_zone: String,
    #[sea_orm(column_name = "weekStart", default_value = "Sunday")]
    pub week_start: String,
    #[sea_orm(column_name = "isOrganization", default_value = false)]
    pub is_organization: bool,
    #[sea_orm(column_name = "pendingPayment", default_value = false)]
    pub pending_payment: bool,
    #[sea_orm(column_name = "isPlatform", default_value = false)]
    pub is_platform: bool,
    #[sea_orm(column_name = "createdByOAuthClientId")]
    pub created_by_oauth_client_id: Option<String>,
    #[sea_orm(column_name = "smsLockState", default_value = "UNLOCKED")]
    pub sms_lock_state: SMSLockState,
    #[sea_orm(column_name = "smsLockReviewedByAdmin", default_value = false)]
    pub sms_lock_reviewed_by_admin: bool,
    #[sea_orm(column_name = "bookingLimits")]
    pub booking_limits: Option<Json>,
    #[sea_orm(column_name = "includeManagedEventsInLimits", default_value = false)]
    pub include_managed_events_in_limits: bool,
    #[sea_orm(column_name = "autoOptInFeatures", default_value = false)]
    pub auto_opt_in_features: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::ParentId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Parent,
    #[sea_orm(
        belongs_to = "super::platform_oauth_client::Entity",
        from = "Column::CreatedByOAuthClientId",
        to = "super::platform_oauth_client::Column::Id",
        on_delete = "Cascade"
    )]
    CreatedByOAuthClient,
    #[sea_orm(has_many = "super::membership::Entity")]
    Members,
    #[sea_orm(has_many = "super::event_type::Entity")]
    EventTypes,
    #[sea_orm(has_many = "super::verified_number::Entity")]
    VerifiedNumbers,
    #[sea_orm(has_many = "super::verified_email::Entity")]
    VerifiedEmails,
    #[sea_orm(
        has_many = "super::team::Entity",
        from = "Column::Id",
        to = "super::team::Column::ParentId"
    )]
    Children,
    #[sea_orm(has_many = "super::user::Entity")]
    OrgUsers,
    #[sea_orm(has_many = "super::verification_token::Entity")]
    InviteTokens,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::credential::Entity")]
    Credentials,
    #[sea_orm(has_many = "super::access_code::Entity")]
    AccessCodes,
    #[sea_orm(has_many = "super::instant_meeting_token::Entity")]
    InstantMeetingTokens,
    #[sea_orm(has_many = "super::profile::Entity")]
    OrgProfiles,
    #[sea_orm(has_many = "super::dsync_team_group_mapping::Entity")]
    DsyncTeamGroupMapping,
    #[sea_orm(
        has_many = "super::platform_oauth_client::Entity",
        from = "Column::Id",
        to = "super::platform_oauth_client::Column::OrganizationId"
    )]
    PlatformOAuthClient,
    #[sea_orm(has_many = "super::attribute::Entity")]
    Attributes,
    #[sea_orm(has_many = "super::delegation_credential::Entity")]
    DelegationCredentials,
    #[sea_orm(has_many = "super::role::Entity")]
    Roles,
    #[sea_orm(has_many = "super::cal_ai_phone_number::Entity")]
    CalAiPhoneNumbers,
    #[sea_orm(has_many = "super::agent::Entity")]
    Agents,
    #[sea_orm(has_many = "super::booking_report::Entity")]
    BookingReports,
    #[sea_orm(has_many = "super::wrong_assignment_report::Entity")]
    WrongAssignmentReports,
    #[sea_orm(has_many = "super::team_features::Entity")]
    Features,
    #[sea_orm(has_many = "super::internal_note_preset::Entity")]
    InternalNotePresets,
    #[sea_orm(
        has_many = "super::managed_organization::Entity",
        from = "Column::Id",
        to = "super::managed_organization::Column::ManagerOrganizationId"
    )]
    ManagedOrganizations,
    #[sea_orm(has_many = "super::filter_segment::Entity")]
    FilterSegments,
    #[sea_orm(has_many = "super::integration_attribute_sync::Entity")]
    IntegrationAttributeSyncs,
    #[sea_orm(has_many = "super::seat_change_log::Entity")]
    SeatChangeLogs,
    #[sea_orm(has_many = "super::monthly_proration::Entity")]
    MonthlyProrations,
    #[sea_orm(has_one = "super::organization_settings::Entity")]
    OrganizationSettings,
    #[sea_orm(has_one = "super::platform_billing::Entity")]
    PlatformBilling,
    #[sea_orm(has_one = "super::credit_balance::Entity")]
    CreditBalance,
    #[sea_orm(has_one = "super::organization_onboarding::Entity")]
    OrganizationOnboarding,
    #[sea_orm(
        has_one = "super::managed_organization::Entity",
        from = "Column::Id",
        to = "super::managed_organization::Column::ManagedOrganizationId"
    )]
    ManagedOrganization,
    #[sea_orm(has_one = "super::team_billing::Entity")]
    TeamBilling,
    #[sea_orm(has_one = "super::organization_billing::Entity")]
    OrganizationBilling,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Parent.def()
    }
}

impl Related<super::platform_oauth_client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedByOAuthClient.def()
    }
}

impl Related<super::membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventTypes.def()
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

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrgUsers.def()
    }
}

impl Related<super::verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InviteTokens.def()
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

impl Related<super::credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credentials.def()
    }
}

impl Related<super::access_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessCodes.def()
    }
}

impl Related<super::instant_meeting_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InstantMeetingTokens.def()
    }
}

impl Related<super::profile::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrgProfiles.def()
    }
}

impl Related<super::dsync_team_group_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DsyncTeamGroupMapping.def()
    }
}

impl Related<super::attribute::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attributes.def()
    }
}

impl Related<super::delegation_credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DelegationCredentials.def()
    }
}

impl Related<super::role::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Roles.def()
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

impl Related<super::booking_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingReports.def()
    }
}

impl Related<super::wrong_assignment_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WrongAssignmentReports.def()
    }
}

impl Related<super::team_features::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Features.def()
    }
}

impl Related<super::internal_note_preset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InternalNotePresets.def()
    }
}

impl Related<super::managed_organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ManagedOrganizations.def()
    }
}

impl Related<super::filter_segment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FilterSegments.def()
    }
}

impl Related<super::integration_attribute_sync::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IntegrationAttributeSyncs.def()
    }
}

impl Related<super::seat_change_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SeatChangeLogs.def()
    }
}

impl Related<super::monthly_proration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MonthlyProrations.def()
    }
}

impl Related<super::organization_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationSettings.def()
    }
}

impl Related<super::platform_billing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlatformBilling.def()
    }
}

impl Related<super::credit_balance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreditBalance.def()
    }
}

impl Related<super::organization_onboarding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationOnboarding.def()
    }
}

impl Related<super::team_billing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamBilling.def()
    }
}

impl Related<super::organization_billing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationBilling.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
