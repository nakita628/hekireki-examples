use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ReplyMemorySource")]
pub struct Model {
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "learnedWritingStyleAnalyzedAt")]
    pub learned_writing_style_analyzed_at: Option<DateTime>,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "replyMemoryId")]
    pub reply_memory_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "draftSendLogId")]
    pub draft_send_log_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::reply_memory::Entity",
        from = "Column::ReplyMemoryId",
        to = "super::reply_memory::Column::Id",
        on_delete = "Cascade"
    )]
    ReplyMemory,
    #[sea_orm(
        belongs_to = "super::draft_send_log::Entity",
        from = "Column::DraftSendLogId",
        to = "super::draft_send_log::Column::Id",
        on_delete = "Cascade"
    )]
    DraftSendLog,
}

impl Related<super::reply_memory::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReplyMemory.def()
    }
}

impl Related<super::draft_send_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DraftSendLog.def()
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
        Ok(self)
    }
}
