use super::alert_type::AlertType;
use super::trigger_action::TriggerAction;
use super::trigger_kind::TriggerKind;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Trigger")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub action: TriggerAction,
    #[sea_orm(column_name = "triggerKind", default_value = "AUTOMATION")]
    pub trigger_kind: TriggerKind,
    #[sea_orm(column_name = "actionParams")]
    pub action_params: Json,
    pub filters: Json,
    #[sea_orm(column_name = "filterQuery")]
    pub filter_query: Option<String>,
    #[sea_orm(column_name = "lastRunAt", default_value = 0)]
    pub last_run_at: f64,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(default_value = true)]
    pub active: bool,
    #[sea_orm(column_name = "pausedReason")]
    pub paused_reason: Option<String>,
    #[sea_orm(column_name = "pausedAt", column_type = "TimestampWithTimeZone")]
    pub paused_at: Option<DateTimeWithTimeZone>,
    pub message: Option<String>,
    #[sea_orm(default_value = false)]
    pub deleted: bool,
    #[sea_orm(column_name = "alertType")]
    pub alert_type: Option<AlertType>,
    #[sea_orm(column_name = "slackTemplateType")]
    pub slack_template_type: Option<String>,
    #[sea_orm(column_name = "slackTemplate")]
    pub slack_template: Option<String>,
    #[sea_orm(column_name = "emailSubjectTemplate")]
    pub email_subject_template: Option<String>,
    #[sea_orm(column_name = "emailBodyTemplate")]
    pub email_body_template: Option<String>,
    #[sea_orm(column_name = "notificationCadence", default_value = "immediate")]
    pub notification_cadence: String,
    #[sea_orm(column_name = "traceDebounceMs", default_value = 30000)]
    pub trace_debounce_ms: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "customGraphId")]
    pub custom_graph_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::custom_graph::Entity",
        from = "Column::CustomGraphId",
        to = "super::custom_graph::Column::Id",
        on_delete = "Cascade"
    )]
    CustomGraph,
    #[sea_orm(has_many = "super::trigger_sent::Entity")]
    TriggerSent,
    #[sea_orm(has_many = "super::webhook_endpoint_delivery::Entity")]
    WebhookDeliveries,
    #[sea_orm(has_one = "super::trigger_latest_evaluation::Entity")]
    LatestEvaluation,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::custom_graph::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomGraph.def()
    }
}

impl Related<super::trigger_sent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TriggerSent.def()
    }
}

impl Related<super::webhook_endpoint_delivery::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WebhookDeliveries.def()
    }
}

impl Related<super::trigger_latest_evaluation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LatestEvaluation.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
