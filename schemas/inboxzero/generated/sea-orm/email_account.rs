use super::cold_email_setting::ColdEmailSetting;
use super::draft_reply_confidence::DraftReplyConfidence;
use super::frequency::Frequency;
use super::mail_layout::MailLayout;
use super::meeting_join_rule::MeetingJoinRule;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EmailAccount")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: Option<String>,
    pub image: Option<String>,
    #[sea_orm(column_name = "sortOrder")]
    pub sort_order: Option<i32>,
    pub about: Option<String>,
    #[sea_orm(column_name = "writingStyle")]
    pub writing_style: Option<String>,
    pub signature: Option<String>,
    #[sea_orm(column_name = "sentSignatureScan")]
    pub sent_signature_scan: Option<Json>,
    #[sea_orm(column_name = "sentSignatureScannedAt")]
    pub sent_signature_scanned_at: Option<DateTime>,
    #[sea_orm(column_name = "includeReferralSignature", default_value = false)]
    pub include_referral_signature: bool,
    #[sea_orm(column_name = "includeSentWithSignature", default_value = true)]
    pub include_sent_with_signature: bool,
    #[sea_orm(column_name = "sentMessageOpenTrackingEnabled", default_value = true)]
    pub sent_message_open_tracking_enabled: bool,
    #[sea_orm(column_name = "watchEmailsExpirationDate")]
    pub watch_emails_expiration_date: Option<DateTime>,
    #[sea_orm(column_name = "watchEmailsSubscriptionId")]
    pub watch_emails_subscription_id: Option<String>,
    #[sea_orm(column_name = "watchEmailsSubscriptionHistory")]
    pub watch_emails_subscription_history: Option<Json>,
    #[sea_orm(column_name = "lastSyncedHistoryId")]
    pub last_synced_history_id: Option<String>,
    #[sea_orm(column_name = "behaviorProfile")]
    pub behavior_profile: Option<Json>,
    #[sea_orm(column_name = "personaAnalysis")]
    pub persona_analysis: Option<Json>,
    pub role: Option<String>,
    pub timezone: Option<String>,
    #[sea_orm(column_name = "calendarBookingLink")]
    pub calendar_booking_link: Option<String>,
    #[sea_orm(column_name = "draftDocsUrls")]
    pub draft_docs_urls: Vec<String>,
    #[sea_orm(column_name = "statsEmailFrequency", default_value = "WEEKLY")]
    pub stats_email_frequency: Frequency,
    #[sea_orm(column_name = "summaryEmailFrequency", default_value = "WEEKLY")]
    pub summary_email_frequency: Frequency,
    #[sea_orm(column_name = "lastSummaryEmailAt")]
    pub last_summary_email_at: Option<DateTime>,
    #[sea_orm(column_name = "lastInboxHealthEmailAt")]
    pub last_inbox_health_email_at: Option<DateTime>,
    #[sea_orm(column_name = "coldEmailBlocker")]
    pub cold_email_blocker: Option<ColdEmailSetting>,
    #[sea_orm(column_name = "coldEmailDigest", default_value = false)]
    pub cold_email_digest: bool,
    #[sea_orm(column_name = "coldEmailPrompt")]
    pub cold_email_prompt: Option<String>,
    #[sea_orm(column_name = "rulesPrompt")]
    pub rules_prompt: Option<String>,
    #[sea_orm(column_name = "autoCategorizeSenders", default_value = false)]
    pub auto_categorize_senders: bool,
    #[sea_orm(column_name = "multiRuleSelectionEnabled", default_value = false)]
    pub multi_rule_selection_enabled: bool,
    #[sea_orm(column_name = "sensitiveDataPolicy")]
    pub sensitive_data_policy: Option<String>,
    #[sea_orm(column_name = "draftReplyConfidence", default_value = "ALL_EMAILS")]
    pub draft_reply_confidence: DraftReplyConfidence,
    #[sea_orm(column_name = "draftCleanupDays", default_value = 14)]
    pub draft_cleanup_days: Option<i32>,
    #[sea_orm(column_name = "allowHiddenAiDraftLinks", default_value = false)]
    pub allow_hidden_ai_draft_links: bool,
    #[sea_orm(column_name = "rulesRevision", default_value = 0)]
    pub rules_revision: i32,
    #[sea_orm(column_name = "mailLayout", default_value = "LIST")]
    pub mail_layout: MailLayout,
    #[sea_orm(column_name = "mailExpandedPreview", default_value = false)]
    pub mail_expanded_preview: bool,
    #[sea_orm(column_name = "mailBundleInbox")]
    pub mail_bundle_inbox: Option<bool>,
    #[sea_orm(column_name = "includeInAllAccounts", default_value = true)]
    pub include_in_all_accounts: bool,
    #[sea_orm(column_name = "meetingBriefingsEnabled", default_value = false)]
    pub meeting_briefings_enabled: bool,
    #[sea_orm(column_name = "meetingBriefingsMinutesBefore", default_value = 240)]
    pub meeting_briefings_minutes_before: i32,
    #[sea_orm(column_name = "meetingBriefsSendEmail", default_value = true)]
    pub meeting_briefs_send_email: bool,
    #[sea_orm(column_name = "meetingRecorderEnabled", default_value = false)]
    pub meeting_recorder_enabled: bool,
    #[sea_orm(column_name = "meetingRecorderJoinRule", default_value = "ALL")]
    pub meeting_recorder_join_rule: MeetingJoinRule,
    #[sea_orm(column_name = "meetingRecorderRecapEmailEnabled", default_value = true)]
    pub meeting_recorder_recap_email_enabled: bool,
    #[sea_orm(
        column_name = "meetingRecorderFollowUpDraftEnabled",
        default_value = true
    )]
    pub meeting_recorder_follow_up_draft_enabled: bool,
    #[sea_orm(column_name = "filingEnabled", default_value = false)]
    pub filing_enabled: bool,
    #[sea_orm(column_name = "filingPrompt")]
    pub filing_prompt: Option<String>,
    #[sea_orm(column_name = "filingConfirmationSendEmail", default_value = true)]
    pub filing_confirmation_send_email: bool,
    #[sea_orm(column_name = "followUpAwaitingReplyDays")]
    pub follow_up_awaiting_reply_days: Option<f64>,
    #[sea_orm(column_name = "followUpNeedsReplyDays")]
    pub follow_up_needs_reply_days: Option<f64>,
    #[sea_orm(column_name = "followUpAutoDraftEnabled", default_value = true)]
    pub follow_up_auto_draft_enabled: bool,
    #[sea_orm(column_name = "digestSendEmail", default_value = true)]
    pub digest_send_email: bool,
    #[sea_orm(column_name = "learnedWritingStyle")]
    pub learned_writing_style: Option<String>,
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "accountId")]
    pub account_id: String,
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
        belongs_to = "super::account::Entity",
        from = "Column::AccountId",
        to = "super::account::Column::Id",
        on_delete = "Cascade"
    )]
    Account,
    #[sea_orm(has_many = "super::label::Entity")]
    Labels,
    #[sea_orm(has_many = "super::rule::Entity")]
    Rules,
    #[sea_orm(has_many = "super::executed_rule::Entity")]
    ExecutedRules,
    #[sea_orm(has_many = "super::newsletter::Entity")]
    Newsletters,
    #[sea_orm(has_many = "super::cold_email::Entity")]
    ColdEmails,
    #[sea_orm(has_many = "super::group::Entity")]
    Groups,
    #[sea_orm(has_many = "super::category::Entity")]
    Categories,
    #[sea_orm(has_many = "super::thread_tracker::Entity")]
    ThreadTrackers,
    #[sea_orm(has_many = "super::cleanup_job::Entity")]
    CleanupJobs,
    #[sea_orm(has_many = "super::cleanup_thread::Entity")]
    CleanupThreads,
    #[sea_orm(has_many = "super::email_message::Entity")]
    EmailMessages,
    #[sea_orm(has_many = "super::email_token::Entity")]
    EmailTokens,
    #[sea_orm(has_many = "super::knowledge::Entity")]
    Knowledge,
    #[sea_orm(has_many = "super::snippet::Entity")]
    Snippets,
    #[sea_orm(has_many = "super::email_signature::Entity")]
    EmailSignatures,
    #[sea_orm(has_many = "super::reply_memory::Entity")]
    ReplyMemories,
    #[sea_orm(has_many = "super::chat::Entity")]
    Chats,
    #[sea_orm(has_many = "super::chat_memory::Entity")]
    ChatMemories,
    #[sea_orm(has_many = "super::digest::Entity")]
    Digests,
    #[sea_orm(has_many = "super::scheduled_action::Entity")]
    ScheduledActions,
    #[sea_orm(has_many = "super::snoozed_thread::Entity")]
    SnoozedThreads,
    #[sea_orm(has_many = "super::email_send_operation::Entity")]
    EmailSendOperations,
    #[sea_orm(has_many = "super::sent_message_open::Entity")]
    SentMessageOpens,
    #[sea_orm(has_many = "super::scheduled_email::Entity")]
    ScheduledEmails,
    #[sea_orm(has_many = "super::response_time::Entity")]
    ResponseTimes,
    #[sea_orm(has_many = "super::action::Entity")]
    Actions,
    #[sea_orm(has_many = "super::member::Entity")]
    Members,
    #[sea_orm(has_many = "super::invitation::Entity")]
    Invitations,
    #[sea_orm(has_many = "super::sso_provider::Entity")]
    Ssoproviders,
    #[sea_orm(has_many = "super::calendar_connection::Entity")]
    CalendarConnections,
    #[sea_orm(has_many = "super::availability_schedule::Entity")]
    AvailabilitySchedules,
    #[sea_orm(has_many = "super::booking_link::Entity")]
    BookingLinks,
    #[sea_orm(has_many = "super::booking::Entity")]
    Bookings,
    #[sea_orm(has_many = "super::mcp_connection::Entity")]
    McpConnections,
    #[sea_orm(has_many = "super::mcp_integration::Entity")]
    McpIntegrations,
    #[sea_orm(has_many = "super::meeting_briefing::Entity")]
    MeetingBriefings,
    #[sea_orm(has_many = "super::meeting::Entity")]
    Meetings,
    #[sea_orm(has_many = "super::meeting_recording::Entity")]
    MeetingRecordings,
    #[sea_orm(has_many = "super::drive_connection::Entity")]
    DriveConnections,
    #[sea_orm(has_many = "super::messaging_channel::Entity")]
    MessagingChannels,
    #[sea_orm(has_many = "super::document_filing::Entity")]
    DocumentFilings,
    #[sea_orm(has_many = "super::filing_folder::Entity")]
    FilingFolders,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::classification_feedback::Entity")]
    ClassificationFeedback,
    #[sea_orm(has_many = "super::mail_split::Entity")]
    MailSplits,
    #[sea_orm(has_one = "super::schedule::Entity")]
    DigestSchedule,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Account.def()
    }
}

