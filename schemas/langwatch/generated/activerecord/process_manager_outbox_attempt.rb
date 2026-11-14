class ProcessManagerOutboxAttempt < ApplicationRecord
  self.table_name = "ProcessManagerOutboxAttempt"

  attribute :id, default: -> { Nanoid.generate }

  enum :outcome, { retry_scheduled: "retry_scheduled", dead: "dead" }, validate: true

  validates :projectId, presence: true
  validates :attempt, presence: true
  validates :occurredAt, presence: true
  validates :errorType, presence: true
  validates :errorMessage, presence: true

  belongs_to :outbox, class_name: "ProcessManagerOutbox", foreign_key: "outboxId", inverse_of: :attempt_log
end
