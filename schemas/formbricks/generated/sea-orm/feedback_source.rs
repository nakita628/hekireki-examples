use super::feedback_source_element_scope::FeedbackSourceElementScope;
use super::feedback_source_import_mode::FeedbackSourceImportMode;
use super::feedback_source_status::FeedbackSourceStatus;
use super::feedback_source_type::FeedbackSourceType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "FeedbackSource")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub name: String,
    pub r#type: FeedbackSourceType,
    #[sea_orm(default_value = "active")]
    pub status: FeedbackSourceStatus,
    #[sea_orm(default_value = "completedOnly")]
    pub import_mode: FeedbackSourceImportMode,
    #[sea_orm(column_name = "elementScope", default_value = "specific")]
    pub element_scope: FeedbackSourceElementScope,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "feedbackDirectoryId")]
    pub feedback_directory_id: String,
    pub last_sync_at: Option<DateTime>,
    pub created_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
    #[sea_orm(
        belongs_to = "super::feedback_directory::Entity",
        from = "Column::FeedbackDirectoryId",
        to = "super::feedback_directory::Column::Id",
        on_delete = "Cascade"
    )]
    FeedbackDirectory,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    Creator,
    #[sea_orm(
        belongs_to = "super::feedback_directory_workspace::Entity",
        from = "(Column::FeedbackDirectoryId, Column::WorkspaceId)",
        to = "(super::feedback_directory_workspace::Column::FeedbackDirectoryId, super::feedback_directory_workspace::Column::WorkspaceId)",
        on_update = "Cascade",
        on_delete = "NoAction"
    )]
    FeedbackDirectoryWorkspace,
    #[sea_orm(has_many = "super::feedback_source_formbricks_mapping::Entity")]
    FormbricksMappings,
    #[sea_orm(has_many = "super::feedback_source_field_mapping::Entity")]
    FieldMappings,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::feedback_directory::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FeedbackDirectory.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Creator.def()
    }
}

impl Related<super::feedback_directory_workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FeedbackDirectoryWorkspace.def()
    }
}

impl Related<super::feedback_source_formbricks_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FormbricksMappings.def()
    }
}

impl Related<super::feedback_source_field_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FieldMappings.def()
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
