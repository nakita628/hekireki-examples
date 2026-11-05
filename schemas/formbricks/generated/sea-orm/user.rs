use super::bootstrap_admin_marker::BootstrapAdminMarker;
use super::identity_provider::IdentityProvider;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub name: String,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(default_value = false)]
    pub email_verified: bool,
    #[sea_orm(column_name = "twoFactorSecret")]
    pub two_factor_secret: Option<String>,
    #[sea_orm(column_name = "twoFactorEnabled", default_value = false)]
    pub two_factor_enabled: bool,
    #[sea_orm(column_name = "backupCodes")]
    pub backup_codes: Option<String>,
    pub password: Option<String>,
    #[sea_orm(column_name = "identityProvider", default_value = "email")]
    pub identity_provider: IdentityProvider,
    #[sea_orm(column_name = "identityProviderAccountId")]
    pub identity_provider_account_id: Option<String>,
    #[sea_orm(column_name = "groupId")]
    pub group_id: Option<String>,
    #[sea_orm(column_name = "notificationSettings", default_value = "{}")]
    pub notification_settings: Json,
    #[sea_orm(default_value = "en-US")]
    pub locale: String,
    #[sea_orm(column_name = "lastLoginAt")]
    pub last_login_at: Option<DateTime>,
    #[sea_orm(column_name = "isActive", default_value = true)]
    pub is_active: bool,
    #[sea_orm(column_name = "reactivatedAt")]
    pub reactivated_at: Option<DateTime>,
    #[sea_orm(column_name = "lastActiveAt")]
    pub last_active_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "isBootstrapAdmin")]
    pub is_bootstrap_admin: Option<BootstrapAdminMarker>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::membership::Entity")]
    Memberships,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::oauth_client::Entity")]
    OauthClients,
    #[sea_orm(has_many = "super::oauth_access_token::Entity")]
    OauthAccessTokens,
    #[sea_orm(has_many = "super::oauth_refresh_token::Entity")]
    OauthRefreshTokens,
    #[sea_orm(has_many = "super::oauth_consent::Entity")]
    OauthConsents,
    #[sea_orm(
        has_many = "super::invite::Entity",
        from = "Column::Id",
        to = "super::invite::Column::CreatorId"
    )]
    InvitesCreated,
    #[sea_orm(
        has_many = "super::invite::Entity",
        from = "Column::Id",
        to = "super::invite::Column::AcceptorId"
    )]
    InvitesAccepted,
    #[sea_orm(
        has_many = "super::survey::Entity",
        from = "Column::Id",
        to = "super::survey::Column::CreatedBy"
    )]
    Surveys,
    #[sea_orm(
        has_many = "super::survey::Entity",
        from = "Column::Id",
        to = "super::survey::Column::OwnerId"
    )]
    OwnedSurveys,
    #[sea_orm(has_many = "super::chart::Entity")]
    Charts,
    #[sea_orm(has_many = "super::dashboard::Entity")]
    Dashboards,
    #[sea_orm(has_many = "super::creative::Entity")]
    Creatives,
    #[sea_orm(has_many = "super::feedback_source::Entity")]
    FeedbackSources,
    #[sea_orm(has_many = "super::team_user::Entity")]
    TeamUsers,
    #[sea_orm(has_many = "super::workflow::Entity")]
    Workflows,
    #[sea_orm(has_many = "super::workflow_version::Entity")]
    WorkflowVersionsPublished,
    #[sea_orm(has_many = "super::retention_policy::Entity")]
    RetentionPoliciesUpdated,
    #[sea_orm(
        has_many = "super::retention_exemption::Entity",
        from = "Column::Id",
        to = "super::retention_exemption::Column::CreatedById"
    )]
    ExemptionsCreated,
    #[sea_orm(
        has_many = "super::retention_exemption::Entity",
        from = "Column::Id",
        to = "super::retention_exemption::Column::RevokedById"
    )]
    ExemptionsRevoked,
    #[sea_orm(has_many = "super::retention_notice::Entity")]
    RetentionNotices,
    #[sea_orm(has_many = "super::attachment_export::Entity")]
    AttachmentExports,
    #[sea_orm(has_one = "super::two_factor::Entity")]
    TwoFactor,
    #[sea_orm(has_one = "super::password_reset_token::Entity")]
    PasswordResetToken,
}

impl Related<super::membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Memberships.def()
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

impl Related<super::oauth_client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthClients.def()
    }
}

impl Related<super::oauth_access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthAccessTokens.def()
    }
}

impl Related<super::oauth_refresh_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthRefreshTokens.def()
    }
}

impl Related<super::oauth_consent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OauthConsents.def()
    }
}

impl Related<super::invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InvitesCreated.def()
    }
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Surveys.def()
    }
}

impl Related<super::chart::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Charts.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboards.def()
    }
}

impl Related<super::creative::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Creatives.def()
    }
}

impl Related<super::feedback_source::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FeedbackSources.def()
    }
}

impl Related<super::team_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamUsers.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflows.def()
    }
}

impl Related<super::workflow_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkflowVersionsPublished.def()
    }
}

impl Related<super::retention_policy::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionPoliciesUpdated.def()
    }
}

impl Related<super::retention_exemption::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExemptionsCreated.def()
    }
}

impl Related<super::retention_notice::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionNotices.def()
    }
}

impl Related<super::attachment_export::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttachmentExports.def()
    }
}

impl Related<super::two_factor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactor.def()
    }
}

impl Related<super::password_reset_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PasswordResetToken.def()
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
