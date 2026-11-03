class TaskRunWaitpoint < ApplicationRecord
  self.table_name = "TaskRunWaitpoint"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :batchIndex, uniqueness: { scope: [ :taskRunId, :waitpointId ], allow_nil: true }

  belongs_to :task_run, foreign_key: "taskRunId", inverse_of: :blocked_by_waitpoints
  belongs_to :waitpoint, foreign_key: "waitpointId", inverse_of: :blocking_task_runs
  belongs_to :project, foreign_key: "projectId", inverse_of: :task_run_waitpoints
  belongs_to :batch, class_name: "BatchTaskRun", foreign_key: "batchId", optional: true, inverse_of: :runs_blocked
end
