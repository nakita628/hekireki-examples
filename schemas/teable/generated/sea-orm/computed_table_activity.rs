use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "computed_table_activity")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub table_id: String,
    pub base_id: String,
    pub status: String,
    #[sea_orm(default_value = 0)]
    pub calculating_field_count: i32,
    #[sea_orm(default_value = 0)]
    pub queued_field_count: i32,
    #[sea_orm(default_value = "0")]
    pub estimated_complexity: i64,
    #[sea_orm(default_value = "[]")]
    pub recent_completions: Json,
    #[sea_orm(default_value = "0")]
    pub generation: i64,
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
        if insert && self.updated_at.is_not_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
