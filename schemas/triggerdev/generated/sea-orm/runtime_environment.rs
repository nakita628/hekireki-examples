use super::environment_pause_source::EnvironmentPauseSource;
use super::runtime_environment_type::RuntimeEnvironmentType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "RuntimeEnvironment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub slug: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "apiKey")]
    pub api_key: String,
    #[sea_orm(column_name = "rootApiKeyHiddenAt")]
    pub root_api_key_hidden_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "pkApiKey")]
    pub pk_api_key: String,
    #[sea_orm(default_value = "DEVELOPMENT")]
    pub r#type: RuntimeEnvironmentType,
    #[sea_orm(column_name = "isBranchableEnvironment", default_value = false)]
    pub is_branchable_environment: bool,
    #[sea_orm(column_name = "branchName")]
    pub branch_name: Option<String>,
    #[sea_orm(column_name = "parentEnvironmentId")]
    pub parent_environment_id: Option<String>,
    #[sea_orm(column_name = "previewAutoArchiveAfterDays")]
    pub preview_auto_archive_after_days: Option<i32>,
    #[sea_orm(column_name = "previewAutoArchiveExcludedBranches")]
    pub preview_auto_archive_excluded_branches: Vec<String>,
    #[sea_orm(column_name = "previewAutoArchiveNextCheckAt")]
    pub preview_auto_archive_next_check_at: Option<DateTime>,
    #[sea_orm(column_name = "previewAutoArchiveCursorCreatedAt")]
    pub preview_auto_archive_cursor_created_at: Option<DateTime>,
    #[sea_orm(column_name = "previewAutoArchiveCursorId")]
    pub preview_auto_archive_cursor_id: Option<String>,
    pub git: Option<Json>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    pub shortcode: String,
    #[sea_orm(column_name = "maximumConcurrencyLimit", default_value = 5)]
    pub maximum_concurrency_limit: i32,
    #[sea_orm(
        column_name = "concurrencyLimitBurstFactor",
        column_type = "Decimal(Some((4, 2)))",
        default_value = 2
    )]
    pub concurrency_limit_burst_factor: Decimal,
    #[sea_orm(default_value = false)]
    pub paused: bool,
    #[sea_orm(column_name = "pauseSource")]
    pub pause_source: Option<EnvironmentPauseSource>,
    #[sea_orm(column_name = "autoEnableInternalSources", default_value = true)]
    pub auto_enable_internal_sources: bool,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "orgMemberId")]
    pub org_member_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "builtInEnvironmentVariableOverrides")]
    pub built_in_environment_variable_overrides: Option<Json>,
    #[sea_orm(column_name = "tunnelId")]
    pub tunnel_id: Option<String>,
    #[sea_orm(column_name = "currentSessionId")]
    pub current_session_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::ParentEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    ParentEnvironment,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::org_member::Entity",
        from = "Column::OrgMemberId",
        to = "super::org_member::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    OrgMember,
    #[sea_orm(
        belongs_to = "super::runtime_environment_session::Entity",
        from = "Column::CurrentSessionId",
        to = "super::runtime_environment_session::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    CurrentSession,
    #[sea_orm(
        has_many = "super::runtime_environment::Entity",
        from = "Column::Id",
        to = "super::runtime_environment::Column::ParentEnvironmentId"
    )]
    ChildEnvironments,
    #[sea_orm(has_many = "super::background_worker::Entity")]
    BackgroundWorkers,
    #[sea_orm(has_many = "super::background_worker_task::Entity")]
    BackgroundWorkerTasks,
    #[sea_orm(has_many = "super::task_run::Entity")]
    TaskRuns,
    #[sea_orm(has_many = "super::task_queue::Entity")]
    TaskQueues,
    #[sea_orm(has_many = "super::environment_variable_value::Entity")]
    EnvironmentVariableValues,
    #[sea_orm(has_many = "super::checkpoint::Entity")]
    Checkpoints,
    #[sea_orm(has_many = "super::worker_deployment::Entity")]
    WorkerDeployments,
    #[sea_orm(has_many = "super::worker_deployment_promotion::Entity")]
    WorkerDeploymentPromotions,
    #[sea_orm(has_many = "super::task_run_attempt::Entity")]
    TaskRunAttempts,
    #[sea_orm(has_many = "super::checkpoint_restore_event::Entity")]
    CheckpointRestoreEvent,
    #[sea_orm(has_many = "super::task_schedule_instance::Entity")]
    TaskScheduleInstances,
    #[sea_orm(has_many = "super::project_alert::Entity")]
    Alerts,
    #[sea_orm(
        has_many = "super::runtime_environment_session::Entity",
        from = "Column::Id",
        to = "super::runtime_environment_session::Column::EnvironmentId"
    )]
    Sessions,
    #[sea_orm(has_many = "super::task_run_number_counter::Entity")]
    TaskRunNumberCounter,
    #[sea_orm(has_many = "super::task_run_checkpoint::Entity")]
    TaskRunCheckpoints,
    #[sea_orm(has_many = "super::waitpoint::Entity")]
    Waitpoints,
    #[sea_orm(has_many = "super::worker_instance::Entity")]
    WorkerInstances,
    #[sea_orm(has_many = "super::waitpoint_tag::Entity")]
    WaitpointTags,
    #[sea_orm(has_many = "super::bulk_action_group::Entity")]
    BulkActionGroup,
    #[sea_orm(has_many = "super::customer_query::Entity")]
    CustomerQueries,
    #[sea_orm(has_many = "super::prompt::Entity")]
    Prompts,
    #[sea_orm(has_many = "super::playground_conversation::Entity")]
    PlaygroundConversations,
    #[sea_orm(has_many = "super::error_group_state::Entity")]
    ErrorGroupStates,
    #[sea_orm(has_many = "super::task_identifier::Entity")]
    TaskIdentifiers,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::revoked_api_key::Entity")]
    RevokedApiKeys,
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ParentEnvironment.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::org_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrgMember.def()
    }
}

impl Related<super::runtime_environment_session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CurrentSession.def()
    }
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundWorkers.def()
    }
}

impl Related<super::background_worker_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundWorkerTasks.def()
    }
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRuns.def()
    }
}

impl Related<super::task_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskQueues.def()
    }
}

impl Related<super::environment_variable_value::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvironmentVariableValues.def()
    }
}

impl Related<super::checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checkpoints.def()
    }
}

impl Related<super::worker_deployment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerDeployments.def()
    }
}

impl Related<super::worker_deployment_promotion::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerDeploymentPromotions.def()
    }
}

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunAttempts.def()
    }
}

impl Related<super::checkpoint_restore_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CheckpointRestoreEvent.def()
    }
}

impl Related<super::task_schedule_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskScheduleInstances.def()
    }
}

impl Related<super::project_alert::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Alerts.def()
    }
}

impl Related<super::task_run_number_counter::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunNumberCounter.def()
    }
}

impl Related<super::task_run_checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunCheckpoints.def()
    }
}

impl Related<super::waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Waitpoints.def()
    }
}

impl Related<super::worker_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerInstances.def()
    }
}

impl Related<super::waitpoint_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WaitpointTags.def()
    }
}

impl Related<super::bulk_action_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BulkActionGroup.def()
    }
}

impl Related<super::customer_query::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomerQueries.def()
    }
}

impl Related<super::prompt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Prompts.def()
    }
}

impl Related<super::playground_conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlaygroundConversations.def()
    }
}

impl Related<super::error_group_state::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ErrorGroupStates.def()
    }
}

impl Related<super::task_identifier::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskIdentifiers.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::revoked_api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RevokedApiKeys.def()
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
