use super::project_alert_status::ProjectAlertStatus;
use super::project_alert_type::ProjectAlertType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProjectAlert")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "channelId")]
    pub channel_id: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: ProjectAlertStatus,
    pub r#type: ProjectAlertType,
    #[sea_orm(column_name = "taskRunAttemptId")]
    pub task_run_attempt_id: Option<String>,
    #[sea_orm(column_name = "taskRunId")]
    pub task_run_id: Option<String>,
    #[sea_orm(column_name = "workerDeploymentId")]
    pub worker_deployment_id: Option<String>,
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
        from = "Column::EnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Environment,
    #[sea_orm(
        belongs_to = "super::project_alert_channel::Entity",
        from = "Column::ChannelId",
        to = "super::project_alert_channel::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Channel,
    #[sea_orm(
        belongs_to = "super::worker_deployment::Entity",
        from = "Column::WorkerDeploymentId",
        to = "super::worker_deployment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    WorkerDeployment,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environment.def()
    }
}

impl Related<super::project_alert_channel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Channel.def()
    }
}

impl Related<super::worker_deployment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerDeployment.def()
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
