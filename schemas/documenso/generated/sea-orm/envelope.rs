use super::document_source::DocumentSource;
use super::document_status::DocumentStatus;
use super::document_visibility::DocumentVisibility;
use super::envelope_type::EnvelopeType;
use super::template_type::TemplateType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Envelope")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "secondaryId")]
    pub secondary_id: String,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    pub r#type: EnvelopeType,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "completedAt")]
    pub completed_at: Option<DateTime>,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    pub title: String,
    #[sea_orm(default_value = "DRAFT")]
    pub status: DocumentStatus,
    pub source: DocumentSource,
    #[sea_orm(column_name = "qrToken")]
    pub qr_token: Option<String>,
    #[sea_orm(column_name = "signatureLevel")]
    pub signature_level: String,
    #[sea_orm(column_name = "internalVersion")]
    pub internal_version: i32,
    #[sea_orm(column_name = "useLegacyFieldInsertion", default_value = false)]
    pub use_legacy_field_insertion: bool,
    #[sea_orm(column_name = "authOptions")]
    pub auth_options: Option<Json>,
    #[sea_orm(column_name = "formValues")]
    pub form_values: Option<Json>,
    #[sea_orm(default_value = "EVERYONE")]
    pub visibility: DocumentVisibility,
    #[sea_orm(column_name = "templateType", default_value = "PRIVATE")]
    pub template_type: TemplateType,
    #[sea_orm(column_name = "publicTitle", default_value = "")]
    pub public_title: String,
    #[sea_orm(column_name = "publicDescription", default_value = "")]
    pub public_description: String,
    #[sea_orm(column_name = "templateId")]
    pub template_id: Option<i32>,
    #[sea_orm(column_name = "userId")]
    pub user_id: i32,
    #[sea_orm(column_name = "teamId")]
    pub team_id: i32,
    #[sea_orm(column_name = "folderId")]
    pub folder_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "documentMetaId")]
    pub document_meta_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::folder::Entity",
        from = "Column::FolderId",
        to = "super::folder::Column::Id",
        on_delete = "SetNull"
    )]
    Folder,
    #[sea_orm(
        belongs_to = "super::document_meta::Entity",
        from = "Column::DocumentMetaId",
        to = "super::document_meta::Column::Id"
    )]
    DocumentMeta,
    #[sea_orm(has_many = "super::envelope_item::Entity")]
    EnvelopeItems,
    #[sea_orm(has_many = "super::recipient::Entity")]
    Recipients,
    #[sea_orm(has_many = "super::field::Entity")]
    Fields,
    #[sea_orm(has_many = "super::document_share_link::Entity")]
    ShareLinks,
    #[sea_orm(has_many = "super::document_audit_log::Entity")]
    AuditLogs,
    #[sea_orm(has_many = "super::envelope_attachment::Entity")]
    EnvelopeAttachments,
    #[sea_orm(has_one = "super::template_direct_link::Entity")]
    DirectLink,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folder.def()
    }
}

impl Related<super::document_meta::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DocumentMeta.def()
    }
}

impl Related<super::envelope_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvelopeItems.def()
    }
}

impl Related<super::recipient::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Recipients.def()
    }
}

impl Related<super::field::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Fields.def()
    }
}

impl Related<super::document_share_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareLinks.def()
    }
}

impl Related<super::document_audit_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AuditLogs.def()
    }
}

impl Related<super::envelope_attachment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvelopeAttachments.def()
    }
}

impl Related<super::template_direct_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DirectLink.def()
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
