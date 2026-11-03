use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "IntegrationDeployment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "integrationName")]
    pub integration_name: String,
    #[sea_orm(column_name = "integrationDeploymentId")]
    pub integration_deployment_id: String,
    #[sea_orm(column_name = "commitSHA")]
    pub commit_sha: String,
    #[sea_orm(column_name = "deploymentId")]
    pub deployment_id: Option<String>,
    pub status: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::worker_deployment::Entity",
        from = "Column::DeploymentId",
        to = "super::worker_deployment::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    WorkerDeployment,
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
