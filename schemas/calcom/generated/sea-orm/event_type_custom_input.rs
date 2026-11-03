use super::event_type_custom_input_type::EventTypeCustomInputType;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EventTypeCustomInput")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "eventTypeId")]
    pub event_type_id: i32,
    pub label: String,
    pub r#type: EventTypeCustomInputType,
    pub options: Option<Json>,
    pub required: bool,
    #[sea_orm(default_value = "")]
    pub placeholder: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::event_type::Entity",
        from = "Column::EventTypeId",
        to = "super::event_type::Column::Id",
        on_delete = "Cascade"
    )]
    EventType,
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
