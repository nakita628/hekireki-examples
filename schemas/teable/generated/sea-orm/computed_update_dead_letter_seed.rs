use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "computed_update_dead_letter_seed")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub task_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub table_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub record_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::computed_update_dead_letter::Entity",
        from = "Column::TaskId",
        to = "super::computed_update_dead_letter::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Task,
}

impl Related<super::computed_update_dead_letter::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Task.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
