use super::conversation_activity_kind::ConversationActivityKind;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ConversationActivity")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "participantId")]
    pub participant_id: String,
    #[sea_orm(column_name = "commentId")]
    pub comment_id: Option<String>,
    pub kind: ConversationActivityKind,
    pub revision: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::conversation_participant::Entity",
        from = "Column::ParticipantId",
        to = "super::conversation_participant::Column::Id",
        on_delete = "Cascade"
    )]
    Participant,
    #[sea_orm(
        belongs_to = "super::conversation_comment::Entity",
        from = "Column::CommentId",
        to = "super::conversation_comment::Column::Id",
        on_delete = "SetNull"
    )]
    Comment,
}

impl Related<super::conversation_participant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Participant.def()
    }
}

impl Related<super::conversation_comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comment.def()
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
