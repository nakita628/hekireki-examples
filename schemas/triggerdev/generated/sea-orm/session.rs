use super::runtime_environment_type::RuntimeEnvironmentType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Session")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    pub r#type: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "environmentType")]
    pub environment_type: RuntimeEnvironmentType,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "taskIdentifier")]
    pub task_identifier: String,
    #[sea_orm(column_name = "triggerConfig")]
    pub trigger_config: Json,
    #[sea_orm(column_name = "isTest", default_value = false)]
    pub is_test: bool,
    pub tags: Vec<String>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "currentRunId")]
    pub current_run_id: Option<String>,
    #[sea_orm(column_name = "currentRunVersion", default_value = 0)]
    pub current_run_version: i32,
    #[sea_orm(column_name = "closedAt")]
    pub closed_at: Option<DateTime>,
    #[sea_orm(column_name = "closedReason")]
    pub closed_reason: Option<String>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "streamBasinName")]
    pub stream_basin_name: Option<String>,
    #[sea_orm(column_name = "chatSnapshotStoragePath")]
    pub chat_snapshot_storage_path: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::session_run::Entity")]
    Runs,
}

impl Related<super::session_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Runs.def()
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
