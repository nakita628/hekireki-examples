use super::annotation_queue_object_type::AnnotationQueueObjectType;
use super::annotation_queue_status::AnnotationQueueStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "annotation_queue_items")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub queue_id: String,
    pub object_id: String,
    pub object_type: AnnotationQueueObjectType,
    #[sea_orm(default_value = "PENDING")]
    pub status: AnnotationQueueStatus,
    pub locked_at: Option<DateTime>,
    pub locked_by_user_id: Option<String>,
    pub annotator_user_id: Option<String>,
    pub completed_at: Option<DateTime>,
    pub project_id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::annotation_queue::Entity",
        from = "Column::QueueId",
        to = "super::annotation_queue::Column::Id",
        on_delete = "Cascade"
    )]
    Queue,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::LockedByUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    LockedByUser,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::AnnotatorUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    AnnotatorUser,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
}

impl Related<super::annotation_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Queue.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LockedByUser.def()
    }
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
