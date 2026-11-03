class CheckpointRestoreEvent < ApplicationRecord
  self.table_name = "CheckpointRestoreEvent"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { checkpoint: "CHECKPOINT", restore: "RESTORE" }, validate: true

  belongs_to :checkpoint, foreign_key: "checkpointId", inverse_of: :events
  belongs_to :run, class_name: "TaskRun", foreign_key: "runId", inverse_of: :checkpoint_restore_event
  belongs_to :attempt, class_name: "TaskRunAttempt", foreign_key: "attemptId", inverse_of: :checkpoint_restore_event
  belongs_to :project, foreign_key: "projectId", inverse_of: :checkpoint_restore_event
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :checkpoint_restore_event
  has_one :task_run_dependency, foreign_key: "checkpointEventId", inverse_of: :checkpoint_event, dependent: :destroy
  has_one :batch_task_run_dependency, class_name: "BatchTaskRun", foreign_key: "checkpointEventId",
                                      inverse_of: :checkpoint_event, dependent: :destroy
end
