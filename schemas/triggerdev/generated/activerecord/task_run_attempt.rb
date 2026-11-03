class TaskRunAttempt < ApplicationRecord
  self.table_name = "TaskRunAttempt"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :number, default: 0
  attribute :usageDurationMs, default: 0
  attribute :outputType, default: "application/json"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", executing: "EXECUTING", paused: "PAUSED", failed: "FAILED", canceled: "CANCELED", completed: "COMPLETED" },
       default: :pending, validate: true

  validates :number, uniqueness: { scope: :taskRunId }
  validates :friendlyId, presence: true, uniqueness: true

  belongs_to :task_run, foreign_key: "taskRunId", inverse_of: :attempts
  belongs_to :background_worker, foreign_key: "backgroundWorkerId", inverse_of: :attempts
  belongs_to :background_worker_task, foreign_key: "backgroundWorkerTaskId", inverse_of: :attempts
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :task_run_attempts
  belongs_to :queue, class_name: "TaskQueue", foreign_key: "queueId", inverse_of: :attempts
  has_many :dependencies, class_name: "TaskRunDependency", foreign_key: "dependentAttemptId",
                          inverse_of: :dependent_attempt, dependent: :nullify
  has_many :batch_dependencies, class_name: "BatchTaskRun", foreign_key: "dependentTaskAttemptId",
                                inverse_of: :dependent_task_attempt, dependent: :destroy
  has_many :checkpoints, foreign_key: "attemptId", inverse_of: :attempt, dependent: :destroy
  has_many :batch_task_run_items, foreign_key: "taskRunAttemptId", inverse_of: :task_run_attempt, dependent: :nullify
  has_many :checkpoint_restore_event, foreign_key: "attemptId", inverse_of: :attempt, dependent: :destroy
  has_many :child_runs, class_name: "TaskRun", foreign_key: "parentTaskRunAttemptId",
                        inverse_of: :parent_task_run_attempt, dependent: :nullify
end
