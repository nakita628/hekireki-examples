use super::provider::Provider;
use super::role::Role;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(column_name = "accessToken")]
    pub access_token: Option<String>,
    #[sea_orm(column_name = "authChallenge")]
    pub auth_challenge: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "ANONYMOUS")]
    pub provider: Provider,
    #[sea_orm(default_value = "USER")]
    pub role: Role,
    #[sea_orm(column_name = "thirdPartyId")]
    pub third_party_id: Option<String>,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        has_many = "super::access::Entity",
        from = "Column::Id",
        to = "super::access::Column::GranteeUserId"
    )]
    AccessesGet,
    #[sea_orm(
        has_many = "super::access::Entity",
        from = "Column::Id",
        to = "super::access::Column::UserId"
    )]
    AccessesGive,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::order::Entity")]
    Activities,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::auth_device::Entity")]
    AuthDevices,
    #[sea_orm(has_many = "super::subscription::Entity")]
    Subscriptions,
    #[sea_orm(has_many = "super::tag::Entity")]
    Tags,
    #[sea_orm(has_many = "super::symbol_profile::Entity")]
    SymbolProfile,
    #[sea_orm(has_one = "super::analytics::Entity")]
    Analytics,
    #[sea_orm(has_one = "super::settings::Entity")]
    Settings,
}

impl Related<super::access::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessesGet.def()
    }
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::order::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Activities.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::auth_device::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AuthDevices.def()
    }
}

impl Related<super::subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Subscriptions.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::symbol_profile::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SymbolProfile.def()
    }
}

impl Related<super::analytics::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Analytics.def()
    }
}

impl Related<super::settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Settings.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
