class SnoozedThread < ApplicationRecord
  self.table_name = "SnoozedThread"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedAt

  enum :status, { preparing: "PREPARING", pending: "PENDING", executing: "EXECUTING", completed: "COMPLETED", cancelled: "CANCELLED" },
       default: :pending, validate: true

  validates :threadId, presence: true
  validates :scheduledFor, presence: true
  validates :clientMutationId, uniqueness: { scope: :emailAccountId, allow_nil: true }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :snoozed_threads
end
