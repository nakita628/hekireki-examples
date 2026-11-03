class TaskIdentifier < ApplicationRecord
  self.table_name = "TaskIdentifier"

  attribute :id, default: -> { Cuid.generate }
  attribute :isInLatestDeployment, default: true

  enum :currentTriggerSource, { standard: "STANDARD", scheduled: "SCHEDULED", agent: "AGENT", webhook: "WEBHOOK" },
       default: :standard, validate: true

  validates :slug, presence: true, uniqueness: { scope: :runtimeEnvironmentId }

  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :task_identifiers
  belongs_to :project, foreign_key: "projectId", inverse_of: :task_identifiers
  belongs_to :current_worker, class_name: "BackgroundWorker", foreign_key: "currentWorkerId", optional: true,
                              inverse_of: :task_identifiers
end
