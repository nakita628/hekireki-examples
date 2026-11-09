use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "reprocessing_workers")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "String(StringLen::N(64))")]
    pub job_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub worker_index: i32,
    #[sea_orm(column_type = "String(StringLen::N(32))")]
    pub status: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub started_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub completed_at: Option<DateTimeWithTimeZone>,
    pub current_file: Option<String>,
    #[sea_orm(default_value = "0")]
    pub current_line: Option<i64>,
    pub current_ts: Option<String>,
    #[sea_orm(default_value = 0)]
    pub assigned_files: Option<i32>,
    #[sea_orm(default_value = 0)]
    pub processed_files: Option<i32>,
    #[sea_orm(default_value = "0")]
    pub total_lines: Option<i64>,
    #[sea_orm(default_value = "0")]
    pub success_count: Option<i64>,
    #[sea_orm(default_value = "0")]
    pub error_count: Option<i64>,
    #[sea_orm(default_value = "0")]
    pub skipped_count: Option<i64>,
    #[sea_orm(default_value = "0")]
    pub total_bytes: Option<i64>,
    #[sea_orm(default_value = "0")]
    pub processed_bytes: Option<i64>,
    pub error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::reprocessing_jobs::Entity",
        from = "Column::JobId",
        to = "super::reprocessing_jobs::Column::Id",
        on_update = "NoAction",
        on_delete = "Cascade"
    )]
    Job,
}

impl Related<super::reprocessing_jobs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Job.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.updated_at.is_not_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
