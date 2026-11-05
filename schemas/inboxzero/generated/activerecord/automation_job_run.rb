class AutomationJobRun < ApplicationRecord
  self.table_name = "AutomationJobRun"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :status, { pending: "PENDING", running: "RUNNING", sent: "SENT", skipped: "SKIPPED", failed: "FAILED" },
       default: :pending, validate: true

  validates :scheduledFor, presence: true, uniqueness: { scope: :automationJobId }

  belongs_to :automation_job, foreign_key: "automationJobId", inverse_of: :runs
end
