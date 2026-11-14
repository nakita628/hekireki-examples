use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LangyConversationTurnProjection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "conversationId")]
    pub conversation_id: String,
    #[sea_orm(column_name = "turnId")]
    pub turn_id: String,
    pub status: String,
    #[sea_orm(column_name = "questionParts")]
    pub question_parts: Json,
    #[sea_orm(column_name = "answerParts")]
    pub answer_parts: Json,
    #[sea_orm(column_name = "toolCalls")]
    pub tool_calls: Json,
    pub plan: Option<Json>,
    pub error: Option<String>,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<f64>,
    #[sea_orm(column_name = "endedAt")]
    pub ended_at: Option<f64>,
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
