class Checkpoint < ApplicationRecord
  self.table_name = "Checkpoint"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { docker: "DOCKER", kubernetes: "KUBERNETES" }, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :location, presence: true
  validates :imageRef, presence: true

  belongs_to :run, class_name: "TaskRun", foreign_key: "runId", inverse_of: :checkpoints
  belongs_to :attempt, class_name: "TaskRunAttempt", foreign_key: "attemptId", inverse_of: :checkpoints
  belongs_to :project, foreign_key: "projectId", inverse_of: :checkpoints
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :checkpoints
  has_many :events, class_name: "CheckpointRestoreEvent", foreign_key: "checkpointId", inverse_of: :checkpoint,
                    dependent: :destroy
end
