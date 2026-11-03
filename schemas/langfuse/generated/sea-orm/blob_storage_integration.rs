use super::analytics_integration_export_source::AnalyticsIntegrationExportSource;
use super::blob_storage_export_mode::BlobStorageExportMode;
use super::blob_storage_integration_file_type::BlobStorageIntegrationFileType;
use super::blob_storage_integration_type::BlobStorageIntegrationType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "blob_storage_integrations")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub project_id: String,
    pub r#type: BlobStorageIntegrationType,
    pub bucket_name: String,
    pub prefix: String,
    pub access_key_id: Option<String>,
    pub secret_access_key: Option<String>,
    pub region: String,
    pub endpoint: Option<String>,
    pub force_path_style: bool,
    pub next_sync_at: Option<DateTime>,
    pub last_sync_at: Option<DateTime>,
    pub enabled: bool,
    pub export_frequency: String,
    #[sea_orm(default_value = "CSV")]
    pub file_type: BlobStorageIntegrationFileType,
    #[sea_orm(default_value = "FULL_HISTORY")]
    pub export_mode: BlobStorageExportMode,
    pub export_start_date: Option<DateTime>,
    #[sea_orm(default_value = "TRACES_OBSERVATIONS")]
    pub export_source: AnalyticsIntegrationExportSource,
    pub export_field_groups: Vec<String>,
    #[sea_orm(default_value = true)]
    pub compressed: bool,
    pub export_tuning: Option<Json>,
    pub run_started_at: Option<DateTime>,
    #[sea_orm(default_value = true)]
    pub backfill: bool,
    pub last_error: Option<String>,
    pub last_error_at: Option<DateTime>,
    pub last_failure_notification_sent_at: Option<DateTime>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
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
