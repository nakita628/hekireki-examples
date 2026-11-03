class WorkflowStepLog < ApplicationRecord
  self.table_name = "WorkflowStepLog"

  attribute :id, default: -> { Cuid.generate }

  validates :conditionsMatched, inclusion: { in: [ true, false ] }

  belongs_to :execution, class_name: "WorkflowExecution", foreign_key: "executionId", inverse_of: :step_logs
  belongs_to :step, class_name: "WorkflowStep", foreign_key: "workflowStepId", inverse_of: :execution_logs
end
