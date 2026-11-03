use super::document_visibility::DocumentVisibility;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganisationGlobalSettings")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "documentVisibility", default_value = "EVERYONE")]
    pub document_visibility: DocumentVisibility,
    #[sea_orm(column_name = "documentLanguage", default_value = "en")]
    pub document_language: String,
    #[sea_orm(column_name = "includeSenderDetails", default_value = true)]
    pub include_sender_details: bool,
    #[sea_orm(column_name = "includeSigningCertificate", default_value = true)]
    pub include_signing_certificate: bool,
    #[sea_orm(column_name = "includeAuditLog", default_value = false)]
    pub include_audit_log: bool,
    #[sea_orm(column_name = "documentTimezone")]
    pub document_timezone: Option<String>,
    #[sea_orm(
        column_name = "documentDateFormat",
        default_value = "yyyy-MM-dd hh:mm a"
    )]
    pub document_date_format: String,
    #[sea_orm(column_name = "delegateDocumentOwnership", default_value = false)]
    pub delegate_document_ownership: bool,
    #[sea_orm(column_name = "typedSignatureEnabled", default_value = true)]
    pub typed_signature_enabled: bool,
    #[sea_orm(column_name = "uploadSignatureEnabled", default_value = true)]
    pub upload_signature_enabled: bool,
    #[sea_orm(column_name = "drawSignatureEnabled", default_value = true)]
    pub draw_signature_enabled: bool,
    #[sea_orm(column_name = "defaultRecipients")]
    pub default_recipients: Option<Json>,
    #[sea_orm(column_name = "emailId")]
    pub email_id: Option<String>,
    #[sea_orm(column_name = "emailReplyTo")]
    pub email_reply_to: Option<String>,
    #[sea_orm(column_name = "emailDocumentSettings")]
    pub email_document_settings: Json,
    #[sea_orm(column_name = "brandingEnabled", default_value = false)]
    pub branding_enabled: bool,
    #[sea_orm(column_name = "brandingLogo", default_value = "")]
    pub branding_logo: String,
    #[sea_orm(column_name = "brandingUrl", default_value = "")]
    pub branding_url: String,
    #[sea_orm(column_name = "brandingCompanyDetails", default_value = "")]
    pub branding_company_details: String,
    #[sea_orm(column_name = "brandingColors")]
    pub branding_colors: Option<Json>,
    #[sea_orm(column_name = "brandingCss", default_value = "")]
    pub branding_css: String,
    #[sea_orm(column_name = "envelopeExpirationPeriod")]
    pub envelope_expiration_period: Option<Json>,
    #[sea_orm(column_name = "reminderSettings")]
    pub reminder_settings: Option<Json>,
    #[sea_orm(column_name = "aiFeaturesEnabled", default_value = false)]
    pub ai_features_enabled: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organisation_email::Entity",
        from = "Column::EmailId",
        to = "super::organisation_email::Column::Id"
    )]
    Email,
    #[sea_orm(has_one = "super::organisation::Entity")]
    Organisation,
}

impl Related<super::organisation_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Email.def()
    }
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organisation.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
