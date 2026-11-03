class TaskRunCheckpoint < ApplicationRecord
  self.table_name = "TaskRunCheckpoint"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { docker: "DOCKER", kubernetes: "KUBERNETES", compute: "COMPUTE" }, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :location, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :task_run_checkpoints
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :task_run_checkpoints
  has_many :execution_snapshot, class_name: "TaskRunExecutionSnapshot", foreign_key: "checkpointId",
                                inverse_of: :checkpoint, dependent: :nullify
end
