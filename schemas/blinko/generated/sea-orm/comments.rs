use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "comments")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_type = "Text")]
    pub content: String,
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<i32>,
    #[sea_orm(column_name = "guestName")]
    pub guest_name: Option<String>,
    #[sea_orm(column_name = "guestIP")]
    pub guest_ip: Option<String>,
    #[sea_orm(column_name = "guestUA")]
    pub guest_ua: Option<String>,
    #[sea_orm(column_name = "noteId")]
    pub note_id: i32,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<i32>,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::notes::Entity",
        from = "Column::NoteId",
        to = "super::notes::Column::Id"
    )]
    Note,
    #[sea_orm(
        belongs_to = "super::accounts::Entity",
        from = "Column::AccountId",
        to = "super::accounts::Column::Id"
    )]
    Account,
    #[sea_orm(
        belongs_to = "super::comments::Entity",
        from = "Column::ParentId",
        to = "super::comments::Column::Id"
    )]
    Parent,
    #[sea_orm(
        has_many = "super::comments::Entity",
        from = "Column::Id",
        to = "super::comments::Column::ParentId"
    )]
    Replies,
}

impl Related<super::notes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Note.def()
    }
}

impl Related<super::accounts::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Account.def()
    }
}

impl Related<super::comments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Parent.def()
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
