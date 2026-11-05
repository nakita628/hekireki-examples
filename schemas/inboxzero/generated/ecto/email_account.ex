defmodule InboxZero.EmailAccount do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          name: String.t() | nil,
          image: String.t() | nil,
          sort_order: integer() | nil,
          about: String.t() | nil,
          writing_style: String.t() | nil,
          signature: String.t() | nil,
          sent_signature_scan: map() | nil,
          sent_signature_scanned_at: DateTime.t() | nil,
          include_referral_signature: boolean(),
          include_sent_with_signature: boolean(),
          sent_message_open_tracking_enabled: boolean(),
          watch_emails_expiration_date: DateTime.t() | nil,
          watch_emails_subscription_id: String.t() | nil,
          watch_emails_subscription_history: map() | nil,
          last_synced_history_id: String.t() | nil,
          behavior_profile: map() | nil,
          persona_analysis: map() | nil,
          role: String.t() | nil,
          timezone: String.t() | nil,
          calendar_booking_link: String.t() | nil,
          draft_docs_urls: [String.t()],
          stats_email_frequency: atom(),
          summary_email_frequency: atom(),
          last_summary_email_at: DateTime.t() | nil,
          last_inbox_health_email_at: DateTime.t() | nil,
          cold_email_blocker: atom() | nil,
          cold_email_digest: boolean(),
          cold_email_prompt: String.t() | nil,
          rules_prompt: String.t() | nil,
          auto_categorize_senders: boolean(),
          multi_rule_selection_enabled: boolean(),
          sensitive_data_policy: String.t() | nil,
          draft_reply_confidence: atom(),
          draft_cleanup_days: integer() | nil,
          allow_hidden_ai_draft_links: boolean(),
          rules_revision: integer(),
          mail_layout: atom(),
          mail_expanded_preview: boolean(),
          include_in_all_accounts: boolean(),
          meeting_briefings_enabled: boolean(),
          meeting_briefings_minutes_before: integer(),
          meeting_briefs_send_email: boolean(),
          meeting_recorder_enabled: boolean(),
          meeting_recorder_join_rule: atom(),
          meeting_recorder_recap_email_enabled: boolean(),
          meeting_recorder_follow_up_draft_enabled: boolean(),
          filing_enabled: boolean(),
          filing_prompt: String.t() | nil,
          filing_confirmation_send_email: boolean(),
          follow_up_awaiting_reply_days: float() | nil,
          follow_up_needs_reply_days: float() | nil,
          follow_up_auto_draft_enabled: boolean(),
          digest_send_email: boolean(),
          learned_writing_style: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil,
          account: InboxZero.Account.t() | nil,
          digest_schedule: InboxZero.Schedule.t() | nil,
          labels: [InboxZero.Label.t()],
          rules: [InboxZero.Rule.t()],
          executed_rules: [InboxZero.ExecutedRule.t()],
          newsletters: [InboxZero.Newsletter.t()],
          cold_emails: [InboxZero.ColdEmail.t()],
          groups: [InboxZero.Group.t()],
          categories: [InboxZero.Category.t()],
          thread_trackers: [InboxZero.ThreadTracker.t()],
          cleanup_jobs: [InboxZero.CleanupJob.t()],
          cleanup_threads: [InboxZero.CleanupThread.t()],
          email_messages: [InboxZero.EmailMessage.t()],
          email_tokens: [InboxZero.EmailToken.t()],
          knowledge: [InboxZero.Knowledge.t()],
          snippets: [InboxZero.Snippet.t()],
          email_signatures: [InboxZero.EmailSignature.t()],
          reply_memories: [InboxZero.ReplyMemory.t()],
          chats: [InboxZero.Chat.t()],
          chat_memories: [InboxZero.ChatMemory.t()],
          digests: [InboxZero.Digest.t()],
          scheduled_actions: [InboxZero.ScheduledAction.t()],
          snoozed_threads: [InboxZero.SnoozedThread.t()],
          email_send_operations: [InboxZero.EmailSendOperation.t()],
          sent_message_opens: [InboxZero.SentMessageOpen.t()],
          scheduled_emails: [InboxZero.ScheduledEmail.t()],
          response_times: [InboxZero.ResponseTime.t()],
          actions: [InboxZero.Action.t()],
          members: [InboxZero.Member.t()],
          invitations: [InboxZero.Invitation.t()],
          ssoproviders: [InboxZero.SsoProvider.t()],
          calendar_connections: [InboxZero.CalendarConnection.t()],
          availability_schedules: [InboxZero.AvailabilitySchedule.t()],
          booking_links: [InboxZero.BookingLink.t()],
          bookings: [InboxZero.Booking.t()],
          mcp_connections: [InboxZero.McpConnection.t()],
          mcp_integrations: [InboxZero.McpIntegration.t()],
          meeting_briefings: [InboxZero.MeetingBriefing.t()],
          meetings: [InboxZero.Meeting.t()],
          meeting_recordings: [InboxZero.MeetingRecording.t()],
          drive_connections: [InboxZero.DriveConnection.t()],
          messaging_channels: [InboxZero.MessagingChannel.t()],
          document_filings: [InboxZero.DocumentFiling.t()],
          filing_folders: [InboxZero.FilingFolder.t()],
          api_keys: [InboxZero.ApiKey.t()],
          classification_feedback: [InboxZero.ClassificationFeedback.t()],
          mail_splits: [InboxZero.MailSplit.t()]
        }

  schema "EmailAccount" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:name, :string)
    field(:image, :string)
    field(:sort_order, :integer, source: :sortOrder)
    field(:about, :string)
    field(:writing_style, :string, source: :writingStyle)
    field(:signature, :string)
    field(:sent_signature_scan, :map, source: :sentSignatureScan)
    field(:sent_signature_scanned_at, InboxZero.PrismaDateTime, source: :sentSignatureScannedAt)
    field(:include_referral_signature, :boolean, default: false, source: :includeReferralSignature)
    field(:include_sent_with_signature, :boolean, default: true, source: :includeSentWithSignature)
    field(:sent_message_open_tracking_enabled, :boolean, default: true, source: :sentMessageOpenTrackingEnabled)
    field(:watch_emails_expiration_date, InboxZero.PrismaDateTime, source: :watchEmailsExpirationDate)
    field(:watch_emails_subscription_id, :string, source: :watchEmailsSubscriptionId)
    field(:watch_emails_subscription_history, :map, source: :watchEmailsSubscriptionHistory)
    field(:last_synced_history_id, :string, source: :lastSyncedHistoryId)
    field(:behavior_profile, :map, source: :behaviorProfile)
    field(:persona_analysis, :map, source: :personaAnalysis)
    field(:role, :string)
    field(:timezone, :string)
    field(:calendar_booking_link, :string, source: :calendarBookingLink)
    field(:draft_docs_urls, {:array, :string}, source: :draftDocsUrls)
    field(:stats_email_frequency, Ecto.Enum, values: [:NEVER, :DAILY, :WEEKLY], default: :WEEKLY, source: :statsEmailFrequency)
    field(:summary_email_frequency, Ecto.Enum, values: [:NEVER, :DAILY, :WEEKLY], default: :WEEKLY, source: :summaryEmailFrequency)
    field(:last_summary_email_at, InboxZero.PrismaDateTime, source: :lastSummaryEmailAt)
    field(:last_inbox_health_email_at, InboxZero.PrismaDateTime, source: :lastInboxHealthEmailAt)
    field(:cold_email_blocker, Ecto.Enum, values: [:DISABLED, :LIST, :LABEL, :ARCHIVE_AND_LABEL, :ARCHIVE_AND_READ_AND_LABEL], source: :coldEmailBlocker)
    field(:cold_email_digest, :boolean, default: false, source: :coldEmailDigest)
    field(:cold_email_prompt, :string, source: :coldEmailPrompt)
    field(:rules_prompt, :string, source: :rulesPrompt)
    field(:auto_categorize_senders, :boolean, default: false, source: :autoCategorizeSenders)
    field(:multi_rule_selection_enabled, :boolean, default: false, source: :multiRuleSelectionEnabled)
    field(:sensitive_data_policy, :string, source: :sensitiveDataPolicy)
    field(:draft_reply_confidence, Ecto.Enum, values: [:ALL_EMAILS, :STANDARD, :HIGH_CONFIDENCE], default: :ALL_EMAILS, source: :draftReplyConfidence)
    field(:draft_cleanup_days, :integer, default: 14, source: :draftCleanupDays)
    field(:allow_hidden_ai_draft_links, :boolean, default: false, source: :allowHiddenAiDraftLinks)
    field(:rules_revision, :integer, default: 0, source: :rulesRevision)
    field(:mail_layout, Ecto.Enum, values: [:LIST, :SPLIT], default: :LIST, source: :mailLayout)
    field(:mail_expanded_preview, :boolean, default: false, source: :mailExpandedPreview)
    field(:include_in_all_accounts, :boolean, default: true, source: :includeInAllAccounts)
    field(:meeting_briefings_enabled, :boolean, default: false, source: :meetingBriefingsEnabled)
    field(:meeting_briefings_minutes_before, :integer, default: 240, source: :meetingBriefingsMinutesBefore)
    field(:meeting_briefs_send_email, :boolean, default: true, source: :meetingBriefsSendEmail)
    field(:meeting_recorder_enabled, :boolean, default: false, source: :meetingRecorderEnabled)
    field(:meeting_recorder_join_rule, Ecto.Enum, values: [:ALL, :EXTERNAL_ONLY, :HOST_ONLY, :OFF], default: :ALL, source: :meetingRecorderJoinRule)
    field(:meeting_recorder_recap_email_enabled, :boolean, default: true, source: :meetingRecorderRecapEmailEnabled)
    field(:meeting_recorder_follow_up_draft_enabled, :boolean, default: true, source: :meetingRecorderFollowUpDraftEnabled)
    field(:filing_enabled, :boolean, default: false, source: :filingEnabled)
    field(:filing_prompt, :string, source: :filingPrompt)
    field(:filing_confirmation_send_email, :boolean, default: true, source: :filingConfirmationSendEmail)
    field(:follow_up_awaiting_reply_days, :float, source: :followUpAwaitingReplyDays)
    field(:follow_up_needs_reply_days, :float, source: :followUpNeedsReplyDays)
    field(:follow_up_auto_draft_enabled, :boolean, default: true, source: :followUpAutoDraftEnabled)
    field(:digest_send_email, :boolean, default: true, source: :digestSendEmail)
    field(:learned_writing_style, :string, source: :learnedWritingStyle)
    field(:user_id, :string, source: :userId)
    field(:account_id, :string, source: :accountId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:account, InboxZero.Account, foreign_key: :account_id, define_field: false, type: :string)
    has_one(:digest_schedule, InboxZero.Schedule, foreign_key: :email_account_id, references: :id)
    has_many(:labels, InboxZero.Label, foreign_key: :email_account_id, references: :id)
    has_many(:rules, InboxZero.Rule, foreign_key: :email_account_id, references: :id)
    has_many(:executed_rules, InboxZero.ExecutedRule, foreign_key: :email_account_id, references: :id)
    has_many(:newsletters, InboxZero.Newsletter, foreign_key: :email_account_id, references: :id)
    has_many(:cold_emails, InboxZero.ColdEmail, foreign_key: :email_account_id, references: :id)
    has_many(:groups, InboxZero.Group, foreign_key: :email_account_id, references: :id)
    has_many(:categories, InboxZero.Category, foreign_key: :email_account_id, references: :id)
    has_many(:thread_trackers, InboxZero.ThreadTracker, foreign_key: :email_account_id, references: :id)
    has_many(:cleanup_jobs, InboxZero.CleanupJob, foreign_key: :email_account_id, references: :id)
    has_many(:cleanup_threads, InboxZero.CleanupThread, foreign_key: :email_account_id, references: :id)
    has_many(:email_messages, InboxZero.EmailMessage, foreign_key: :email_account_id, references: :id)
    has_many(:email_tokens, InboxZero.EmailToken, foreign_key: :email_account_id, references: :id)
    has_many(:knowledge, InboxZero.Knowledge, foreign_key: :email_account_id, references: :id)
    has_many(:snippets, InboxZero.Snippet, foreign_key: :email_account_id, references: :id)
    has_many(:email_signatures, InboxZero.EmailSignature, foreign_key: :email_account_id, references: :id)
    has_many(:reply_memories, InboxZero.ReplyMemory, foreign_key: :email_account_id, references: :id)
    has_many(:chats, InboxZero.Chat, foreign_key: :email_account_id, references: :id)
    has_many(:chat_memories, InboxZero.ChatMemory, foreign_key: :email_account_id, references: :id)
    has_many(:digests, InboxZero.Digest, foreign_key: :email_account_id, references: :id)
    has_many(:scheduled_actions, InboxZero.ScheduledAction, foreign_key: :email_account_id, references: :id)
    has_many(:snoozed_threads, InboxZero.SnoozedThread, foreign_key: :email_account_id, references: :id)
    has_many(:email_send_operations, InboxZero.EmailSendOperation, foreign_key: :email_account_id, references: :id)
    has_many(:sent_message_opens, InboxZero.SentMessageOpen, foreign_key: :email_account_id, references: :id)
    has_many(:scheduled_emails, InboxZero.ScheduledEmail, foreign_key: :email_account_id, references: :id)
    has_many(:response_times, InboxZero.ResponseTime, foreign_key: :email_account_id, references: :id)
    has_many(:actions, InboxZero.Action, foreign_key: :email_account_id, references: :id)
    has_many(:members, InboxZero.Member, foreign_key: :email_account_id, references: :id)
    has_many(:invitations, InboxZero.Invitation, foreign_key: :inviter_id, references: :id)
    has_many(:ssoproviders, InboxZero.SsoProvider, foreign_key: :email_account_id, references: :id)
    has_many(:calendar_connections, InboxZero.CalendarConnection, foreign_key: :email_account_id, references: :id)
    has_many(:availability_schedules, InboxZero.AvailabilitySchedule, foreign_key: :email_account_id, references: :id)
    has_many(:booking_links, InboxZero.BookingLink, foreign_key: :email_account_id, references: :id)
    has_many(:bookings, InboxZero.Booking, foreign_key: :email_account_id, references: :id)
    has_many(:mcp_connections, InboxZero.McpConnection, foreign_key: :email_account_id, references: :id)
    has_many(:mcp_integrations, InboxZero.McpIntegration, foreign_key: :email_account_id, references: :id)
    has_many(:meeting_briefings, InboxZero.MeetingBriefing, foreign_key: :email_account_id, references: :id)
    has_many(:meetings, InboxZero.Meeting, foreign_key: :email_account_id, references: :id)
    has_many(:meeting_recordings, InboxZero.MeetingRecording, foreign_key: :email_account_id, references: :id)
    has_many(:drive_connections, InboxZero.DriveConnection, foreign_key: :email_account_id, references: :id)
    has_many(:messaging_channels, InboxZero.MessagingChannel, foreign_key: :email_account_id, references: :id)
    has_many(:document_filings, InboxZero.DocumentFiling, foreign_key: :email_account_id, references: :id)
    has_many(:filing_folders, InboxZero.FilingFolder, foreign_key: :email_account_id, references: :id)
    has_many(:api_keys, InboxZero.ApiKey, foreign_key: :email_account_id, references: :id)
    has_many(:classification_feedback, InboxZero.ClassificationFeedback, foreign_key: :email_account_id, references: :id)
    has_many(:mail_splits, InboxZero.MailSplit, foreign_key: :email_account_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end