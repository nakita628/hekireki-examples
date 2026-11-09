use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AuditLog")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub timestamp: DateTime,
    pub r#type: String,
    #[sea_orm(default_value = "info")]
    pub severity: Option<String>,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<String>,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: Option<String>,
    #[sea_orm(column_name = "objectId")]
    pub object_id: Option<String>,
    pub changes: Option<Json>,
    #[sea_orm(column_name = "authType")]
    pub auth_type: Option<String>,
    #[sea_orm(column_name = "tokenId")]
    pub token_id: Option<String>,
    pub ip: Option<String>,
    #[sea_orm(column_name = "requestHeaders")]
    pub request_headers: Option<Json>,
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
        if insert && self.timestamp.is_not_set() {
            self.timestamp = Set(now.naive_utc());
        }
        Ok(self)
    }
}
