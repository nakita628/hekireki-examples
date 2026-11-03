class Waitpoint < ApplicationRecord
  self.table_name = "Waitpoint"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :outputType, default: "application/json"
  attribute :outputIsError, default: false
  attribute :tags, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { run: "RUN", datetime: "DATETIME", manual: "MANUAL", batch: "BATCH" }, validate: true
  enum :status, { pending: "PENDING", completed: "COMPLETED" }, default: :pending, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :idempotencyKey, presence: true, uniqueness: { scope: :environmentId }
  validates :userProvidedIdempotencyKey, inclusion: { in: [ true, false ] }
  validates :completedByTaskRunId, uniqueness: { allow_nil: true }

  belongs_to :completed_by_task_run, class_name: "TaskRun", foreign_key: "completedByTaskRunId", optional: true,
                                     inverse_of: :associated_waitpoint
  belongs_to :completed_by_batch, class_name: "BatchTaskRun", foreign_key: "completedByBatchId", optional: true,
                                  inverse_of: :waitpoints
  belongs_to :project, foreign_key: "projectId", inverse_of: :waitpoints
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", inverse_of: :waitpoints
  has_many :blocking_task_runs, class_name: "TaskRunWaitpoint", foreign_key: "waitpointId", inverse_of: :waitpoint,
                                dependent: :restrict_with_error
  has_and_belongs_to_many :connected_runs, class_name: "TaskRun", join_table: "_WaitpointRunConnections",
                                           foreign_key: "B", association_foreign_key: "A"
  has_and_belongs_to_many :completed_execution_snapshots, class_name: "TaskRunExecutionSnapshot",
                                                          join_table: "_completedWaitpoints", foreign_key: "B",
                                                          association_foreign_key: "A"
end
