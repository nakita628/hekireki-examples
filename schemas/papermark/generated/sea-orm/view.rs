use super::download_type::DownloadType;
use super::view_type::ViewType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "View")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "linkId")]
    pub link_id: String,
    #[sea_orm(column_name = "documentId")]
    pub document_id: Option<String>,
    #[sea_orm(column_name = "dataroomId")]
    pub dataroom_id: Option<String>,
    #[sea_orm(column_name = "dataroomViewId")]
    pub dataroom_view_id: Option<String>,
    #[sea_orm(column_name = "viewerEmail")]
    pub viewer_email: Option<String>,
    #[sea_orm(column_name = "viewerName")]
    pub viewer_name: Option<String>,
    #[sea_orm(default_value = false)]
    pub verified: bool,
    #[sea_orm(column_name = "viewedAt")]
    pub viewed_at: DateTime,
    #[sea_orm(column_name = "downloadedAt")]
    pub downloaded_at: Option<DateTime>,
    #[sea_orm(column_name = "downloadType")]
    pub download_type: Option<DownloadType>,
    #[sea_orm(column_name = "downloadMetadata")]
    pub download_metadata: Option<Json>,
    #[sea_orm(column_name = "viewType", default_value = "DOCUMENT_VIEW")]
    pub view_type: ViewType,
    #[sea_orm(column_name = "viewerId")]
    pub viewer_id: Option<String>,
    #[sea_orm(column_name = "groupId")]
    pub group_id: Option<String>,
    #[sea_orm(column_name = "isArchived", default_value = false)]
    pub is_archived: bool,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::link::Entity",
        from = "Column::LinkId",
        to = "super::link::Column::Id",
        on_delete = "Cascade"
    )]
    Link,
    #[sea_orm(
        belongs_to = "super::document::Entity",
        from = "Column::DocumentId",
        to = "super::document::Column::Id",
        on_delete = "Cascade"
    )]
    Document,
    #[sea_orm(
        belongs_to = "super::dataroom::Entity",
        from = "Column::DataroomId",
        to = "super::dataroom::Column::Id",
        on_delete = "Cascade"
    )]
    Dataroom,
    #[sea_orm(
        belongs_to = "super::viewer::Entity",
        from = "Column::ViewerId",
        to = "super::viewer::Column::Id",
        on_delete = "Cascade"
    )]
    Viewer,
    #[sea_orm(
        belongs_to = "super::viewer_group::Entity",
        from = "Column::GroupId",
        to = "super::viewer_group::Column::Id",
        on_delete = "SetNull"
    )]
    Group,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(has_many = "super::reaction::Entity")]
    Reactions,
    #[sea_orm(has_many = "super::conversation_view::Entity")]
    ConversationViews,
    #[sea_orm(has_many = "super::message::Entity")]
    Messages,
    #[sea_orm(has_many = "super::conversation::Entity")]
    InitialConversations,
    #[sea_orm(has_many = "super::document_upload::Entity")]
    UploadedDocuments,
    #[sea_orm(has_many = "super::task_activity::Entity")]
    TaskActivities,
    #[sea_orm(has_many = "super::chat::Entity")]
    Chats,
    #[sea_orm(has_one = "super::feedback_response::Entity")]
    FeedbackResponse,
    #[sea_orm(has_one = "super::agreement_response::Entity")]
    AgreementResponse,
    #[sea_orm(has_one = "super::custom_field_response::Entity")]
    CustomFieldResponse,
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Link.def()
    }
}

impl Related<super::document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Document.def()
    }
}

impl Related<super::dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dataroom.def()
    }
}

impl Related<super::viewer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Viewer.def()
    }
}

impl Related<super::viewer_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Group.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::reaction::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Reactions.def()
    }
}

impl Related<super::conversation_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConversationViews.def()
    }
}

impl Related<super::message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Messages.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InitialConversations.def()
    }
}

impl Related<super::document_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadedDocuments.def()
    }
}

impl Related<super::task_activity::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskActivities.def()
    }
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chats.def()
    }
}

impl Related<super::feedback_response::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FeedbackResponse.def()
    }
}

impl Related<super::agreement_response::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AgreementResponse.def()
    }
}

impl Related<super::custom_field_response::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomFieldResponse.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.viewed_at.is_not_set() {
            self.viewed_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
