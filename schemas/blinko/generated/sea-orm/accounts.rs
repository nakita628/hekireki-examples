use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "accounts")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(default_value = "")]
    pub name: String,
    #[sea_orm(default_value = "")]
    pub nickname: String,
    #[sea_orm(default_value = "")]
    pub password: String,
    #[sea_orm(default_value = "")]
    pub image: String,
    #[sea_orm(column_name = "apiToken", default_value = "")]
    pub api_token: String,
    #[sea_orm(column_type = "Text", default_value = "")]
    pub description: String,
    #[sea_orm(default_value = 0)]
    pub note: i32,
    #[sea_orm(default_value = "")]
    pub role: String,
    #[sea_orm(column_name = "loginType", default_value = "")]
    pub login_type: String,
    #[sea_orm(column_name = "linkAccountId")]
    pub link_account_id: Option<i32>,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::notes::Entity")]
    Notes,
    #[sea_orm(has_many = "super::config::Entity")]
    Configs,
    #[sea_orm(has_many = "super::tag::Entity")]
    Tags,
    #[sea_orm(has_many = "super::comments::Entity")]
    Comments,
    #[sea_orm(has_many = "super::attachments::Entity")]
    Attachments,
    #[sea_orm(has_many = "super::follows::Entity")]
    Follows,
    #[sea_orm(has_many = "super::notifications::Entity")]
    Notifications,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_many = "super::note_internal_share::Entity")]
    SharedNotes,
    #[sea_orm(has_many = "super::ai_scheduled_task::Entity")]
    AiScheduledTasks,
}

impl Related<super::notes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notes.def()
    }
}

impl Related<super::config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Configs.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::comments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::attachments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attachments.def()
    }
}

impl Related<super::follows::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Follows.def()
    }
}

impl Related<super::notifications::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notifications.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::note_internal_share::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SharedNotes.def()
    }
}

impl Related<super::ai_scheduled_task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AiScheduledTasks.def()
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
