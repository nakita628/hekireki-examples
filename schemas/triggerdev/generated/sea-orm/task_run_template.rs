use super::task_trigger_source::TaskTriggerSource;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskRunTemplate")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "taskSlug")]
    pub task_slug: String,
    #[sea_orm(column_name = "triggerSource")]
    pub trigger_source: TaskTriggerSource,
    pub label: String,
    pub payload: Option<String>,
    #[sea_orm(column_name = "payloadType", default_value = "application/json")]
    pub payload_type: String,
    pub metadata: Option<String>,
    #[sea_orm(column_name = "metadataType", default_value = "application/json")]
    pub metadata_type: String,
    pub queue: Option<String>,
    #[sea_orm(column_name = "concurrencyKey")]
    pub concurrency_key: Option<String>,
    #[sea_orm(column_name = "ttlSeconds")]
    pub ttl_seconds: Option<i32>,
    #[sea_orm(column_name = "maxAttempts")]
    pub max_attempts: Option<i32>,
    #[sea_orm(column_name = "maxDurationSeconds")]
    pub max_duration_seconds: Option<i32>,
    pub tags: Vec<String>,
    #[sea_orm(column_name = "machinePreset")]
    pub machine_preset: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
