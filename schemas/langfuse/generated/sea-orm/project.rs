use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "projects")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub org_id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub deleted_at: Option<DateTime>,
    pub name: String,
    pub retention_days: Option<i32>,
    #[sea_orm(default_value = false)]
    pub has_traces: bool,
    pub metadata: Option<Json>,
    pub home_dashboard_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::dashboard::Entity",
        from = "Column::HomeDashboardId",
        to = "super::dashboard::Column::Id",
        on_delete = "SetNull"
    )]
    HomeDashboard,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrgId",
        to = "super::organization::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_many = "super::project_membership::Entity")]
    ProjectMembers,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::role_assignment::Entity")]
    RoleAssignments,
    #[sea_orm(has_many = "super::dataset::Entity")]
    Dataset,
    #[sea_orm(has_many = "super::membership_invitation::Entity")]
    Invitations,
    #[sea_orm(has_many = "super::trace_session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::prompt::Entity")]
    Prompt,
    #[sea_orm(has_many = "super::skill::Entity")]
    Skill,
    #[sea_orm(has_many = "super::skill_file::Entity")]
    SkillFile,
    #[sea_orm(has_many = "super::skill_blob::Entity")]
    SkillBlob,
    #[sea_orm(has_many = "super::model::Entity")]
    Model,
    #[sea_orm(has_many = "super::eval_template::Entity")]
    EvalTemplate,
    #[sea_orm(has_many = "super::job_configuration::Entity")]
    JobConfiguration,
    #[sea_orm(has_many = "super::job_execution::Entity")]
    JobExecution,
    #[sea_orm(has_many = "super::evaluator::Entity")]
    Evaluator,
    #[sea_orm(has_many = "super::evaluation_rule::Entity")]
    EvaluationRule,
    #[sea_orm(has_many = "super::evaluation_rule_evaluator_assignment::Entity")]
    EvaluationRuleEvaluatorAssignment,
    #[sea_orm(has_many = "super::llm_api_keys::Entity")]
    LlmApiKeys,
    #[sea_orm(has_many = "super::posthog_integration::Entity")]
    PosthogIntegration,
    #[sea_orm(has_many = "super::mixpanel_integration::Entity")]
    MixpanelIntegration,
    #[sea_orm(has_many = "super::blob_storage_integration::Entity")]
    BlobStorageIntegration,
    #[sea_orm(has_many = "super::web_callout_endpoint::Entity")]
    WebCalloutEndpoints,
    #[sea_orm(has_many = "super::score_config::Entity")]
    ScoreConfig,
    #[sea_orm(has_many = "super::batch_export::Entity")]
    BatchExport,
    #[sea_orm(has_many = "super::batch_action::Entity")]
    BatchAction,
    #[sea_orm(has_many = "super::comment::Entity")]
    Comment,
    #[sea_orm(has_many = "super::comment_reaction::Entity")]
    CommentReactions,
    #[sea_orm(has_many = "super::annotation_queue::Entity")]
    AnnotationQueue,
    #[sea_orm(has_many = "super::annotation_queue_item::Entity")]
    AnnotationQueueItem,
    #[sea_orm(has_many = "super::media::Entity")]
    Media,
    #[sea_orm(has_many = "super::prompt_dependency::Entity")]
    PromptDependency,
    #[sea_orm(has_many = "super::llm_schema::Entity")]
    LlmSchema,
    #[sea_orm(has_many = "super::llm_tool::Entity")]
    LlmTool,
    #[sea_orm(has_many = "super::prompt_protected_labels::Entity")]
    PromptProtectedLabels,
    #[sea_orm(
        has_many = "super::dashboard::Entity",
        from = "Column::Id",
        to = "super::dashboard::Column::ProjectId"
    )]
    Dashboard,
    #[sea_orm(has_many = "super::dashboard_widget::Entity")]
    DashboardWidget,
    #[sea_orm(has_many = "super::table_view_preset::Entity")]
    TableViewPreset,
    #[sea_orm(has_many = "super::session_view::Entity")]
    SessionViews,
    #[sea_orm(has_many = "super::session_view_pin_override::Entity")]
    SessionViewPinOverrides,
    #[sea_orm(has_many = "super::action::Entity")]
    Actions,
    #[sea_orm(has_many = "super::trigger::Entity")]
    Triggers,
    #[sea_orm(has_many = "super::automation_execution::Entity")]
    AutomationExecutions,
    #[sea_orm(has_many = "super::automation::Entity")]
    Automation,
    #[sea_orm(has_many = "super::monitor::Entity")]
    Monitors,
    #[sea_orm(has_many = "super::default_llm_model::Entity")]
    DefaultLlmModel,
    #[sea_orm(has_many = "super::price::Entity")]
    Price,
    #[sea_orm(has_many = "super::pending_deletion::Entity")]
    PendingDeletion,
    #[sea_orm(has_many = "super::annotation_queue_assignment::Entity")]
    AnnotationQueueAssignment,
    #[sea_orm(has_many = "super::notification_preference::Entity")]
    NotificationPreference,
    #[sea_orm(has_many = "super::default_view::Entity")]
    DefaultView,
    #[sea_orm(has_many = "super::in_app_agent_conversation::Entity")]
    InAppAgentConversation,
    #[sea_orm(has_many = "super::in_app_agent_event::Entity")]
    InAppAgentEvent,
    #[sea_orm(has_many = "super::in_app_agent_run::Entity")]
    InAppAgentRun,
    #[sea_orm(has_many = "super::in_app_agent_pending_tool_approval::Entity")]
    InAppAgentPendingToolApproval,
    #[sea_orm(has_many = "super::topic_clustering_run::Entity")]
    TopicClusteringRuns,
    #[sea_orm(has_many = "super::gateway_config::Entity")]
    GatewayConfigs,
    #[sea_orm(has_one = "super::tracing_readiness_settings::Entity")]
    TracingReadinessSettings,
    #[sea_orm(has_one = "super::external_media_storage_integration::Entity")]
    ExternalMediaStorageIntegration,
    #[sea_orm(has_one = "super::topic_config::Entity")]
    TopicConfig,
    #[sea_orm(has_one = "super::slack_integration::Entity")]
    SlackIntegration,
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HomeDashboard.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::project_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProjectMembers.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::role_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleAssignments.def()
    }
}

