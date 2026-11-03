use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "schema_operation")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub r#type: String,
    pub status: String,
    pub phase: String,
    pub resource_type: String,
    pub resource_id: String,
    pub base_id: Option<String>,
    pub table_id: Option<String>,
    #[sea_orm(unique)]
    pub idempotency_key: String,
    pub payload: Option<Json>,
    pub result: Option<Json>,
    #[sea_orm(default_value = 0)]
    pub attempts: i32,
    #[sea_orm(default_value = 8)]
    pub max_attempts: i32,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub next_run_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub locked_at: Option<DateTimeWithTimeZone>,
    pub locked_by: Option<String>,
    pub last_error: Option<String>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_time: DateTimeWithTimeZone,
    pub created_by: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub last_modified_time: Option<DateTimeWithTimeZone>,
    pub last_modified_by: Option<String>,
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
        if insert && self.next_run_at.is_not_set() {
            self.next_run_at = Set(now.fixed_offset());
        }
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.fixed_offset());
        }
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
