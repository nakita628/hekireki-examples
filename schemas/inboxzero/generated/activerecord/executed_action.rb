class ExecutedAction < ApplicationRecord
  self.table_name = "ExecutedAction"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :sentMessageIds, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { archive: "ARCHIVE", label: "LABEL", reply: "REPLY", send_email: "SEND_EMAIL", forward: "FORWARD", draft_email: "DRAFT_EMAIL", draft_messaging_channel: "DRAFT_MESSAGING_CHANNEL", notify_messaging_channel: "NOTIFY_MESSAGING_CHANNEL", mark_spam: "MARK_SPAM", call_webhook: "CALL_WEBHOOK", mark_read: "MARK_READ", star: "STAR", delete: "DELETE", digest: "DIGEST", move_folder: "MOVE_FOLDER", notify_sender: "NOTIFY_SENDER", integration: "INTEGRATION" },
       prefix: true, validate: true
  enum :executionStatus, { succeeded: "SUCCEEDED", failed: "FAILED", skipped: "SKIPPED" }, validate: { allow_nil: true }
  enum :messagingMessageStatus, { sent: "SENT", draft_sent: "DRAFT_SENT", draft_edited: "DRAFT_EDITED", dismissed: "DISMISSED", expired: "EXPIRED", failed: "FAILED" },
       prefix: true, validate: { allow_nil: true }
  enum :draftStatus, { pending: "PENDING", likely_sent: "LIKELY_SENT", replied_without_draft: "REPLIED_WITHOUT_DRAFT", cleaned_up_unused: "CLEANED_UP_UNUSED", missing_from_provider: "MISSING_FROM_PROVIDER", user_edited: "USER_EDITED", cleanup_skipped: "CLEANUP_SKIPPED" },
       validate: { allow_nil: true }

  belongs_to :executed_rule, foreign_key: "executedRuleId", inverse_of: :action_items
  belongs_to :messaging_channel, foreign_key: "messagingChannelId", optional: true, inverse_of: :executed_actions
  has_one :draft_send_log, foreign_key: "executedActionId", inverse_of: :executed_action, dependent: :destroy
  has_one :scheduled_action, foreign_key: "executedActionId", inverse_of: :executed_action, dependent: :destroy
  has_many :digest_items, foreign_key: "actionId", inverse_of: :action, dependent: :destroy
end
