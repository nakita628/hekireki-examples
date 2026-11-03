class AutomationExecution < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :status, { completed: "COMPLETED", error: "ERROR", pending: "PENDING", cancelled: "CANCELLED" },
       default: :pending, validate: true

  validates :source_id, presence: true
  validates :input, exclusion: { in: [ nil ] }

  belongs_to :automation, inverse_of: :automation_execution
  belongs_to :trigger, inverse_of: :automation_executions
  belongs_to :action, inverse_of: :automation_executions
  belongs_to :project, inverse_of: :automation_executions
end
