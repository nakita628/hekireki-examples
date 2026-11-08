use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EnvelopeContent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "envelopeId")]
    pub envelope_id: String,
    #[sea_orm(column_name = "envelopeItemId")]
    pub envelope_item_id: String,
    #[sea_orm(column_name = "contentMeta")]
    pub content_meta: Json,
    #[sea_orm(column_name = "dataContentId")]
    pub data_content_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::envelope_item::Entity",
        from = "Column::EnvelopeItemId",
        to = "super::envelope_item::Column::Id",
        on_delete = "Cascade"
    )]
    EnvelopeItem,
    #[sea_orm(
        belongs_to = "super::envelope::Entity",
        from = "Column::EnvelopeId",
        to = "super::envelope::Column::Id",
        on_delete = "Cascade"
    )]
    Envelope,
    #[sea_orm(
        belongs_to = "super::data_content::Entity",
        from = "Column::DataContentId",
        to = "super::data_content::Column::Id",
        on_delete = "SetNull"
    )]
    DataContent,
}

impl Related<super::envelope_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvelopeItem.def()
    }
}

impl Related<super::envelope::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Envelope.def()
    }
}

impl Related<super::data_content::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataContent.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
