use super::attachment_export_status::AttachmentExportStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AttachmentExport")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: String,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "requestedById")]
    pub requested_by_id: Option<String>,
    #[sea_orm(default_value = "queued")]
    pub status: AttachmentExportStatus,
    #[sea_orm(column_name = "filterCriteria")]
    pub filter_criteria: Option<Json>,
    #[sea_orm(column_name = "timeZone")]
    pub time_zone: String,
    #[sea_orm(column_name = "dispatchCount", default_value = 0)]
    pub dispatch_count: i32,
    #[sea_orm(column_name = "fileCount")]
    pub file_count: i32,
    #[sea_orm(column_name = "responseCount")]
    pub response_count: i32,
    #[sea_orm(column_name = "includedFileCount")]
    pub included_file_count: Option<i32>,
    #[sea_orm(column_name = "totalBytes")]
    pub total_bytes: Option<i64>,
    #[sea_orm(default_value = false)]
    pub truncated: bool,
    #[sea_orm(column_name = "storageKey")]
    pub storage_key: Option<String>,
    #[sea_orm(column_name = "errorCode")]
    pub error_code: Option<String>,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: Option<DateTime>,
    #[sea_orm(column_name = "finishedAt")]
    pub finished_at: Option<DateTime>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::survey::Entity",
        from = "(Column::SurveyId, Column::WorkspaceId)",
        to = "(super::survey::Column::Id, super::survey::Column::WorkspaceId)",
        on_delete = "Cascade"
    )]
    Survey,
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::RequestedById",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    RequestedBy,
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Survey.def()
    }
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RequestedBy.def()
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
