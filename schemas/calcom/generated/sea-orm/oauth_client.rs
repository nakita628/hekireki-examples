use super::oauth_client_status::OAuthClientStatus;
use super::oauth_client_type::OAuthClientType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OAuthClient")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "clientId")]
    pub client_id: String,
    #[sea_orm(column_name = "redirectUri")]
    pub redirect_uri: String,
    #[sea_orm(column_name = "clientSecret")]
    pub client_secret: Option<String>,
    #[sea_orm(column_name = "clientType", default_value = "confidential")]
    pub client_type: OAuthClientType,
    pub name: String,
    pub purpose: Option<String>,
    pub logo: Option<String>,
    #[sea_orm(column_name = "websiteUrl")]
    pub website_url: Option<String>,
    #[sea_orm(column_name = "rejectionReason")]
    pub rejection_reason: Option<String>,
    #[sea_orm(column_name = "isTrusted", default_value = false)]
    pub is_trusted: bool,
    #[sea_orm(default_value = "approved")]
    pub status: OAuthClientStatus,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    User,
    #[sea_orm(has_many = "super::access_code::Entity")]
    AccessCodes,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::access_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccessCodes.def()
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
