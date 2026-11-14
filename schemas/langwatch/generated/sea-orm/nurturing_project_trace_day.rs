use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "NurturingProjectTraceDay")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub day: String,
    #[sea_orm(column_name = "traceCount", default_value = 0)]
    pub trace_count: i32,
    #[sea_orm(column_name = "errorCount", default_value = 0)]
    pub error_count: i32,
    #[sea_orm(column_name = "lastTraceAt")]
    pub last_trace_at: Option<DateTime>,
    #[sea_orm(column_name = "lastFailedTraceAt")]
    pub last_failed_trace_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
