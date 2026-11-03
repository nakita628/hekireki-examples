use super::document_visibility::DocumentVisibility;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TeamGlobalSettings")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "documentVisibility")]
    pub document_visibility: Option<DocumentVisibility>,
    #[sea_orm(column_name = "documentLanguage")]
    pub document_language: Option<String>,
    #[sea_orm(column_name = "documentTimezone")]
    pub document_timezone: Option<String>,
    #[sea_orm(column_name = "documentDateFormat")]
    pub document_date_format: Option<String>,
    #[sea_orm(column_name = "delegateDocumentOwnership")]
    pub delegate_document_ownership: Option<bool>,
    #[sea_orm(column_name = "includeSenderDetails")]
    pub include_sender_details: Option<bool>,
    #[sea_orm(column_name = "includeSigningCertificate")]
    pub include_signing_certificate: Option<bool>,
    #[sea_orm(column_name = "includeAuditLog")]
    pub include_audit_log: Option<bool>,
    #[sea_orm(column_name = "typedSignatureEnabled")]
    pub typed_signature_enabled: Option<bool>,
    #[sea_orm(column_name = "uploadSignatureEnabled")]
    pub upload_signature_enabled: Option<bool>,
    #[sea_orm(column_name = "drawSignatureEnabled")]
    pub draw_signature_enabled: Option<bool>,
    #[sea_orm(column_name = "defaultRecipients")]
    pub default_recipients: Option<Json>,
    #[sea_orm(column_name = "emailId")]
    pub email_id: Option<String>,
    #[sea_orm(column_name = "emailReplyTo")]
    pub email_reply_to: Option<String>,
    #[sea_orm(column_name = "emailDocumentSettings")]
    pub email_document_settings: Option<Json>,
    #[sea_orm(column_name = "brandingEnabled")]
    pub branding_enabled: Option<bool>,
    #[sea_orm(column_name = "brandingLogo")]
    pub branding_logo: Option<String>,
    #[sea_orm(column_name = "brandingUrl")]
    pub branding_url: Option<String>,
    #[sea_orm(column_name = "brandingCompanyDetails")]
    pub branding_company_details: Option<String>,
    #[sea_orm(column_name = "brandingColors")]
    pub branding_colors: Option<Json>,
    #[sea_orm(column_name = "brandingCss")]
    pub branding_css: Option<String>,
    #[sea_orm(column_name = "envelopeExpirationPeriod")]
    pub envelope_expiration_period: Option<Json>,
    #[sea_orm(column_name = "reminderSettings")]
    pub reminder_settings: Option<Json>,
    #[sea_orm(column_name = "aiFeaturesEnabled")]
    pub ai_features_enabled: Option<bool>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organisation_email::Entity",
        from = "Column::EmailId",
        to = "super::organisation_email::Column::Id"
    )]
    Email,
    #[sea_orm(has_one = "super::team::Entity")]
    Team,
}

impl Related<super::organisation_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Email.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
