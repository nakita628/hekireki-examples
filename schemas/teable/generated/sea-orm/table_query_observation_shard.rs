use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "table_query_observation_shard")]
pub struct Model {
    pub space_id: Option<String>,
    pub base_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub table_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub query_kind: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub shape_hash: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub window_start: DateTimeWithTimeZone,
    #[sea_orm(primary_key, auto_increment = false)]
    pub writer_id: String,
    pub window_size_seconds: i32,
    pub request_count: i32,
    pub slow_count: i32,
    pub timeout_count: i32,
    pub db_error_count: i32,
    pub total_duration_ms: f64,
    pub max_duration_ms: f64,
    pub total_db_duration_ms: Option<f64>,
    pub max_db_duration_ms: Option<f64>,
    pub shape: Json,
    pub sql_diagnostics: Option<Json>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_time: DateTimeWithTimeZone,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub last_modified_time: Option<DateTimeWithTimeZone>,
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
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
