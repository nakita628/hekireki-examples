class ScheduledAction < ApplicationRecord
  self.table_name = "ScheduledAction"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :actionType, { archive: "ARCHIVE", label: "LABEL", reply: "REPLY", send_email: "SEND_EMAIL", forward: "FORWARD", draft_email: "DRAFT_EMAIL", draft_messaging_channel: "DRAFT_MESSAGING_CHANNEL", notify_messaging_channel: "NOTIFY_MESSAGING_CHANNEL", mark_spam: "MARK_SPAM", call_webhook: "CALL_WEBHOOK", mark_read: "MARK_READ", star: "STAR", delete: "DELETE", digest: "DIGEST", move_folder: "MOVE_FOLDER", notify_sender: "NOTIFY_SENDER", integration: "INTEGRATION" },
       prefix: true, validate: true
  enum :status, { pending: "PENDING", executing: "EXECUTING", completed: "COMPLETED", failed: "FAILED", cancelled: "CANCELLED" },
       default: :pending, validate: true
  enum :schedulingStatus, { pending: "PENDING", scheduled: "SCHEDULED", failed: "FAILED" }, default: :pending,
                                                                                            prefix: true, validate: true

  validates :messageId, presence: true
  validates :threadId, presence: true
  validates :scheduledFor, presence: true
  validates :executedActionId, uniqueness: { allow_nil: true }

  belongs_to :executed_rule, foreign_key: "executedRuleId", inverse_of: :scheduled_actions
  belongs_to :executed_action, foreign_key: "executedActionId", optional: true, inverse_of: :scheduled_action
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :scheduled_actions
end
