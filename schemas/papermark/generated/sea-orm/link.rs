use super::link_audience_type::LinkAudienceType;
use super::link_type::LinkType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Link")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "documentId")]
    pub document_id: Option<String>,
    #[sea_orm(column_name = "dataroomId")]
    pub dataroom_id: Option<String>,
    #[sea_orm(column_name = "linkType", default_value = "DOCUMENT_LINK")]
    pub link_type: LinkType,
    #[sea_orm(unique)]
    pub url: Option<String>,
    pub name: Option<String>,
    pub slug: Option<String>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    pub password: Option<String>,
    #[sea_orm(column_name = "allowList")]
    pub allow_list: Vec<String>,
    #[sea_orm(column_name = "denyList")]
    pub deny_list: Vec<String>,
    #[sea_orm(column_name = "emailProtected", default_value = true)]
    pub email_protected: bool,
    #[sea_orm(column_name = "emailAuthenticated", default_value = false)]
    pub email_authenticated: bool,
    #[sea_orm(column_name = "allowDownload", default_value = false)]
    pub allow_download: Option<bool>,
    #[sea_orm(column_name = "isArchived", default_value = false)]
    pub is_archived: bool,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "domainId")]
    pub domain_id: Option<String>,
    #[sea_orm(column_name = "domainSlug")]
    pub domain_slug: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "enableNotification", default_value = true)]
    pub enable_notification: Option<bool>,
    #[sea_orm(column_name = "enableFeedback", default_value = false)]
    pub enable_feedback: Option<bool>,
    #[sea_orm(column_name = "enableQuestion", default_value = false)]
    pub enable_question: Option<bool>,
    #[sea_orm(column_name = "enableScreenshotProtection", default_value = false)]
    pub enable_screenshot_protection: Option<bool>,
    #[sea_orm(column_name = "enableConfidentialView", default_value = false)]
    pub enable_confidential_view: Option<bool>,
    #[sea_orm(column_name = "enableAgreement", default_value = false)]
    pub enable_agreement: Option<bool>,
    #[sea_orm(column_name = "agreementId")]
    pub agreement_id: Option<String>,
    #[sea_orm(column_name = "showBanner", default_value = false)]
    pub show_banner: Option<bool>,
    #[sea_orm(column_name = "enableWatermark", default_value = false)]
    pub enable_watermark: Option<bool>,
    #[sea_orm(column_name = "watermarkConfig")]
    pub watermark_config: Option<Json>,
    #[sea_orm(column_name = "audienceType", default_value = "GENERAL")]
    pub audience_type: LinkAudienceType,
    #[sea_orm(column_name = "groupId")]
    pub group_id: Option<String>,
    #[sea_orm(column_name = "permissionGroupId")]
    pub permission_group_id: Option<String>,
    #[sea_orm(column_name = "metaTitle")]
    pub meta_title: Option<String>,
    #[sea_orm(column_name = "metaDescription")]
    pub meta_description: Option<String>,
    #[sea_orm(column_name = "metaImage")]
    pub meta_image: Option<String>,
    #[sea_orm(column_name = "metaFavicon")]
    pub meta_favicon: Option<String>,
    #[sea_orm(column_name = "enableCustomMetatag", default_value = false)]
    pub enable_custom_metatag: Option<bool>,
    #[sea_orm(column_name = "welcomeMessage")]
    pub welcome_message: Option<String>,
    #[sea_orm(column_name = "enableConversation", default_value = false)]
    pub enable_conversation: bool,
    #[sea_orm(column_name = "enableAIAgents", default_value = false)]
    pub enable_aiagents: Option<bool>,
    #[sea_orm(column_name = "enableUpload", default_value = false)]
    pub enable_upload: Option<bool>,
    #[sea_orm(column_name = "isFileRequestOnly", default_value = false)]
    pub is_file_request_only: Option<bool>,
    #[sea_orm(column_name = "uploadFolderId")]
    pub upload_folder_id: Option<String>,
    #[sea_orm(column_name = "uploadFolderIds")]
    pub upload_folder_ids: Vec<String>,
    #[sea_orm(column_name = "enableIndexFile", default_value = false)]
    pub enable_index_file: Option<bool>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<String>,
    #[sea_orm(column_name = "brandId")]
    pub brand_id: Option<String>,
    #[sea_orm(column_name = "ownerId")]
    pub owner_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
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
        belongs_to = "super::domain::Entity",
        from = "Column::DomainId",
        to = "super::domain::Column::Id",
        on_delete = "SetNull"
    )]
    Domain,
    #[sea_orm(
        belongs_to = "super::agreement::Entity",
        from = "Column::AgreementId",
        to = "super::agreement::Column::Id",
        on_delete = "SetNull"
    )]
    Agreement,
    #[sea_orm(
        belongs_to = "super::viewer_group::Entity",
        from = "Column::GroupId",
        to = "super::viewer_group::Column::Id",
        on_delete = "SetNull"
    )]
    Group,
    #[sea_orm(
        belongs_to = "super::permission_group::Entity",
        from = "Column::PermissionGroupId",
        to = "super::permission_group::Column::Id",
        on_delete = "SetNull"
    )]
    PermissionGroup,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::brand::Entity",
        from = "Column::BrandId",
        to = "super::brand::Column::Id",
        on_delete = "SetNull"
    )]
    Brand,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    Owner,
    #[sea_orm(has_many = "super::view::Entity")]
    Views,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_many = "super::dataroom_question_assignment::Entity")]
    QuestionAssignments,
    #[sea_orm(has_many = "super::dataroom_faq_item::Entity")]
    DataroomFaqItems,
    #[sea_orm(has_many = "super::chat::Entity")]
    Chats,
    #[sea_orm(has_many = "super::document_upload::Entity")]
    UploadedDocuments,
    #[sea_orm(has_many = "super::custom_field::Entity")]
    CustomFields,
    #[sea_orm(has_many = "super::tag_item::Entity")]
    Tags,
    #[sea_orm(has_many = "super::viewer_invitation::Entity")]
    ViewerInvitations,
    #[sea_orm(has_many = "super::link_visitor_group::Entity")]
    VisitorGroups,
    #[sea_orm(has_many = "super::task_assignment::Entity")]
    TaskAssignments,
    #[sea_orm(has_one = "super::feedback::Entity")]
    Feedback,
    #[sea_orm(has_one = "super::workflow::Entity")]
    Workflow,
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

impl Related<super::domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Domain.def()
    }
}

impl Related<super::agreement::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agreement.def()
    }
}

impl Related<super::viewer_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Group.def()
    }
}

impl Related<super::permission_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PermissionGroup.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::brand::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Brand.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Owner.def()
    }
}

impl Related<super::view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Views.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::dataroom_question_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::QuestionAssignments.def()
    }
}

impl Related<super::dataroom_faq_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataroomFaqItems.def()
    }
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chats.def()
    }
}

impl Related<super::document_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadedDocuments.def()
    }
}

impl Related<super::custom_field::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomFields.def()
    }
}

impl Related<super::tag_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::viewer_invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ViewerInvitations.def()
    }
}

impl Related<super::link_visitor_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VisitorGroups.def()
    }
}

impl Related<super::task_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskAssignments.def()
    }
}

impl Related<super::feedback::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Feedback.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflow.def()
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
