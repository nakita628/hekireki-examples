use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Signature")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub created: DateTime,
    #[sea_orm(column_name = "recipientId")]
    pub recipient_id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "fieldId")]
    pub field_id: i32,
    #[sea_orm(column_name = "signatureImageAsBase64")]
    pub signature_image_as_base64: Option<String>,
    #[sea_orm(column_name = "typedSignature")]
    pub typed_signature: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::recipient::Entity",
        from = "Column::RecipientId",
        to = "super::recipient::Column::Id",
        on_delete = "Cascade"
    )]
    Recipient,
    #[sea_orm(
        belongs_to = "super::field::Entity",
        from = "Column::FieldId",
        to = "super::field::Column::Id",
        on_delete = "Cascade"
    )]
    Field,
}

impl Related<super::recipient::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Recipient.def()
    }
}

impl Related<super::field::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Field.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created.is_not_set() {
            self.created = Set(now.naive_utc());
        }
        Ok(self)
    }
}
