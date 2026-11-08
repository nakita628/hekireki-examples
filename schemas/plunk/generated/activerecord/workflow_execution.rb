class WorkflowExecution < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { running: "RUNNING", waiting: "WAITING", completed: "COMPLETED", exited: "EXITED", failed: "FAILED", cancelled: "CANCELLED" },
       default: :running, validate: true

  belongs_to :workflow, foreign_key: "workflowId", inverse_of: :executions
  belongs_to :contact, foreign_key: "contactId", inverse_of: :workflow_executions
  belongs_to :current_step, class_name: "WorkflowStep", foreign_key: "currentStepId", optional: true,
                            inverse_of: :current_workflow_executions
  has_many :step_executions, class_name: "WorkflowStepExecution", foreign_key: "executionId", inverse_of: :execution,
                             dependent: :destroy
  has_many :emails, foreign_key: "workflowExecutionId", inverse_of: :workflow_execution, dependent: :destroy
end
