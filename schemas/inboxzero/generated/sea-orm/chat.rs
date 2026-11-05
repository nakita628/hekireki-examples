use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Chat")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    pub name: Option<String>,
    #[sea_orm(column_name = "compactionCount", default_value = 0)]
    pub compaction_count: i32,
    #[sea_orm(column_name = "lastSeenRulesRevision")]
    pub last_seen_rules_revision: Option<i32>,
    #[sea_orm(column_name = "activeStreamId")]
    pub active_stream_id: Option<String>,
    #[sea_orm(column_name = "activeStreamStartedAt")]
    pub active_stream_started_at: Option<DateTime>,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
    #[sea_orm(has_many = "super::chat_message::Entity")]
    Messages,
    #[sea_orm(has_many = "super::chat_compaction::Entity")]
    Compactions,
    #[sea_orm(has_many = "super::chat_memory::Entity")]
    Memories,
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::chat_message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Messages.def()
    }
}

impl Related<super::chat_compaction::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Compactions.def()
    }
}

impl Related<super::chat_memory::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Memories.def()
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
