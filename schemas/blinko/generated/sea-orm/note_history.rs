use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "noteHistory")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "noteId")]
    pub note_id: i32,
    #[sea_orm(column_type = "Text")]
    pub content: String,
    pub metadata: Option<Json>,
    pub version: i32,
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<i32>,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::notes::Entity",
        from = "Column::NoteId",
        to = "super::notes::Column::Id",
        on_delete = "Cascade"
    )]
    Note,
}

impl Related<super::notes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Note.def()
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
        Ok(self)
    }
}
