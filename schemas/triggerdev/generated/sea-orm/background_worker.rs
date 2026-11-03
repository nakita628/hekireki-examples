use super::run_engine_version::RunEngineVersion;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BackgroundWorker")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(default_value = "V1")]
    pub engine: RunEngineVersion,
    #[sea_orm(column_name = "contentHash")]
    pub content_hash: String,
    #[sea_orm(column_name = "sdkVersion", default_value = "unknown")]
    pub sdk_version: String,
    #[sea_orm(column_name = "cliVersion", default_value = "unknown")]
    pub cli_version: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    pub version: String,
    pub metadata: Json,
    pub runtime: Option<String>,
    #[sea_orm(column_name = "runtimeVersion")]
    pub runtime_version: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "workerGroupId")]
    pub worker_group_id: Option<String>,
    #[sea_orm(column_name = "supportsLazyAttempts", default_value = false)]
    pub supports_lazy_attempts: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::RuntimeEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    RuntimeEnvironment,
    #[sea_orm(
        belongs_to = "super::worker_instance_group::Entity",
        from = "Column::WorkerGroupId",
        to = "super::worker_instance_group::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    WorkerGroup,
    #[sea_orm(has_many = "super::background_worker_task::Entity")]
    Tasks,
    #[sea_orm(has_many = "super::task_run_attempt::Entity")]
    Attempts,
    #[sea_orm(has_many = "super::task_run::Entity")]
    LockedRuns,
    #[sea_orm(has_many = "super::prompt_version::Entity")]
    PromptVersions,
    #[sea_orm(has_many = "super::task_identifier::Entity")]
    TaskIdentifiers,
    #[sea_orm(has_one = "super::worker_deployment::Entity")]
    Deployment,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RuntimeEnvironment.def()
    }
}

impl Related<super::worker_instance_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerGroup.def()
    }
}

impl Related<super::background_worker_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tasks.def()
    }
}

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attempts.def()
    }
}

impl Related<super::task_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LockedRuns.def()
    }
}

impl Related<super::prompt_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptVersions.def()
    }
}

impl Related<super::task_identifier::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskIdentifiers.def()
    }
}

impl Related<super::worker_deployment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Deployment.def()
    }
}

impl Related<super::background_worker_file::Entity> for Entity {
    fn to() -> RelationDef {
        super::background_worker_to_background_worker_file::Relation::BackgroundWorkerFile.def()
    }
    fn via() -> Option<RelationDef> {
        Some(
            super::background_worker_to_background_worker_file::Relation::BackgroundWorker
                .def()
                .rev(),
        )
    }
}

impl Related<super::task_queue::Entity> for Entity {
    fn to() -> RelationDef {
        super::background_worker_to_task_queue::Relation::TaskQueue.def()
    }
    fn via() -> Option<RelationDef> {
        Some(
            super::background_worker_to_task_queue::Relation::BackgroundWorker
                .def()
                .rev(),
        )
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
