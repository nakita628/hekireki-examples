use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ContactAttribute")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_name = "attributeKeyId")]
    pub attribute_key_id: String,
    #[sea_orm(column_name = "contactId")]
    pub contact_id: String,
    pub value: String,
    #[sea_orm(column_name = "valueNumber")]
    pub value_number: Option<f64>,
    #[sea_orm(column_name = "valueDate")]
    pub value_date: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::contact_attribute_key::Entity",
        from = "Column::AttributeKeyId",
        to = "super::contact_attribute_key::Column::Id",
        on_delete = "Cascade"
    )]
    AttributeKey,
    #[sea_orm(
        belongs_to = "super::contact::Entity",
        from = "Column::ContactId",
        to = "super::contact::Column::Id",
        on_delete = "Cascade"
    )]
    Contact,
}

impl Related<super::contact_attribute_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttributeKey.def()
    }
}

impl Related<super::contact::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Contact.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
