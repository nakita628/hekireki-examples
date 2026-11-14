use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TopicClusteringRunProjection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "lastRequestedAt")]
    pub last_requested_at: Option<f64>,
    #[sea_orm(column_name = "lastRequestTrigger")]
    pub last_request_trigger: Option<String>,
    #[sea_orm(column_name = "lastRunAt")]
    pub last_run_at: Option<f64>,
    #[sea_orm(column_name = "lastRunOutcome")]
    pub last_run_outcome: Option<String>,
    #[sea_orm(column_name = "lastRunMode")]
    pub last_run_mode: Option<String>,
    #[sea_orm(column_name = "lastRunSkippedReason")]
    pub last_run_skipped_reason: Option<String>,
    #[sea_orm(column_name = "lastRunError")]
    pub last_run_error: Option<String>,
    #[sea_orm(column_name = "lastRunErrorCode")]
    pub last_run_error_code: Option<String>,
    #[sea_orm(column_name = "lastRunErrorUserActionable", default_value = false)]
    pub last_run_error_user_actionable: bool,
    #[sea_orm(column_name = "lastRunTracesProcessed", default_value = 0)]
    pub last_run_traces_processed: i32,
    #[sea_orm(column_name = "lastRunTopicsCount", default_value = 0)]
    pub last_run_topics_count: i32,
    #[sea_orm(column_name = "lastRunSubtopicsCount", default_value = 0)]
    pub last_run_subtopics_count: i32,
    #[sea_orm(column_name = "lastRunPages", default_value = 0)]
    pub last_run_pages: i32,
    #[sea_orm(column_name = "inProgressRunId")]
    pub in_progress_run_id: Option<String>,
    #[sea_orm(column_name = "inProgressTraces", default_value = 0)]
    pub in_progress_traces: i32,
    #[sea_orm(column_name = "inProgressPages", default_value = 0)]
    pub in_progress_pages: i32,
    #[sea_orm(column_name = "inProgressStartedAt")]
    pub in_progress_started_at: Option<f64>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: f64,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: f64,
    #[sea_orm(column_name = "occurredAt")]
    pub occurred_at: f64,
    #[sea_orm(column_name = "acceptedAt")]
    pub accepted_at: f64,
    #[sea_orm(column_name = "lastEventId")]
    pub last_event_id: String,
    #[sea_orm(column_name = "projectionVersion")]
    pub projection_version: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
