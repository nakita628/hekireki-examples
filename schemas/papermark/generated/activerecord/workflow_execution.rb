class WorkflowExecution < ApplicationRecord
  self.table_name = "WorkflowExecution"

  attribute :id, default: -> { Cuid.generate }

  enum :status, { pending: "PENDING", in_progress: "IN_PROGRESS", completed: "COMPLETED", failed: "FAILED", blocked: "BLOCKED" },
       validate: true

  belongs_to :workflow, foreign_key: "workflowId", inverse_of: :executions
  has_many :step_logs, class_name: "WorkflowStepLog", foreign_key: "executionId", inverse_of: :execution,
                       dependent: :destroy
end
