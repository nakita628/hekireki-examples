class ProcessManagerOutbox < ApplicationRecord
  self.table_name = "ProcessManagerOutbox"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :attempts, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "pending", dispatched: "dispatched", dead: "dead", discarded: "discarded" },
       default: :pending, validate: true

  validates :processName, presence: true, uniqueness: { scope: [ :projectId, :messageKey ] }
  validates :projectId, presence: true
  validates :processKey, presence: true
  validates :tenantId, presence: true
  validates :messageKey, presence: true
  validates :intentType, presence: true
  validates :payload, exclusion: { in: [ nil ] }
  validates :traceCarrier, exclusion: { in: [ nil ] }
  validates :nextAttemptAt, presence: true
  validates :leaseToken, uniqueness: { allow_nil: true }

  has_many :attempt_log, class_name: "ProcessManagerOutboxAttempt", foreign_key: "outboxId", inverse_of: :outbox,
                         dependent: :destroy
end
