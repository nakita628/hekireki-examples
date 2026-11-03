use super::referral_track::ReferralTrack;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Team")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub slug: Option<String>,
    #[sea_orm(column_name = "defaultBrandId")]
    pub default_brand_id: Option<String>,
    #[sea_orm(default_value = "free")]
    pub plan: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeId")]
    pub stripe_id: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "subscriptionId")]
    pub subscription_id: Option<String>,
    #[sea_orm(column_name = "startsAt")]
    pub starts_at: Option<DateTime>,
    #[sea_orm(column_name = "endsAt")]
    pub ends_at: Option<DateTime>,
    #[sea_orm(column_name = "pausedAt")]
    pub paused_at: Option<DateTime>,
    #[sea_orm(column_name = "pauseStartsAt")]
    pub pause_starts_at: Option<DateTime>,
    #[sea_orm(column_name = "pauseEndsAt")]
    pub pause_ends_at: Option<DateTime>,
    #[sea_orm(column_name = "cancelledAt")]
    pub cancelled_at: Option<DateTime>,
    #[sea_orm(column_name = "trialStartedAt")]
    pub trial_started_at: Option<DateTime>,
    #[sea_orm(column_name = "trialEndsAt")]
    pub trial_ends_at: Option<DateTime>,
    #[sea_orm(column_name = "trialReminderRunId")]
    pub trial_reminder_run_id: Option<String>,
    #[sea_orm(column_name = "trialExpiredRunId")]
    pub trial_expired_run_id: Option<String>,
    pub limits: Option<Json>,
    #[sea_orm(column_name = "enableExcelAdvancedMode", default_value = false)]
    pub enable_excel_advanced_mode: bool,
    #[sea_orm(column_name = "replicateDataroomFolders", default_value = true)]
    pub replicate_dataroom_folders: bool,
    #[sea_orm(default_value = "Etc/UTC")]
    pub timezone: String,
    #[sea_orm(column_name = "ssoEnabled", default_value = false)]
    pub sso_enabled: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "ssoEmailDomain")]
    pub sso_email_domain: Option<String>,
    #[sea_orm(column_name = "ssoEnforcedAt")]
    pub sso_enforced_at: Option<DateTime>,
    #[sea_orm(column_name = "surveyData")]
    pub survey_data: Option<Json>,
    #[sea_orm(column_name = "agentsEnabled", default_value = false)]
    pub agents_enabled: bool,
    #[sea_orm(column_name = "vectorStoreId")]
    pub vector_store_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "ignoredDomains")]
    pub ignored_domains: Vec<String>,
    #[sea_orm(column_name = "globalBlockList")]
    pub global_block_list: Vec<String>,
    #[sea_orm(column_name = "referralTrack")]
    pub referral_track: Option<ReferralTrack>,
    #[sea_orm(column_name = "referralTrackSelectedAt")]
    pub referral_track_selected_at: Option<DateTime>,
    #[sea_orm(column_name = "referralProfile")]
    pub referral_profile: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::brand::Entity",
        from = "Column::DefaultBrandId",
        to = "super::brand::Column::Id",
        on_delete = "SetNull"
    )]
    DefaultBrand,
    #[sea_orm(has_many = "super::user_team::Entity")]
    Users,
    #[sea_orm(has_many = "super::user_dataroom::Entity")]
    UserDatarooms,
    #[sea_orm(has_many = "super::document::Entity")]
    Documents,
    #[sea_orm(has_many = "super::folder::Entity")]
    Folders,
    #[sea_orm(has_many = "super::domain::Entity")]
    Domains,
    #[sea_orm(has_many = "super::invitation::Entity")]
    Invitations,
    #[sea_orm(has_many = "super::sent_email::Entity")]
    SentEmails,
    #[sea_orm(
        has_many = "super::brand::Entity",
        from = "Column::Id",
        to = "super::brand::Column::TeamId"
    )]
    Brands,
    #[sea_orm(has_many = "super::dataroom::Entity")]
    Datarooms,
    #[sea_orm(has_many = "super::agreement::Entity")]
    Agreements,
    #[sea_orm(has_many = "super::viewer_group::Entity")]
    ViewerGroups,
    #[sea_orm(has_many = "super::visitor_group::Entity")]
    VisitorGroups,
    #[sea_orm(has_many = "super::viewer::Entity")]
    Viewers,
    #[sea_orm(has_many = "super::permission_group::Entity")]
    PermissionGroups,
    #[sea_orm(has_many = "super::link_preset::Entity")]
    LinkPresets,
    #[sea_orm(has_many = "super::incoming_webhook::Entity")]
    IncomingWebhooks,
    #[sea_orm(has_many = "super::restricted_token::Entity")]
    RestrictedTokens,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_many = "super::dataroom_question::Entity")]
    DataroomQuestions,
    #[sea_orm(has_many = "super::dataroom_faq_item::Entity")]
    DataroomFaqItems,
    #[sea_orm(has_many = "super::document_upload::Entity")]
    UploadedDocuments,
    #[sea_orm(has_many = "super::tag::Entity")]
    Tag,
    #[sea_orm(has_many = "super::document_annotation::Entity")]
    Annotations,
    #[sea_orm(has_many = "super::document_redaction_job::Entity")]
    RedactionJobs,
    #[sea_orm(has_many = "super::installed_integration::Entity")]
    InstalledIntegrations,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::view::Entity")]
    Views,
    #[sea_orm(has_many = "super::workflow::Entity")]
    Workflows,
    #[sea_orm(has_many = "super::task_list::Entity")]
    TaskLists,
    #[sea_orm(has_many = "super::task::Entity")]
    Tasks,
    #[sea_orm(has_many = "super::chat::Entity")]
    Chats,
    #[sea_orm(has_many = "super::notification_preference::Entity")]
    NotificationPreferences,
    #[sea_orm(has_many = "super::referral_invite::Entity")]
    ReferralInvites,
}

impl Related<super::brand::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultBrand.def()
    }
}

impl Related<super::user_team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::user_dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserDatarooms.def()
    }
}

impl Related<super::document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Documents.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Domains.def()
    }
}

impl Related<super::invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::sent_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SentEmails.def()
    }
}

impl Related<super::dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Datarooms.def()
    }
}

impl Related<super::agreement::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agreements.def()
    }
}

impl Related<super::viewer_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ViewerGroups.def()
    }
}

impl Related<super::visitor_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VisitorGroups.def()
    }
}

impl Related<super::viewer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Viewers.def()
    }
}

impl Related<super::permission_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PermissionGroups.def()
    }
}

impl Related<super::link_preset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LinkPresets.def()
    }
}

impl Related<super::incoming_webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IncomingWebhooks.def()
    }
}

impl Related<super::restricted_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RestrictedTokens.def()
    }
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::dataroom_question::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataroomQuestions.def()
    }
}

impl Related<super::dataroom_faq_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DataroomFaqItems.def()
    }
}

impl Related<super::document_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadedDocuments.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tag.def()
    }
}

impl Related<super::document_annotation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Annotations.def()
    }
}

impl Related<super::document_redaction_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RedactionJobs.def()
    }
}

impl Related<super::installed_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InstalledIntegrations.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Views.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflows.def()
    }
}

impl Related<super::task_list::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskLists.def()
    }
}

impl Related<super::task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tasks.def()
    }
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chats.def()
    }
}

impl Related<super::notification_preference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationPreferences.def()
    }
}

impl Related<super::referral_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReferralInvites.def()
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
