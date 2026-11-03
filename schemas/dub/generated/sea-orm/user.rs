use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: Option<String>,
    #[sea_orm(unique)]
    pub email: Option<String>,
    #[sea_orm(column_name = "emailVerified")]
    pub email_verified: Option<DateTime>,
    pub image: Option<String>,
    #[sea_orm(column_name = "isMachine", default_value = false)]
    pub is_machine: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "passwordHash")]
    pub password_hash: Option<String>,
    #[sea_orm(column_name = "invalidLoginAttempts", default_value = 0)]
    pub invalid_login_attempts: i32,
    #[sea_orm(column_name = "lockedAt")]
    pub locked_at: Option<DateTime>,
    #[sea_orm(column_name = "defaultWorkspace")]
    pub default_workspace: Option<String>,
    pub source: Option<String>,
    #[sea_orm(column_name = "sentMail", default_value = false)]
    pub sent_mail: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::project_users::Entity")]
    Projects,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::dashboard::Entity")]
    Dashboards,
    #[sea_orm(has_many = "super::token::Entity")]
    Tokens,
    #[sea_orm(has_many = "super::restricted_token::Entity")]
    RestrictedTokens,
    #[sea_orm(has_many = "super::oauth_code::Entity")]
    OAuthCodes,
    #[sea_orm(has_many = "super::integration::Entity")]
    Integrations,
    #[sea_orm(has_many = "super::installed_integration::Entity")]
    InstalledIntegrations,
    #[sea_orm(has_many = "super::folder_user::Entity")]
    Folders,
    #[sea_orm(has_many = "super::folder_access_request::Entity")]
    FolderAccessRequests,
    #[sea_orm(has_many = "super::utm_template::Entity")]
    UtmTemplates,
    #[sea_orm(has_one = "super::user_notification_preferences::Entity")]
    NotificationPreferences,
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

impl Related<super::project_users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboards.def()
    }
}

impl Related<super::token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tokens.def()
    }
}

impl Related<super::restricted_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RestrictedTokens.def()
    }
}

impl Related<super::oauth_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OAuthCodes.def()
    }
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integrations.def()
    }
}

impl Related<super::installed_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InstalledIntegrations.def()
    }
}

impl Related<super::folder_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::folder_access_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FolderAccessRequests.def()
    }
}

impl Related<super::utm_template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UtmTemplates.def()
    }
}

impl Related<super::user_notification_preferences::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationPreferences.def()
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
