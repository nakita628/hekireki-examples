use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AnnotationQueueScores")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "annotationQueueId")]
    pub annotation_queue_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "annotationScoreId")]
    pub annotation_score_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::annotation_score::Entity",
        from = "Column::AnnotationScoreId",
        to = "super::annotation_score::Column::Id"
    )]
    AnnotationScore,
    #[sea_orm(
        belongs_to = "super::annotation_queue::Entity",
        from = "Column::AnnotationQueueId",
        to = "super::annotation_queue::Column::Id"
    )]
    AnnotationQueue,
}

impl Related<super::annotation_score::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationScore.def()
    }
}

impl Related<super::annotation_queue::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueue.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
