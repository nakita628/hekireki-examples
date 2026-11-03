use super::worker_deployment_status::WorkerDeploymentStatus;
use super::worker_deployment_type::WorkerDeploymentType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WorkerDeployment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "contentHash")]
    pub content_hash: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "shortCode")]
    pub short_code: String,
    pub version: String,
    pub runtime: Option<String>,
    #[sea_orm(column_name = "runtimeVersion")]
    pub runtime_version: Option<String>,
    #[sea_orm(column_name = "cliVersion")]
    pub cli_version: Option<String>,
    #[sea_orm(column_name = "imageReference")]
    pub image_reference: Option<String>,
    #[sea_orm(column_name = "imagePlatform", default_value = "linux/amd64")]
    pub image_platform: String,
    #[sea_orm(column_name = "templateKey")]
    pub template_key: Option<String>,
    #[sea_orm(column_name = "indexObjectKey")]
    pub index_object_key: Option<String>,
    #[sea_orm(column_name = "indexSha256")]
    pub index_sha256: Option<String>,
    #[sea_orm(column_name = "indexFormatVersion")]
    pub index_format_version: Option<i32>,
    #[sea_orm(column_name = "externalBuildData")]
    pub external_build_data: Option<Json>,
    #[sea_orm(column_name = "buildServerMetadata")]
    pub build_server_metadata: Option<Json>,
    #[sea_orm(column_name = "buildEnvVars")]
    pub build_env_vars: Option<Json>,
    #[sea_orm(default_value = "PENDING")]
    pub status: WorkerDeploymentStatus,
    #[sea_orm(default_value = "V1")]
    pub r#type: WorkerDeploymentType,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "workerId")]
    pub worker_id: Option<String>,
    #[sea_orm(column_name = "triggeredById")]
    pub triggered_by_id: Option<String>,
    #[sea_orm(column_name = "triggeredVia")]
    pub triggered_via: Option<String>,
    #[sea_orm(column_name = "commitSHA")]
    pub commit_sha: Option<String>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<DateTime>,
    #[sea_orm(column_name = "installedAt")]
    pub installed_at: Option<DateTime>,
    #[sea_orm(column_name = "builtAt")]
    pub built_at: Option<DateTime>,
    #[sea_orm(column_name = "deployedAt")]
    pub deployed_at: Option<DateTime>,
    #[sea_orm(column_name = "failedAt")]
    pub failed_at: Option<DateTime>,
    #[sea_orm(column_name = "errorData")]
    pub error_data: Option<Json>,
    #[sea_orm(column_name = "canceledAt")]
    pub canceled_at: Option<DateTime>,
    #[sea_orm(column_name = "canceledReason")]
    pub canceled_reason: Option<String>,
    pub git: Option<Json>,
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
        belongs_to = "super::background_worker::Entity",
        from = "Column::WorkerId",
        to = "super::background_worker::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Worker,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::TriggeredById",
        to = "super::user::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    TriggeredBy,
    #[sea_orm(has_many = "super::worker_deployment_promotion::Entity")]
    Promotions,
    #[sea_orm(has_many = "super::project_alert::Entity")]
    Alerts,
    #[sea_orm(has_many = "super::worker_instance::Entity")]
    WorkerInstance,
    #[sea_orm(has_many = "super::integration_deployment::Entity")]
    IntegrationDeployments,
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

impl Related<super::background_worker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Worker.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TriggeredBy.def()
    }
}

impl Related<super::worker_deployment_promotion::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Promotions.def()
    }
}

impl Related<super::project_alert::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Alerts.def()
    }
}

impl Related<super::worker_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerInstance.def()
    }
}

impl Related<super::integration_deployment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IntegrationDeployments.def()
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
