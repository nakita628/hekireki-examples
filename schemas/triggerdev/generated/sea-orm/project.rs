use super::project_version::ProjectVersion;
use super::run_engine_version::RunEngineVersion;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Project")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub slug: String,
    pub name: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "externalRef")]
    pub external_ref: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "initializedAt")]
    pub initialized_at: Option<DateTime>,
    #[sea_orm(column_name = "defaultRuntime")]
    pub default_runtime: Option<String>,
    #[sea_orm(default_value = "V2")]
    pub version: ProjectVersion,
    #[sea_orm(default_value = "V1")]
    pub engine: RunEngineVersion,
    #[sea_orm(column_name = "builderProjectId")]
    pub builder_project_id: Option<String>,
    #[sea_orm(column_name = "defaultWorkerGroupId")]
    pub default_worker_group_id: Option<String>,
    #[sea_orm(column_name = "allowedMasterQueues")]
    pub allowed_worker_queues: Vec<String>,
    #[sea_orm(column_name = "buildSettings")]
    pub build_settings: Option<Json>,
    #[sea_orm(column_name = "onboardingData")]
    pub onboarding_data: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::worker_instance_group::Entity",
        from = "Column::DefaultWorkerGroupId",
        to = "super::worker_instance_group::Column::Id"
    )]
    DefaultWorkerGroup,
    #[sea_orm(
        has_many = "super::worker_instance_group::Entity",
        from = "Column::Id",
        to = "super::worker_instance_group::Column::ProjectId"
    )]
    WorkerGroups,
    #[sea_orm(has_many = "super::worker_instance::Entity")]
    Workers,
    #[sea_orm(has_many = "super::runtime_environment::Entity")]
    Environments,
    #[sea_orm(has_many = "super::background_worker::Entity")]
    BackgroundWorkers,
    #[sea_orm(has_many = "super::background_worker_task::Entity")]
    BackgroundWorkerTasks,
    #[sea_orm(has_many = "super::task_run::Entity")]
    TaskRuns,
    #[sea_orm(has_many = "super::task_run_tag::Entity")]
    RunTags,
    #[sea_orm(has_many = "super::task_queue::Entity")]
    TaskQueues,
    #[sea_orm(has_many = "super::environment_variable::Entity")]
    EnvironmentVariables,
    #[sea_orm(has_many = "super::checkpoint::Entity")]
    Checkpoints,
    #[sea_orm(has_many = "super::worker_deployment::Entity")]
    WorkerDeployment,
    #[sea_orm(has_many = "super::checkpoint_restore_event::Entity")]
    CheckpointRestoreEvent,
    #[sea_orm(has_many = "super::task_schedule::Entity")]
    TaskSchedules,
    #[sea_orm(has_many = "super::project_alert_channel::Entity")]
    AlertChannels,
    #[sea_orm(has_many = "super::project_alert::Entity")]
    Alerts,
    #[sea_orm(has_many = "super::project_alert_storage::Entity")]
    AlertStorages,
    #[sea_orm(has_many = "super::bulk_action_group::Entity")]
    BulkActionGroups,
    #[sea_orm(has_many = "super::background_worker_file::Entity")]
    BackgroundWorkerFile,
    #[sea_orm(has_many = "super::waitpoint::Entity")]
    Waitpoints,
    #[sea_orm(has_many = "super::task_run_waitpoint::Entity")]
    TaskRunWaitpoints,
    #[sea_orm(has_many = "super::task_run_checkpoint::Entity")]
    TaskRunCheckpoints,
    #[sea_orm(has_many = "super::waitpoint_tag::Entity")]
    WaitpointTags,
    #[sea_orm(has_many = "super::organization_project_integration::Entity")]
    OrganizationProjectIntegration,
    #[sea_orm(has_many = "super::customer_query::Entity")]
    CustomerQueries,
    #[sea_orm(has_many = "super::playground_conversation::Entity")]
    PlaygroundConversations,
    #[sea_orm(has_many = "super::task_schedule_instance::Entity")]
    TaskScheduleInstances,
    #[sea_orm(has_many = "super::metrics_dashboard::Entity")]
    MetricsDashboards,
    #[sea_orm(has_many = "super::llm_model::Entity")]
    LlmModels,
    #[sea_orm(has_many = "super::prompt::Entity")]
    Prompts,
    #[sea_orm(has_many = "super::platform_notification::Entity")]
    PlatformNotifications,
    #[sea_orm(has_many = "super::error_group_state::Entity")]
    ErrorGroupStates,
    #[sea_orm(has_many = "super::task_identifier::Entity")]
    TaskIdentifiers,
    #[sea_orm(has_one = "super::connected_github_repository::Entity")]
    ConnectedGithubRepository,
    #[sea_orm(has_one = "super::trusted_deploy_repository::Entity")]
    TrustedDeployRepository,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::worker_instance_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultWorkerGroup.def()
    }
}

impl Related<super::worker_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workers.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environments.def()
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

impl Related<super::task_run_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RunTags.def()
    }
}

impl Related<super::task_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskQueues.def()
    }
}

impl Related<super::environment_variable::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvironmentVariables.def()
    }
}

impl Related<super::checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Checkpoints.def()
    }
}

impl Related<super::worker_deployment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerDeployment.def()
    }
}

impl Related<super::checkpoint_restore_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CheckpointRestoreEvent.def()
    }
}

impl Related<super::task_schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskSchedules.def()
    }
}

impl Related<super::project_alert_channel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AlertChannels.def()
    }
}

impl Related<super::project_alert::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Alerts.def()
    }
}

impl Related<super::project_alert_storage::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AlertStorages.def()
    }
}

impl Related<super::bulk_action_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BulkActionGroups.def()
    }
}

impl Related<super::background_worker_file::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BackgroundWorkerFile.def()
    }
}

impl Related<super::waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Waitpoints.def()
    }
}

impl Related<super::task_run_waitpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunWaitpoints.def()
    }
}

impl Related<super::task_run_checkpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskRunCheckpoints.def()
    }
}

impl Related<super::waitpoint_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WaitpointTags.def()
    }
}

impl Related<super::organization_project_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationProjectIntegration.def()
    }
}

impl Related<super::customer_query::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomerQueries.def()
    }
}

impl Related<super::playground_conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlaygroundConversations.def()
    }
}

impl Related<super::task_schedule_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskScheduleInstances.def()
    }
}

impl Related<super::metrics_dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MetricsDashboards.def()
    }
}

impl Related<super::llm_model::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LlmModels.def()
    }
}

impl Related<super::prompt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Prompts.def()
    }
}

impl Related<super::platform_notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlatformNotifications.def()
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

impl Related<super::connected_github_repository::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConnectedGithubRepository.def()
    }
}

impl Related<super::trusted_deploy_repository::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TrustedDeployRepository.def()
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
