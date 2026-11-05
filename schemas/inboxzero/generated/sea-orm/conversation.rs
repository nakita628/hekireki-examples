use super::conversation_status::ConversationStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Conversation")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "publisherId")]
    pub publisher_id: String,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: String,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: ConversationStatus,
    #[sea_orm(default_value = 0)]
    pub revision: i32,
    #[sea_orm(default_value = 1)]
    pub generation: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::member::Entity",
        from = "Column::PublisherId",
        to = "super::member::Column::Id",
        on_delete = "Cascade"
    )]
    Publisher,
    #[sea_orm(has_many = "super::conversation_participant::Entity")]
    Participants,
    #[sea_orm(has_many = "super::conversation_comment::Entity")]
    Comments,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Publisher.def()
    }
}

impl Related<super::conversation_participant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Participants.def()
    }
}

impl Related<super::conversation_comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
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
