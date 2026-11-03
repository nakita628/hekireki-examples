use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AttributeOption")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "attributeId")]
    pub attribute_id: String,
    pub value: String,
    pub slug: String,
    #[sea_orm(column_name = "isGroup", default_value = false)]
    pub is_group: bool,
    pub contains: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::attribute::Entity",
        from = "Column::AttributeId",
        to = "super::attribute::Column::Id",
        on_delete = "Cascade"
    )]
    Attribute,
    #[sea_orm(has_many = "super::attribute_to_user::Entity")]
    AssignedUsers,
}

impl Related<super::attribute::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attribute.def()
    }
}

impl Related<super::attribute_to_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AssignedUsers.def()
    }
}

impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
