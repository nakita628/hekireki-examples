use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ConversationParticipant")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "conversationId")]
    pub conversation_id: String,
    #[sea_orm(column_name = "memberId")]
    pub member_id: String,
    pub generation: i32,
    #[sea_orm(default_value = true)]
    pub active: bool,
    #[sea_orm(column_name = "readRevision", default_value = 0)]
    pub read_revision: i32,
    #[sea_orm(default_value = false)]
    pub muted: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::conversation::Entity",
        from = "Column::ConversationId",
        to = "super::conversation::Column::Id",
        on_delete = "Cascade"
    )]
    Conversation,
    #[sea_orm(
        belongs_to = "super::member::Entity",
        from = "Column::MemberId",
        to = "super::member::Column::Id",
        on_delete = "Cascade"
    )]
    Member,
    #[sea_orm(has_many = "super::conversation_activity::Entity")]
    Activity,
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversation.def()
    }
}

impl Related<super::member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Member.def()
    }
}

impl Related<super::conversation_activity::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Activity.def()
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
