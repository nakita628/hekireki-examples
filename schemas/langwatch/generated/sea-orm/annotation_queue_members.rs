use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AnnotationQueueMembers")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "annotationQueueId")]
    pub annotation_queue_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::annotation_queue::Entity",
        from = "Column::AnnotationQueueId",
        to = "super::annotation_queue::Column::Id"
    )]
    AnnotationQueue,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::annotation_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueue.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
