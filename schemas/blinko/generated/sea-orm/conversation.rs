use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "conversation")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_type = "String(StringLen::N(255))", default_value = "")]
    pub title: String,
    #[sea_orm(column_name = "isShare", default_value = false)]
    pub is_share: bool,
    #[sea_orm(column_name = "accountId")]
    pub account_id: i32,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::accounts::Entity",
        from = "Column::AccountId",
        to = "super::accounts::Column::Id"
    )]
    Account,
    #[sea_orm(has_many = "super::message::Entity")]
    Messages,
}

impl Related<super::accounts::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Account.def()
    }
}

impl Related<super::message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Messages.def()
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
            self.created_at = Set(now.fixed_offset());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
