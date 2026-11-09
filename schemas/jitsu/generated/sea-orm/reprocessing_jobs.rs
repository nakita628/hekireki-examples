use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "reprocessing_jobs")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "String(StringLen::N(64))")]
    pub id: String,
    pub config: Json,
    #[sea_orm(column_type = "String(StringLen::N(32))")]
    pub status: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub started_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub completed_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub k8s_job_name: Option<String>,
    #[sea_orm(default_value = 0)]
    pub total_files: Option<i32>,
    #[sea_orm(default_value = 0)]
    pub total_workers: Option<i32>,
    pub error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::reprocessing_workers::Entity")]
    ReprocessingWorkers,
}

impl Related<super::reprocessing_workers::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReprocessingWorkers.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
