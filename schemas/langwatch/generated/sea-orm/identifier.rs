use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Identifier")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub provider: String,
    pub value: Option<String>,
    pub domain: Option<String>,
    #[sea_orm(column_name = "identifierHash")]
    pub identifier_hash: Option<String>,
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<String>,
    #[sea_orm(column_name = "providerId")]
    pub provider_id: Option<String>,
    pub issuer: Option<String>,
    #[sea_orm(column_name = "providerAccountId")]
    pub provider_account_id: Option<String>,
    pub state: String,
    #[sea_orm(column_name = "connectionId")]
    pub connection_id: Option<String>,
    #[sea_orm(column_name = "verifiedAt")]
    pub verified_at: Option<DateTime>,
    #[sea_orm(column_name = "attachedAt")]
    pub attached_at: DateTime,
    #[sea_orm(column_name = "detachedAt")]
    pub detached_at: Option<DateTime>,
    #[sea_orm(column_name = "lastUsedAt")]
    pub last_used_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
