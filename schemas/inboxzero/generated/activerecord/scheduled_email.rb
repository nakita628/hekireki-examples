class ScheduledEmail < ApplicationRecord
  self.table_name = "ScheduledEmail"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :heldForUndo, default: false
  attribute :draftMessageIds, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", processing: "PROCESSING", sent: "SENT", blocked_auth: "BLOCKED_AUTH", uncertain: "UNCERTAIN", failed: "FAILED", cancelled: "CANCELLED" },
       default: :pending, validate: true
  enum :reminderStatus, { none: "NONE", pending: "PENDING", processing: "PROCESSING", completed: "COMPLETED", cancelled: "CANCELLED" },
       default: :none, prefix: true, validate: true

  validates :clientMutationId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :payloadHash, presence: true
  validates :payload, exclusion: { in: [ nil ] }
  validates :sendAt, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :scheduled_emails
end