impl Related<super::label::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Labels.def()
    }
}

impl Related<super::rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Rules.def()
    }
}

impl Related<super::executed_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExecutedRules.def()
    }
}

impl Related<super::newsletter::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Newsletters.def()
    }
}

impl Related<super::cold_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ColdEmails.def()
    }
}

impl Related<super::group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Groups.def()
    }
}

impl Related<super::category::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Categories.def()
    }
}

impl Related<super::thread_tracker::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ThreadTrackers.def()
    }
}

impl Related<super::cleanup_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CleanupJobs.def()
    }
}

impl Related<super::cleanup_thread::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CleanupThreads.def()
    }
}

impl Related<super::email_message::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailMessages.def()
    }
}

impl Related<super::email_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailTokens.def()
    }
}

impl Related<super::knowledge::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Knowledge.def()
    }
}

impl Related<super::snippet::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Snippets.def()
    }
}

impl Related<super::email_signature::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailSignatures.def()
    }
}

impl Related<super::reply_memory::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReplyMemories.def()
    }
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chats.def()
    }
}

impl Related<super::chat_memory::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ChatMemories.def()
    }
}

impl Related<super::digest::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Digests.def()
    }
}

impl Related<super::scheduled_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledActions.def()
    }
}

impl Related<super::snoozed_thread::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SnoozedThreads.def()
    }
}

