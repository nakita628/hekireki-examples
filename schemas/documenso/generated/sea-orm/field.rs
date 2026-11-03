use super::field_type::FieldType;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Field")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "secondaryId")]
    pub secondary_id: String,
    #[sea_orm(column_name = "envelopeId")]
    pub envelope_id: String,
    #[sea_orm(column_name = "envelopeItemId")]
    pub envelope_item_id: String,
    #[sea_orm(column_name = "recipientId")]
    pub recipient_id: i32,
    pub r#type: FieldType,
    pub page: i32,
    #[sea_orm(column_name = "positionX", default_value = 0)]
    pub position_x: Decimal,
    #[sea_orm(column_name = "positionY", default_value = 0)]
    pub position_y: Decimal,
    #[sea_orm(default_value = -1)]
    pub width: Decimal,
    #[sea_orm(default_value = -1)]
    pub height: Decimal,
    #[sea_orm(column_name = "customText")]
    pub custom_text: String,
    pub inserted: bool,
    #[sea_orm(column_name = "fieldMeta")]
    pub field_meta: Option<Json>,
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
        belongs_to = "super::recipient::Entity",
        from = "Column::RecipientId",
        to = "super::recipient::Column::Id",
        on_delete = "Cascade"
    )]
    Recipient,
    #[sea_orm(has_one = "super::signature::Entity")]
    Signature,
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

impl Related<super::recipient::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Recipient.def()
    }
}

impl Related<super::signature::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Signature.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
