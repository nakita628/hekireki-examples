use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "source_task")]
pub struct Model {
    pub sync_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub task_id: String,
    pub package: String,
    pub version: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub started_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
    pub started_by: Option<Json>,
    pub status: String,
    pub description: Option<String>,
    pub metrics: Option<Json>,
    pub error: Option<String>,
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
        if insert && self.started_at.is_not_set() {
            self.started_at = Set(now.fixed_offset());
        }
        if insert && self.updated_at.is_not_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
