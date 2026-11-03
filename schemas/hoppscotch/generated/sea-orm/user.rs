use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub uid: String,
    #[sea_orm(column_name = "displayName")]
    pub display_name: Option<String>,
    #[sea_orm(unique)]
    pub email: Option<String>,
    #[sea_orm(column_name = "photoURL")]
    pub photo_url: Option<String>,
    #[sea_orm(column_name = "isAdmin", default_value = false)]
    pub is_admin: bool,
    #[sea_orm(column_name = "refreshToken")]
    pub refresh_token: Option<String>,
    #[sea_orm(column_name = "currentRESTSession")]
    pub current_restsession: Option<Json>,
    #[sea_orm(column_name = "currentGQLSession")]
    pub current_gqlsession: Option<Json>,
    #[sea_orm(column_name = "createdOn", column_type = "TimestampWithTimeZone")]
    pub created_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "lastLoggedOn", column_type = "TimestampWithTimeZone")]
    pub last_logged_on: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "lastActiveOn", column_type = "TimestampWithTimeZone")]
    pub last_active_on: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::account::Entity")]
    ProviderAccounts,
    #[sea_orm(has_many = "super::invited_users::Entity")]
    InvitedUsers,
    #[sea_orm(has_many = "super::mock_server::Entity")]
    MockServers,
    #[sea_orm(has_many = "super::personal_access_token::Entity")]
    PersonalAccessTokens,
    #[sea_orm(has_many = "super::shortcode::Entity")]
    Shortcodes,
    #[sea_orm(has_many = "super::user_collection::Entity")]
    UserCollections,
    #[sea_orm(has_many = "super::user_environment::Entity")]
    Environments,
    #[sea_orm(has_many = "super::user_history::Entity")]
    History,
    #[sea_orm(has_many = "super::user_request::Entity")]
    UserRequests,
    #[sea_orm(has_many = "super::verification_token::Entity")]
    VerificationTokens,
    #[sea_orm(has_one = "super::user_settings::Entity")]
    Settings,
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProviderAccounts.def()
    }
}

impl Related<super::invited_users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InvitedUsers.def()
    }
}

impl Related<super::mock_server::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MockServers.def()
    }
}

impl Related<super::personal_access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PersonalAccessTokens.def()
    }
}

impl Related<super::shortcode::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Shortcodes.def()
    }
}

impl Related<super::user_collection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserCollections.def()
    }
}

impl Related<super::user_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environments.def()
    }
}

impl Related<super::user_history::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::History.def()
    }
}

impl Related<super::user_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserRequests.def()
    }
}

impl Related<super::verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerificationTokens.def()
    }
}

impl Related<super::user_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Settings.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_on.is_not_set() {
            self.created_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
