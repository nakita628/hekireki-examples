use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DataroomFolder")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub path: String,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<String>,
    pub icon: Option<String>,
    pub color: Option<String>,
    #[sea_orm(column_name = "dataroomId")]
    pub dataroom_id: String,
    #[sea_orm(column_name = "orderIndex")]
    pub order_index: Option<i32>,
    #[sea_orm(column_name = "hierarchicalIndex")]
    pub hierarchical_index: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::dataroom_folder::Entity",
        from = "Column::ParentId",
        to = "super::dataroom_folder::Column::Id",
        on_delete = "SetNull"
    )]
    ParentFolder,
    #[sea_orm(
        belongs_to = "super::dataroom::Entity",
        from = "Column::DataroomId",
        to = "super::dataroom::Column::Id",
        on_delete = "Cascade"
    )]
    Dataroom,
    #[sea_orm(has_many = "super::dataroom_document::Entity")]
    Documents,
    #[sea_orm(
        has_many = "super::dataroom_folder::Entity",
        from = "Column::Id",
        to = "super::dataroom_folder::Column::ParentId"
    )]
    ChildFolders,
    #[sea_orm(has_many = "super::task::Entity")]
    UploadTasks,
}

impl Related<super::dataroom_folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ParentFolder.def()
    }
}

impl Related<super::dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dataroom.def()
    }
}

impl Related<super::dataroom_document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Documents.def()
    }
}

impl Related<super::task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadTasks.def()
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
