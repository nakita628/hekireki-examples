use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "InsightProjection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub title: String,
    pub body: String,
    pub tone: String,
    pub topic: Option<String>,
    #[sea_orm(column_name = "validDays")]
    pub valid_days: i32,
    pub lwql: Option<String>,
    #[sea_orm(column_name = "sourceConversationId")]
    pub source_conversation_id: Option<String>,
    #[sea_orm(column_name = "sourceMessageId")]
    pub source_message_id: Option<String>,
    #[sea_orm(column_name = "filedByUserId")]
    pub filed_by_user_id: Option<String>,
    #[sea_orm(column_name = "filedAt")]
    pub filed_at: f64,
    #[sea_orm(column_name = "renewedAt")]
    pub renewed_at: Option<f64>,
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
    #[sea_orm(column_name = "boardId")]
    pub board_id: Option<String>,
    #[sea_orm(column_name = "boardName")]
    pub board_name: Option<String>,
    #[sea_orm(column_name = "widgetId")]
    pub widget_id: Option<String>,
    #[sea_orm(column_name = "widgetName")]
    pub widget_name: Option<String>,
    #[sea_orm(column_name = "filedVia", default_value = "chat")]
    pub filed_via: String,
    #[sea_orm(column_name = "replayStart")]
    pub replay_start: Option<f64>,
    #[sea_orm(column_name = "replayEnd")]
    pub replay_end: Option<f64>,
    #[sea_orm(column_name = "replayGranularitySeconds")]
    pub replay_granularity_seconds: Option<i32>,
    #[sea_orm(column_name = "replayContext")]
    pub replay_context: Option<Json>,
    #[sea_orm(column_name = "ownerUserId")]
    pub owner_user_id: Option<String>,
    #[sea_orm(column_name = "findingKey")]
    pub finding_key: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
