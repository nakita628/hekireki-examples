class LiteLLMScheduledJobRun < ApplicationRecord
  self.table_name = "LiteLLM_ScheduledJobRun"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :status, { pending: "PENDING", running: "RUNNING", succeeded: "SUCCEEDED", failed: "FAILED", cancelled: "CANCELLED" },
       default: :pending, validate: true

  validates :job_name, presence: true
  validates :lock_group, presence: true
  validates :schedule, presence: true
  validates :scheduled_for, presence: true
end
