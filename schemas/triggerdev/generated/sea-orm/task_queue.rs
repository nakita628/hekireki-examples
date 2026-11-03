use super::task_queue_concurrency_version::TaskQueueConcurrencyVersion;
use super::task_queue_role::TaskQueueRole;
use super::task_queue_type::TaskQueueType;
use super::task_queue_version::TaskQueueVersion;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskQueue")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    pub name: String,
    #[sea_orm(default_value = "VIRTUAL")]
    pub r#type: TaskQueueType,
    #[sea_orm(default_value = "V1")]
    pub version: TaskQueueVersion,
    #[sea_orm(column_name = "orderableName")]
    pub orderable_name: Option<String>,
    #[sea_orm(column_name = "concurrencyVersion", default_value = "V1")]
    pub concurrency_version: TaskQueueConcurrencyVersion,
    #[sea_orm(default_value = "QUEUE")]
    pub role: TaskQueueRole,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "concurrencyLimit")]
    pub concurrency_limit: Option<i32>,
    #[sea_orm(column_name = "concurrencyLimitOverriddenAt")]
    pub concurrency_limit_overridden_at: Option<DateTime>,
    #[sea_orm(column_name = "concurrencyLimitOverriddenBy")]
    pub concurrency_limit_overridden_by: Option<String>,
    #[sea_orm(column_name = "concurrencyLimitBase")]
    pub concurrency_limit_base: Option<i32>,
    #[sea_orm(
        column_name = "concurrencyLimitOverridePercent",
        column_type = "Decimal(Some((5, 2)))"
    )]
    pub concurrency_limit_override_percent: Option<Decimal>,
    #[sea_orm(column_name = "totalConcurrencyLimit")]
    pub total_concurrency_limit: Option<i32>,
    #[sea_orm(column_name = "totalConcurrencyLimitOverriddenAt")]
    pub total_concurrency_limit_overridden_at: Option<DateTime>,
    #[sea_orm(column_name = "totalConcurrencyLimitOverriddenBy")]
    pub total_concurrency_limit_overridden_by: Option<String>,
    #[sea_orm(column_name = "totalConcurrencyLimitBase")]
    pub total_concurrency_limit_base: Option<i32>,
    #[sea_orm(column_name = "rateLimit")]
    pub rate_limit: Option<Json>,
    #[sea_orm(default_value = false)]
    pub paused: bool,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
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
    #[sea_orm(has_many = "super::task_run_attempt::Entity")]
    Attempts,
    #[sea_orm(has_many = "super::background_worker_task::Entity")]
    Tasks,
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

impl Related<super::task_run_attempt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attempts.def()
    }
}

impl Related<super::background_worker_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tasks.def()
    }
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        super::background_worker_to_task_queue::Relation::BackgroundWorker.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::background_worker_to_task_queue::Relation::TaskQueue.def().rev())
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
