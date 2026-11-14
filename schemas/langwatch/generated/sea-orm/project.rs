use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Project")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub slug: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "apiKey")]
    pub api_key: String,
    #[sea_orm(column_name = "legacyApiKeyLastUsedAt")]
    pub legacy_api_key_last_used_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "lwqlKey")]
    pub lwql_key: String,
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    pub language: String,
    pub framework: String,
    #[sea_orm(default_value = "application")]
    pub kind: String,
    #[sea_orm(column_name = "aggregateRule")]
    pub aggregate_rule: Option<Json>,
    #[sea_orm(column_name = "firstMessage", default_value = false)]
    pub first_message: bool,
    #[sea_orm(default_value = false)]
    pub integrated: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "userLinkTemplate")]
    pub user_link_template: Option<String>,
    #[sea_orm(column_name = "traceSharingEnabled", default_value = true)]
    pub trace_sharing_enabled: bool,
    #[sea_orm(column_name = "presenceEnabled", default_value = true)]
    pub presence_enabled: bool,
    #[sea_orm(column_name = "s3Endpoint")]
    pub s3_endpoint: Option<String>,
    #[sea_orm(column_name = "s3AccessKeyId")]
    pub s3_access_key_id: Option<String>,
    #[sea_orm(column_name = "s3SecretAccessKey")]
    pub s3_secret_access_key: Option<String>,
    #[sea_orm(column_name = "s3Bucket")]
    pub s3_bucket: Option<String>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "isPersonal", default_value = false)]
    pub is_personal: bool,
    #[sea_orm(column_name = "ownerUserId", column_type = "String(StringLen::N(255))")]
    pub owner_user_id: Option<String>,
    #[sea_orm(column_name = "personalFeatures", default_value = "{}")]
    pub personal_features: Json,
    #[sea_orm(column_name = "departmentId")]
    pub department_id: Option<String>,
    #[sea_orm(column_name = "langyEgressAllowlist")]
    pub langy_egress_allowlist: Option<Json>,
    #[sea_orm(column_name = "lastCodingAgentSessionAt")]
    pub last_coding_agent_session_at: Option<DateTime>,
    #[sea_orm(column_name = "lastCodingAgentPullRequestAt")]
    pub last_coding_agent_pull_request_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerUserId",
        to = "super::user::Column::Id"
    )]
    OwnerUser,
    #[sea_orm(has_many = "super::monitor::Entity")]
    Checks,
    #[sea_orm(has_many = "super::cost::Entity")]
    Costs,
    #[sea_orm(has_many = "super::topic::Entity")]
    Topics,
    #[sea_orm(has_many = "super::dataset::Entity")]
    Datasets,
    #[sea_orm(has_many = "super::dataset_record::Entity")]
    DatasetRecords,
    #[sea_orm(has_many = "super::custom_graph::Entity")]
    CustomGraphs,
    #[sea_orm(has_many = "super::dashboard::Entity")]
    Dashboards,
    #[sea_orm(has_many = "super::batch_evaluation::Entity")]
    BatchEvaluations,
    #[sea_orm(has_many = "super::trigger::Entity")]
    Triggers,
    #[sea_orm(has_many = "super::experiment::Entity")]
    Experiments,
    #[sea_orm(has_many = "super::annotation::Entity")]
    Annotations,
    #[sea_orm(has_many = "super::project_secret::Entity")]
    ProjectSecrets,
    #[sea_orm(has_many = "super::trigger_sent::Entity")]
    TriggerSent,
    #[sea_orm(has_many = "super::webhook_endpoint_delivery::Entity")]
    WebhookDeliveries,
    #[sea_orm(has_many = "super::annotation_score::Entity")]
    AnnotationScores,
    #[sea_orm(has_many = "super::share_link::Entity")]
    ShareLinks,
    #[sea_orm(has_many = "super::workflow::Entity")]
    Workflows,
    #[sea_orm(has_many = "super::workflow_version::Entity")]
    WorkflowVersion,
    #[sea_orm(has_many = "super::annotation_queue::Entity")]
    AnnotationQueue,
    #[sea_orm(has_many = "super::annotation_queue_item::Entity")]
    AnnotationQueueItem,
    #[sea_orm(has_many = "super::llm_prompt_config::Entity")]
    LlmPromptConfigs,
    #[sea_orm(has_many = "super::analytics::Entity")]
    Analytics,
    #[sea_orm(has_many = "super::notification::Entity")]
    Notifications,
    #[sea_orm(has_many = "super::agent::Entity")]
    Agents,
    #[sea_orm(has_many = "super::evaluator::Entity")]
    Evaluators,
    #[sea_orm(has_many = "super::scenario::Entity")]
    Scenarios,
    #[sea_orm(has_many = "super::simulation_suite::Entity")]
    SuiteConfigurations,
    #[sea_orm(has_many = "super::saved_view::Entity")]
    SavedViews,
    #[sea_orm(has_many = "super::gateway_guardrail::Entity")]
    GatewayGuardrails,
    #[sea_orm(has_many = "super::gateway_change_event::Entity")]
    GatewayChangeEvents,
    #[sea_orm(has_many = "super::pinned_trace::Entity")]
    PinnedTraces,
    #[sea_orm(has_many = "super::trace_edit_overlay::Entity")]
    TraceEditOverlays,
    #[sea_orm(has_many = "super::email_suppression::Entity")]
    EmailSuppressions,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OwnerUser.def()
    }
}

impl Related<super::monitor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checks.def()
    }
}

impl Related<super::cost::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Costs.def()
    }
}

impl Related<super::topic::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Topics.def()
    }
}

impl Related<super::dataset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Datasets.def()
    }
}

impl Related<super::dataset_record::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DatasetRecords.def()
    }
}

impl Related<super::custom_graph::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomGraphs.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboards.def()
    }
}

impl Related<super::batch_evaluation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchEvaluations.def()
    }
}

impl Related<super::trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Triggers.def()
    }
}

impl Related<super::experiment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Experiments.def()
    }
}

impl Related<super::annotation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Annotations.def()
    }
}

impl Related<super::project_secret::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProjectSecrets.def()
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

impl Related<super::annotation_score::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationScores.def()
    }
}

impl Related<super::share_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareLinks.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflows.def()
    }
}

impl Related<super::workflow_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkflowVersion.def()
    }
}

impl Related<super::annotation_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueue.def()
    }
}

impl Related<super::annotation_queue_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueueItem.def()
    }
}

impl Related<super::llm_prompt_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LlmPromptConfigs.def()
    }
}

impl Related<super::analytics::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Analytics.def()
    }
}

impl Related<super::notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notifications.def()
    }
}

impl Related<super::agent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agents.def()
    }
}

impl Related<super::evaluator::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Evaluators.def()
    }
}

impl Related<super::scenario::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scenarios.def()
    }
}

impl Related<super::simulation_suite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SuiteConfigurations.def()
    }
}

impl Related<super::saved_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SavedViews.def()
    }
}

impl Related<super::gateway_guardrail::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayGuardrails.def()
    }
}

impl Related<super::gateway_change_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayChangeEvents.def()
    }
}

impl Related<super::pinned_trace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PinnedTraces.def()
    }
}

impl Related<super::trace_edit_overlay::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TraceEditOverlays.def()
    }
}

impl Related<super::email_suppression::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailSuppressions.def()
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
