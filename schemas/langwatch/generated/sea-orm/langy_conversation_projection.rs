use super::langy_projection_title_source::LangyProjectionTitleSource;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LangyConversationProjection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "conversationId")]
    pub conversation_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    pub title: Option<String>,
    #[sea_orm(column_name = "titleSource")]
    pub title_source: LangyProjectionTitleSource,
    #[sea_orm(default_value = "interactive")]
    pub origin: String,
    pub status: String,
    #[sea_orm(column_name = "isShared", default_value = false)]
    pub is_shared: bool,
    #[sea_orm(column_name = "sharedAt")]
    pub shared_at: Option<f64>,
    #[sea_orm(column_name = "sharedById")]
    pub shared_by_id: Option<String>,
    #[sea_orm(column_name = "messageCount", default_value = 0)]
    pub message_count: i32,
    #[sea_orm(column_name = "lastActivityAt")]
    pub last_activity_at: Option<f64>,
    #[sea_orm(column_name = "currentTurnId")]
    pub current_turn_id: Option<String>,
    #[sea_orm(column_name = "lastError")]
    pub last_error: Option<String>,
    #[sea_orm(column_name = "lastModel")]
    pub last_model: Option<String>,
    #[sea_orm(column_name = "pendingHandoffToken")]
    pub pending_handoff_token: Option<String>,
    #[sea_orm(column_name = "pendingHandoffTurnId")]
    pub pending_handoff_turn_id: Option<String>,
    #[sea_orm(column_name = "runToken")]
    pub run_token: Option<String>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<f64>,
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
