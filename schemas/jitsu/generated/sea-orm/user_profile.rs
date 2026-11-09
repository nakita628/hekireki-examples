use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "UserProfile")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub email: String,
    #[sea_orm(default_value = false)]
    pub admin: Option<bool>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "loginProvider")]
    pub login_provider: String,
    #[sea_orm(column_name = "externalUsername")]
    pub external_username: Option<String>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::user_api_token::Entity")]
    Tokens,
    #[sea_orm(has_many = "super::workspace_access::Entity")]
    WorkspaceAccess,
    #[sea_orm(has_many = "super::user_preferences::Entity")]
    UserPreferences,
    #[sea_orm(has_many = "super::workspace_user_properties::Entity")]
    WorkspaceUserProperties,
    #[sea_orm(has_one = "super::user_password::Entity")]
    Password,
}

impl Related<super::user_api_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tokens.def()
    }
}

impl Related<super::workspace_access::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceAccess.def()
    }
}

impl Related<super::user_preferences::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserPreferences.def()
    }
}

impl Related<super::workspace_user_properties::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceUserProperties.def()
    }
}

impl Related<super::user_password::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Password.def()
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
