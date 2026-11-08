class WorkflowStepExecution < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", scheduled: "SCHEDULED", waiting: "WAITING", running: "RUNNING", completed: "COMPLETED", skipped: "SKIPPED", failed: "FAILED" },
       default: :pending, validate: true

  belongs_to :execution, class_name: "WorkflowExecution", foreign_key: "executionId", inverse_of: :step_executions
  belongs_to :step, class_name: "WorkflowStep", foreign_key: "stepId", inverse_of: :executions
  has_many :emails, foreign_key: "workflowStepExecutionId", inverse_of: :workflow_step_execution, dependent: :destroy
end
