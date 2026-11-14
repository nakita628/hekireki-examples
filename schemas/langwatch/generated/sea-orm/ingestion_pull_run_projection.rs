use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "IngestionPullRunProjection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "sourceId")]
    pub source_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    pub cron: Option<String>,
    pub cursor: Option<String>,
    #[sea_orm(column_name = "lastRunAt")]
    pub last_run_at: Option<f64>,
    #[sea_orm(column_name = "lastRunOutcome")]
    pub last_run_outcome: Option<String>,
    #[sea_orm(column_name = "lastRunEventCount", default_value = 0)]
    pub last_run_event_count: i32,
    #[sea_orm(column_name = "lastRunError")]
    pub last_run_error: Option<String>,
    #[sea_orm(column_name = "lastRunErrorCode")]
    pub last_run_error_code: Option<String>,
    #[sea_orm(column_name = "consecutiveErrors", default_value = 0)]
    pub consecutive_errors: i32,
    #[sea_orm(column_name = "lastSuccessAt")]
    pub last_success_at: Option<f64>,
    #[sea_orm(column_name = "lastRunScheduledFor")]
    pub last_run_scheduled_for: Option<f64>,
    #[sea_orm(column_name = "lastReadThroughAt")]
    pub last_read_through_at: Option<f64>,
    #[sea_orm(column_name = "lastRunCompleteness")]
    pub last_run_completeness: Option<String>,
    #[sea_orm(column_name = "lastAgentsListingAt")]
    pub last_agents_listing_at: Option<f64>,
    #[sea_orm(column_name = "lastAgentsListingOutcome")]
    pub last_agents_listing_outcome: Option<String>,
    #[sea_orm(column_name = "lastAgentsListingCount")]
    pub last_agents_listing_count: Option<i32>,
    #[sea_orm(column_name = "lastAgentsListingReason")]
    pub last_agents_listing_reason: Option<String>,
    #[sea_orm(column_name = "lastAgentsListingStatus")]
    pub last_agents_listing_status: Option<i32>,
    #[sea_orm(column_name = "lastPeopleListingAt")]
    pub last_people_listing_at: Option<f64>,
    #[sea_orm(column_name = "lastPeopleListingOutcome")]
    pub last_people_listing_outcome: Option<String>,
    #[sea_orm(column_name = "lastPeopleDirectoryCount")]
    pub last_people_directory_count: Option<i32>,
    #[sea_orm(column_name = "lastPeopleWithheldCount")]
    pub last_people_withheld_count: Option<i32>,
    #[sea_orm(column_name = "lastPeopleListingReason")]
    pub last_people_listing_reason: Option<String>,
    #[sea_orm(column_name = "lastPeopleListingStatus")]
    pub last_people_listing_status: Option<i32>,
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
