use super::signup_intent::SignupIntent;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: Option<String>,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "emailVerified", default_value = false)]
    pub email_verified: Option<bool>,
    pub image: Option<String>,
    #[sea_orm(column_name = "completedOnboardingAt")]
    pub completed_onboarding_at: Option<DateTime>,
    #[sea_orm(column_name = "signupIntent")]
    pub signup_intent: Option<SignupIntent>,
    #[sea_orm(column_name = "onboardingAnswers")]
    pub onboarding_answers: Option<Json>,
    #[sea_orm(column_name = "lastLogin")]
    pub last_login: Option<DateTime>,
    pub utms: Option<Json>,
    #[sea_orm(column_name = "errorMessages")]
    pub error_messages: Option<Json>,
    #[sea_orm(column_name = "announcementDismissedAt")]
    pub announcement_dismissed_at: Option<DateTime>,
    #[sea_orm(column_name = "dismissedHints")]
    pub dismissed_hints: Vec<String>,
    #[sea_orm(column_name = "surveyFeatures")]
    pub survey_features: Vec<String>,
    #[sea_orm(column_name = "surveyRole")]
    pub survey_role: Option<String>,
    #[sea_orm(column_name = "surveyGoal")]
    pub survey_goal: Option<String>,
    #[sea_orm(column_name = "surveyCompanySize")]
    pub survey_company_size: Option<i32>,
    #[sea_orm(column_name = "surveySource")]
    pub survey_source: Option<String>,
    #[sea_orm(column_name = "surveyImprovements")]
    pub survey_improvements: Option<String>,
    #[sea_orm(column_name = "mcpServerEnabled", default_value = false)]
    pub mcp_server_enabled: bool,
    #[sea_orm(column_name = "mcpTokenVersion", default_value = 0)]
    pub mcp_token_version: i32,
    #[sea_orm(column_name = "emailOtpEnabled", default_value = false)]
    pub email_otp_enabled: bool,
    #[sea_orm(column_name = "emailOtpVersion", default_value = 0)]
    pub email_otp_version: i32,
    #[sea_orm(column_name = "aiProvider")]
    pub ai_provider: Option<String>,
    #[sea_orm(column_name = "aiModel")]
    pub ai_model: Option<String>,
    #[sea_orm(column_name = "aiApiKey")]
    pub ai_api_key: Option<String>,
    #[sea_orm(column_name = "decisionModelEnabled")]
    pub decision_model_enabled: Option<bool>,
    #[sea_orm(column_name = "webhookSecret")]
    pub webhook_secret: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "referralCode")]
    pub referral_code: Option<String>,
    #[sea_orm(column_name = "premiumId")]
    pub premium_id: Option<String>,
    #[sea_orm(column_name = "premiumAdminId")]
    pub premium_admin_id: Option<String>,
    #[sea_orm(column_name = "scimAccessDisabled", default_value = false)]
    pub scim_access_disabled: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::premium::Entity",
        from = "Column::PremiumId",
        to = "super::premium::Column::Id"
    )]
    Premium,
    #[sea_orm(
        belongs_to = "super::premium::Entity",
        from = "Column::PremiumAdminId",
        to = "super::premium::Column::Id"
    )]
    PremiumAdmin,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::email_account::Entity")]
    EmailAccounts,
    #[sea_orm(
        has_many = "super::referral::Entity",
        from = "Column::Id",
        to = "super::referral::Column::ReferrerUserId"
    )]
    ReferralsMade,
    #[sea_orm(has_many = "super::mobile_push_token::Entity")]
    MobilePushTokens,
    #[sea_orm(has_many = "super::oauth_client::Entity")]
    OauthClientRecords,
    #[sea_orm(has_many = "super::oauth_refresh_token::Entity")]
    OauthRefreshTokenRecords,
    #[sea_orm(has_many = "super::oauth_access_token::Entity")]
    OauthAccessTokenRecords,
    #[sea_orm(has_many = "super::oauth_consent::Entity")]
    OauthConsentRecords,
    #[sea_orm(has_many = "super::sso_provider::Entity")]
    SsoProviders,
    #[sea_orm(has_many = "super::scim_identity_tombstone::Entity")]
    ScimIdentityTombstones,
    #[sea_orm(has_many = "super::scim_user::Entity")]
    ScimUsers,
    #[sea_orm(has_many = "super::scim_projection_grant::Entity")]
    ScimProjectionGrants,
    #[sea_orm(has_many = "super::scim_identity_link::Entity")]
    ScimIdentityLinks,
    #[sea_orm(
        has_one = "super::referral::Entity",
        from = "Column::Id",
        to = "super::referral::Column::ReferredUserId"
    )]
    ReferralReceived,
    #[sea_orm(has_one = "super::scim_subject::Entity")]
    ScimSubject,
}

impl Related<super::premium::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Premium.def()
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

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccounts.def()
    }
}

impl Related<super::referral::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReferralsMade.def()
    }
}

impl Related<super::mobile_push_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MobilePushTokens.def()
    }
}

impl Related<super::oauth_client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthClientRecords.def()
    }
}

impl Related<super::oauth_refresh_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthRefreshTokenRecords.def()
    }
}

impl Related<super::oauth_access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthAccessTokenRecords.def()
    }
}

impl Related<super::oauth_consent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthConsentRecords.def()
    }
}

impl Related<super::sso_provider::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SsoProviders.def()
    }
}

impl Related<super::scim_identity_tombstone::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimIdentityTombstones.def()
    }
}

impl Related<super::scim_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimUsers.def()
    }
}

impl Related<super::scim_projection_grant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimProjectionGrants.def()
    }
}

impl Related<super::scim_identity_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimIdentityLinks.def()
    }
}

impl Related<super::scim_subject::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimSubject.def()
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
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
