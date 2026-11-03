class TaskRunDependency < ApplicationRecord
  self.table_name = "TaskRunDependency"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :taskRunId, uniqueness: true
  validates :checkpointEventId, uniqueness: { allow_nil: true }

  belongs_to :task_run, foreign_key: "taskRunId", inverse_of: :dependency
  belongs_to :checkpoint_event, class_name: "CheckpointRestoreEvent", foreign_key: "checkpointEventId", optional: true,
                                inverse_of: :task_run_dependency
  belongs_to :dependent_attempt, class_name: "TaskRunAttempt", foreign_key: "dependentAttemptId", optional: true,
                                 inverse_of: :dependencies
  belongs_to :dependent_batch_run, class_name: "BatchTaskRun", foreign_key: "dependentBatchRunId", optional: true,
                                   inverse_of: :run_dependencies
end
