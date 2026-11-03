use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_background_tasks")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub tool_call_id: String,
    pub tool_name: String,
    pub agent_id: String,
    pub run_id: String,
    pub thread_id: Option<String>,
    pub resource_id: Option<String>,
    pub status: String,
    pub args: Json,
    pub result: Option<Json>,
    pub error: Option<Json>,
    pub suspend_payload: Option<Json>,
    pub retry_count: i32,
    pub max_retries: i32,
    pub timeout_ms: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<DateTime>,
    #[sea_orm(column_name = "suspendedAt")]
    pub suspended_at: Option<DateTime>,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "startedAtZ", column_type = "TimestampWithTimeZone")]
    pub started_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "suspendedAtZ", column_type = "TimestampWithTimeZone")]
    pub suspended_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "completedAtZ", column_type = "TimestampWithTimeZone")]
    pub completed_at_z: Option<DateTimeWithTimeZone>,
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
        if insert && self.created_at_z.is_not_set() {
            self.created_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.started_at_z.is_not_set() {
            self.started_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.suspended_at_z.is_not_set() {
            self.suspended_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.completed_at_z.is_not_set() {
            self.completed_at_z = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
