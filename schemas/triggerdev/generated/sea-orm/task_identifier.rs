use super::task_trigger_source::TaskTriggerSource;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskIdentifier")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub slug: String,
    #[sea_orm(column_name = "currentTriggerSource", default_value = "STANDARD")]
    pub current_trigger_source: TaskTriggerSource,
    #[sea_orm(column_name = "currentWorkerId")]
    pub current_worker_id: Option<String>,
    #[sea_orm(column_name = "firstSeenAt")]
    pub first_seen_at: DateTime,
    #[sea_orm(column_name = "lastSeenAt")]
    pub last_seen_at: DateTime,
    #[sea_orm(column_name = "isInLatestDeployment", default_value = true)]
    pub is_in_latest_deployment: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::RuntimeEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    RuntimeEnvironment,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::background_worker::Entity",
        from = "Column::CurrentWorkerId",
        to = "super::background_worker::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    CurrentWorker,
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RuntimeEnvironment.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CurrentWorker.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.first_seen_at.is_not_set() {
            self.first_seen_at = Set(now.naive_utc());
        }
        if insert && self.last_seen_at.is_not_set() {
            self.last_seen_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
