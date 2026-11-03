use super::conversation_visibility::ConversationVisibility;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Conversation")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub title: Option<String>,
    #[sea_orm(column_name = "isEnabled", default_value = true)]
    pub is_enabled: bool,
    #[sea_orm(column_name = "visibilityMode", default_value = "PRIVATE")]
    pub visibility_mode: ConversationVisibility,
    #[sea_orm(column_name = "dataroomId")]
    pub dataroom_id: String,
    #[sea_orm(column_name = "dataroomDocumentId")]
    pub dataroom_document_id: Option<String>,
    #[sea_orm(column_name = "documentVersionNumber")]
    pub document_version_number: Option<i32>,
    #[sea_orm(column_name = "documentPageNumber")]
    pub document_page_number: Option<i32>,
    #[sea_orm(column_name = "linkId")]
    pub link_id: Option<String>,
    #[sea_orm(column_name = "viewerGroupId")]
    pub viewer_group_id: Option<String>,
    #[sea_orm(column_name = "questionId")]
    pub question_id: Option<String>,
    #[sea_orm(default_value = "visitor")]
    pub origin: String,
    pub category: Option<String>,
    pub priority: Option<String>,
    #[sea_orm(column_name = "recipientViewerId")]
    pub recipient_viewer_id: Option<String>,
    #[sea_orm(column_name = "initialViewId")]
    pub initial_view_id: Option<String>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    #[sea_orm(column_name = "lastMessageAt")]
    pub last_message_at: Option<DateTime>,
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
        belongs_to = "super::dataroom_document::Entity",
        from = "Column::DataroomDocumentId",
        to = "super::dataroom_document::Column::Id",
        on_delete = "SetNull"
    )]
    DataroomDocument,
    #[sea_orm(
        belongs_to = "super::link::Entity",
        from = "Column::LinkId",
        to = "super::link::Column::Id",
        on_delete = "SetNull"
    )]
    Link,
    #[sea_orm(
        belongs_to = "super::viewer_group::Entity",
        from = "Column::ViewerGroupId",
        to = "super::viewer_group::Column::Id",
        on_delete = "SetNull"
    )]
    ViewerGroup,
    #[sea_orm(
        belongs_to = "super::dataroom_question::Entity",
        from = "Column::QuestionId",
        to = "super::dataroom_question::Column::Id",
        on_delete = "Cascade"
    )]
    Question,
    #[sea_orm(
        belongs_to = "super::viewer::Entity",
        from = "Column::RecipientViewerId",
        to = "super::viewer::Column::Id",
        on_delete = "SetNull"
    )]
    RecipientViewer,
    #[sea_orm(
        belongs_to = "super::view::Entity",
        from = "Column::InitialViewId",
        to = "super::view::Column::Id",
        on_delete = "SetNull"
    )]
    InitialView,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(has_many = "super::conversation_view::Entity")]
    Views,
    #[sea_orm(has_many = "super::conversation_participant::Entity")]
    Participants,
    #[sea_orm(has_many = "super::message::Entity")]
    Messages,
    #[sea_orm(has_many = "super::dataroom_faq_item::Entity")]
    FaqItems,
}

impl Related<super::dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dataroom.def()
    }
}

impl Related<super::dataroom_document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataroomDocument.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Link.def()
    }
}

impl Related<super::viewer_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ViewerGroup.def()
    }
}

impl Related<super::dataroom_question::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Question.def()
    }
}

impl Related<super::viewer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RecipientViewer.def()
    }
}

impl Related<super::view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InitialView.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::conversation_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Views.def()
    }
}

impl Related<super::conversation_participant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Participants.def()
    }
}

impl Related<super::message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Messages.def()
    }
}

impl Related<super::dataroom_faq_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FaqItems.def()
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
