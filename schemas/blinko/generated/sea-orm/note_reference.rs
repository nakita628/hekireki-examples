use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "noteReference")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "fromNoteId")]
    pub from_note_id: i32,
    #[sea_orm(column_name = "toNoteId")]
    pub to_note_id: i32,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::notes::Entity",
        from = "Column::FromNoteId",
        to = "super::notes::Column::Id"
    )]
    FromNote,
    #[sea_orm(
        belongs_to = "super::notes::Entity",
        from = "Column::ToNoteId",
        to = "super::notes::Column::Id"
    )]
    ToNote,
}

impl Related<super::notes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FromNote.def()
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
