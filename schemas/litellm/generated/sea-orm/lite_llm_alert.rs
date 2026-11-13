use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_Alert")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i64,
    #[sea_orm(unique)]
    #[sea_orm(column_type = "String(StringLen::N(64))")]
    pub detection_key: String,
    #[sea_orm(column_type = "String(StringLen::N(32))")]
    pub r#type: String,
    #[sea_orm(column_type = "String(StringLen::N(16))")]
    pub resource_type: String,
    #[sea_orm(column_type = "String(StringLen::N(1024))")]
    pub resource_id: String,
    #[sea_orm(column_type = "String(StringLen::N(16))", default_value = "open")]
    pub status: String,
    pub window_start: DateTime,
    pub window_end: DateTime,
    pub evidence: Json,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_type = "String(StringLen::N(1024))")]
    pub updated_by: Option<String>,
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
