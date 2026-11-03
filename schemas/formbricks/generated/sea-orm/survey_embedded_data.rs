use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SurveyEmbeddedData")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: String,
    #[sea_orm(column_name = "embeddedDataId")]
    pub embedded_data_id: String,
    #[sea_orm(column_name = "storageKey")]
    pub storage_key: String,
    pub order: i32,
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
        belongs_to = "super::embedded_data::Entity",
        from = "(Column::EmbeddedDataId, Column::WorkspaceId)",
        to = "(super::embedded_data::Column::Id, super::embedded_data::Column::WorkspaceId)",
        on_delete = "Cascade"
    )]
    EmbeddedData,
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Survey.def()
    }
}

impl Related<super::embedded_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbeddedData.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
