class BatchTaskRun < ApplicationRecord
  self.table_name = "BatchTaskRun"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :runIds, default: -> { [] }
  attribute :runCount, default: 0
  attribute :payloadType, default: "application/json"
  attribute :batchVersion, default: "v1"
  attribute :sealed, default: false
  attribute :expectedCount, default: 0
  attribute :completedCount, default: 0
  attribute :processingJobsCount, default: 0
  attribute :processingJobsExpectedCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", processing: "PROCESSING", completed: "COMPLETED", partial_failed: "PARTIAL_FAILED", aborted: "ABORTED" },
       default: :pending, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :runtimeEnvironmentId, presence: true,
                                   uniqueness: { scope: :idempotencyKey, conditions: -> { where.not(idempotencyKey: nil) } }
  validates :oneTimeUseToken, uniqueness: { allow_nil: true }
  validates :oneTimeUseToken, uniqueness: { allow_nil: true }
  validates :checkpointEventId, uniqueness: { allow_nil: true }

  belongs_to :checkpoint_event, class_name: "CheckpointRestoreEvent", foreign_key: "checkpointEventId", optional: true,
                                inverse_of: :batch_task_run_dependency
  belongs_to :dependent_task_attempt, class_name: "TaskRunAttempt", foreign_key: "dependentTaskAttemptId",
                                      optional: true, inverse_of: :batch_dependencies
  has_many :runs, class_name: "TaskRun", foreign_key: "batchId", inverse_of: :batch, dependent: :nullify
  has_many :runs_blocked, class_name: "TaskRunWaitpoint", foreign_key: "batchId", inverse_of: :batch,
                          dependent: :nullify
  has_many :waitpoints, foreign_key: "completedByBatchId", inverse_of: :completed_by_batch, dependent: :nullify
  has_many :errors, class_name: "BatchTaskRunError", foreign_key: "batchTaskRunId", inverse_of: :batch_task_run,
                    dependent: :destroy
  has_many :items, class_name: "BatchTaskRunItem", foreign_key: "batchTaskRunId", inverse_of: :batch_task_run,
                   dependent: :destroy
  has_many :run_dependencies, class_name: "TaskRunDependency", foreign_key: "dependentBatchRunId",
                              inverse_of: :dependent_batch_run, dependent: :nullify
end
