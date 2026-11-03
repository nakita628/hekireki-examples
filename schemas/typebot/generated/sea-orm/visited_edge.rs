use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "VisitedEdge")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "resultId")]
    pub result_id: String,
    #[sea_orm(column_name = "edgeId")]
    pub edge_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub index: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::result::Entity",
        from = "Column::ResultId",
        to = "super::result::Column::Id",
        on_delete = "Cascade"
    )]
    Result,
}

impl Related<super::result::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Result.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
