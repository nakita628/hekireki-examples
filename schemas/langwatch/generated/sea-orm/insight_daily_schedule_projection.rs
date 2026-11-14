use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "InsightDailyScheduleProjection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(column_name = "boardKind")]
    pub board_kind: String,
    #[sea_orm(column_name = "boardId")]
    pub board_id: String,
    #[sea_orm(column_name = "boardName")]
    pub board_name: String,
    #[sea_orm(default_value = "undecided")]
    pub state: String,
    #[sea_orm(column_name = "offReason")]
    pub off_reason: Option<String>,
    pub hour: Option<i32>,
    pub timezone: Option<String>,
    #[sea_orm(column_name = "maxInsights")]
    pub max_insights: Option<i32>,
    #[sea_orm(column_name = "lastRunId")]
    pub last_run_id: Option<String>,
    #[sea_orm(column_name = "lastRunAt")]
    pub last_run_at: Option<f64>,
    #[sea_orm(column_name = "lastRunOutcome")]
    pub last_run_outcome: Option<String>,
    #[sea_orm(column_name = "lastRunReason")]
    pub last_run_reason: Option<String>,
    #[sea_orm(column_name = "lastRunFiled")]
    pub last_run_filed: Option<i32>,
    #[sea_orm(column_name = "lastRunRenewed")]
    pub last_run_renewed: Option<i32>,
    #[sea_orm(column_name = "lastRunConversationId")]
    pub last_run_conversation_id: Option<String>,
    #[sea_orm(column_name = "consecutiveSkips", default_value = 0)]
    pub consecutive_skips: i32,
    #[sea_orm(column_name = "skipStreakSlot")]
    pub skip_streak_slot: Option<f64>,
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
