class EmailAccount < ApplicationRecord
  self.table_name = "EmailAccount"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :includeReferralSignature, default: false
  attribute :includeSentWithSignature, default: true
  attribute :sentMessageOpenTrackingEnabled, default: true
  attribute :draftDocsUrls, default: -> { [] }
  attribute :coldEmailDigest, default: false
  attribute :autoCategorizeSenders, default: false
  attribute :multiRuleSelectionEnabled, default: false
  attribute :draftCleanupDays, default: 14
  attribute :allowHiddenAiDraftLinks, default: false
  attribute :rulesRevision, default: 0
  attribute :mailExpandedPreview, default: false
  attribute :includeInAllAccounts, default: true
  attribute :meetingBriefingsEnabled, default: false
  attribute :meetingBriefingsMinutesBefore, default: 240
  attribute :meetingBriefsSendEmail, default: true
  attribute :meetingRecorderEnabled, default: false
  attribute :meetingRecorderRecapEmailEnabled, default: true
  attribute :meetingRecorderFollowUpDraftEnabled, default: true
  attribute :filingEnabled, default: false
  attribute :filingConfirmationSendEmail, default: true
  attribute :followUpAutoDraftEnabled, default: false
  attribute :digestSendEmail, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :statsEmailFrequency, { never: "NEVER", daily: "DAILY", weekly: "WEEKLY" }, default: :weekly, validate: true
  enum :summaryEmailFrequency, { never: "NEVER", daily: "DAILY", weekly: "WEEKLY" }, default: :weekly, prefix: true,
                                                                                     validate: true
  enum :coldEmailBlocker, { disabled: "DISABLED", list: "LIST", label: "LABEL", archive_and_label: "ARCHIVE_AND_LABEL", archive_and_read_and_label: "ARCHIVE_AND_READ_AND_LABEL" },
       validate: { allow_nil: true }
  enum :draftReplyConfidence, { all_emails: "ALL_EMAILS", standard: "STANDARD", high_confidence: "HIGH_CONFIDENCE" },
       default: :all_emails, validate: true
  enum :mailLayout, { list: "LIST", split: "SPLIT" }, default: :list, prefix: true, validate: true
  enum :mailSetup, { pending: "PENDING", suggested: "SUGGESTED", done: "DONE" }, default: :pending, validate: true
  enum :meetingRecorderJoinRule, { all: "ALL", external_only: "EXTERNAL_ONLY", host_only: "HOST_ONLY", off: "OFF" },
       default: :all, prefix: true, validate: true

  validates :email, presence: true, uniqueness: true
  validates :accountId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :email_accounts
  belongs_to :account, foreign_key: "accountId", inverse_of: :email_account
  has_one :digest_schedule, class_name: "Schedule", foreign_key: "emailAccountId", inverse_of: :email_account,
                            dependent: :destroy
  has_many :labels, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :rules, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :executed_rules, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :newsletters, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :cold_emails, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :groups, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :categories, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :thread_trackers, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :cleanup_jobs, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :bulk_inbox_action_jobs, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :cleanup_threads, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :email_messages, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :email_tokens, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :knowledge, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :snippets, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :email_signatures, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :reply_memories, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :chats, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :chat_memories, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :digests, class_name: "DigestRecord", foreign_key: "emailAccountId", inverse_of: :email_account,
                     dependent: :destroy
  has_many :scheduled_actions, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :snoozed_threads, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :email_send_operations, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :sent_message_opens, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :scheduled_emails, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :response_times, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :actions, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :members, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :restrict_with_error
  has_many :invitations, foreign_key: "inviterId", inverse_of: :inviter, dependent: :destroy
  has_many :ssoproviders, class_name: "SsoProvider", foreign_key: "emailAccountId", inverse_of: :email_account,
                          dependent: :destroy
  has_many :calendar_connections, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :availability_schedules, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :booking_links, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :bookings, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :mcp_connections, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :mcp_integrations, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :meeting_briefings, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :meetings, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :meeting_recordings, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :drive_connections, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :messaging_channels, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :document_filings, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :filing_folders, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :api_keys, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :classification_feedback, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
  has_many :mail_splits, foreign_key: "emailAccountId", inverse_of: :email_account, dependent: :destroy
end