impl Related<super::email_send_operation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailSendOperations.def()
    }
}

impl Related<super::sent_message_open::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SentMessageOpens.def()
    }
}

impl Related<super::scheduled_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledEmails.def()
    }
}

impl Related<super::response_time::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ResponseTimes.def()
    }
}

impl Related<super::action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Actions.def()
    }
}

impl Related<super::member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::sso_provider::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Ssoproviders.def()
    }
}

impl Related<super::calendar_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CalendarConnections.def()
    }
}

impl Related<super::availability_schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AvailabilitySchedules.def()
    }
}

impl Related<super::booking_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingLinks.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Bookings.def()
    }
}

impl Related<super::mcp_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::McpConnections.def()
    }
}

impl Related<super::mcp_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::McpIntegrations.def()
    }
}

impl Related<super::meeting_briefing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MeetingBriefings.def()
    }
}

impl Related<super::meeting::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Meetings.def()
    }
}

impl Related<super::meeting_recording::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MeetingRecordings.def()
    }
}

impl Related<super::drive_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DriveConnections.def()
    }
}

impl Related<super::messaging_channel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MessagingChannels.def()
    }
}

impl Related<super::document_filing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DocumentFilings.def()
    }
}

impl Related<super::filing_folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FilingFolders.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::classification_feedback::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ClassificationFeedback.def()
    }
}

impl Related<super::mail_split::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MailSplits.def()
    }
}

impl Related<super::schedule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DigestSchedule.def()
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