impl Related<super::dataset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dataset.def()
    }
}

impl Related<super::membership_invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::trace_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::prompt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Prompt.def()
    }
}

impl Related<super::skill::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Skill.def()
    }
}

impl Related<super::skill_file::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SkillFile.def()
    }
}

impl Related<super::skill_blob::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SkillBlob.def()
    }
}

impl Related<super::model::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Model.def()
    }
}

impl Related<super::eval_template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvalTemplate.def()
    }
}

impl Related<super::job_configuration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::JobConfiguration.def()
    }
}

impl Related<super::job_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::JobExecution.def()
    }
}

impl Related<super::evaluator::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Evaluator.def()
    }
}

impl Related<super::evaluation_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvaluationRule.def()
    }
}

impl Related<super::evaluation_rule_evaluator_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvaluationRuleEvaluatorAssignment.def()
    }
}

impl Related<super::llm_api_keys::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LlmApiKeys.def()
    }
}

impl Related<super::posthog_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PosthogIntegration.def()
    }
}

impl Related<super::mixpanel_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MixpanelIntegration.def()
    }
}

impl Related<super::blob_storage_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BlobStorageIntegration.def()
    }
}

impl Related<super::web_callout_endpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WebCalloutEndpoints.def()
    }
}

impl Related<super::score_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScoreConfig.def()
    }
}

impl Related<super::batch_export::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchExport.def()
    }
}

impl Related<super::batch_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BatchAction.def()
    }
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comment.def()
    }
}

impl Related<super::comment_reaction::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CommentReactions.def()
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

impl Related<super::media::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Media.def()
    }
}

impl Related<super::prompt_dependency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptDependency.def()
    }
}

impl Related<super::llm_schema::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LlmSchema.def()
    }
}

impl Related<super::llm_tool::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LlmTool.def()
    }
}

impl Related<super::prompt_protected_labels::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptProtectedLabels.def()
    }
}

impl Related<super::dashboard_widget::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DashboardWidget.def()
    }
}

impl Related<super::table_view_preset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TableViewPreset.def()
    }
}

impl Related<super::session_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionViews.def()
    }
}

impl Related<super::session_view_pin_override::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionViewPinOverrides.def()
    }
}

impl Related<super::action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Actions.def()
    }
}

impl Related<super::trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Triggers.def()
    }
}

impl Related<super::automation_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AutomationExecutions.def()
    }
}

impl Related<super::automation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Automation.def()
    }
}

impl Related<super::monitor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Monitors.def()
    }
}

impl Related<super::default_llm_model::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultLlmModel.def()
    }
}

impl Related<super::price::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Price.def()
    }
}

impl Related<super::pending_deletion::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PendingDeletion.def()
    }
}

impl Related<super::annotation_queue_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueueAssignment.def()
    }
}

impl Related<super::notification_preference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationPreference.def()
    }
}

impl Related<super::default_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultView.def()
    }
}

impl Related<super::in_app_agent_conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InAppAgentConversation.def()
    }
}

impl Related<super::in_app_agent_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InAppAgentEvent.def()
    }
}

impl Related<super::in_app_agent_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InAppAgentRun.def()
    }
}

impl Related<super::in_app_agent_pending_tool_approval::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InAppAgentPendingToolApproval.def()
    }
}

impl Related<super::topic_clustering_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TopicClusteringRuns.def()
    }
}

impl Related<super::gateway_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayConfigs.def()
    }
}

impl Related<super::tracing_readiness_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TracingReadinessSettings.def()
    }
}

impl Related<super::external_media_storage_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExternalMediaStorageIntegration.def()
    }
}

impl Related<super::topic_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TopicConfig.def()
    }
}

impl Related<super::slack_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SlackIntegration.def()
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
