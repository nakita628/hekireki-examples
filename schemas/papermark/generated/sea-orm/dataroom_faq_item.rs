use super::faq_status::FaqStatus;
use super::faq_visibility::FaqVisibility;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DataroomFaqItem")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub title: Option<String>,
    #[sea_orm(column_name = "editedQuestion")]
    pub edited_question: String,
    #[sea_orm(column_name = "originalQuestion")]
    pub original_question: Option<String>,
    pub answer: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "dataroomId")]
    pub dataroom_id: String,
    #[sea_orm(column_name = "linkId")]
    pub link_id: Option<String>,
    #[sea_orm(column_name = "dataroomDocumentId")]
    pub dataroom_document_id: Option<String>,
    #[sea_orm(column_name = "sourceConversationId")]
    pub source_conversation_id: Option<String>,
    #[sea_orm(column_name = "questionMessageId")]
    pub question_message_id: Option<String>,
    #[sea_orm(column_name = "answerMessageId")]
    pub answer_message_id: Option<String>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    #[sea_orm(column_name = "publishedByUserId")]
    pub published_by_user_id: String,
    #[sea_orm(column_name = "visibilityMode", default_value = "PUBLIC_DATAROOM")]
    pub visibility_mode: FaqVisibility,
    #[sea_orm(default_value = "PUBLISHED")]
    pub status: FaqStatus,
    #[sea_orm(column_name = "isAnonymized", default_value = true)]
    pub is_anonymized: bool,
    #[sea_orm(column_name = "viewCount", default_value = 0)]
    pub view_count: i32,
    pub tags: Vec<String>,
    #[sea_orm(column_name = "documentPageNumber")]
    pub document_page_number: Option<i32>,
    #[sea_orm(column_name = "documentVersionNumber")]
    pub document_version_number: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::dataroom::Entity",
        from = "Column::DataroomId",
        to = "super::dataroom::Column::Id",
        on_delete = "Cascade"
    )]
    Dataroom,
    #[sea_orm(
        belongs_to = "super::link::Entity",
        from = "Column::LinkId",
        to = "super::link::Column::Id",
        on_delete = "SetNull"
    )]
    Link,
    #[sea_orm(
        belongs_to = "super::dataroom_document::Entity",
        from = "Column::DataroomDocumentId",
        to = "super::dataroom_document::Column::Id",
        on_delete = "SetNull"
    )]
    DataroomDocument,
    #[sea_orm(
        belongs_to = "super::conversation::Entity",
        from = "Column::SourceConversationId",
        to = "super::conversation::Column::Id",
        on_delete = "SetNull"
    )]
    SourceConversation,
    #[sea_orm(
        belongs_to = "super::message::Entity",
        from = "Column::QuestionMessageId",
        to = "super::message::Column::Id",
        on_delete = "SetNull"
    )]
    QuestionMessage,
    #[sea_orm(
        belongs_to = "super::message::Entity",
        from = "Column::AnswerMessageId",
        to = "super::message::Column::Id",
        on_delete = "SetNull"
    )]
    AnswerMessage,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::PublishedByUserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    PublishedByUser,
}

impl Related<super::dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dataroom.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Link.def()
    }
}

impl Related<super::dataroom_document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataroomDocument.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SourceConversation.def()
    }
}

impl Related<super::message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::QuestionMessage.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PublishedByUser.def()
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
