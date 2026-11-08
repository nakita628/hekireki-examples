use super::document_data_type::DocumentDataType;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DataContent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub r#type: DocumentDataType,
    pub data: String,
    pub metadata: Json,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::envelope_content::Entity")]
    EnvelopeContents,
}

impl Related<super::envelope_content::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvelopeContents.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
