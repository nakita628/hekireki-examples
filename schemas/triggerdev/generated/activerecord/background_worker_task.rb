class BackgroundWorkerTask < ApplicationRecord
  self.table_name = "BackgroundWorkerTask"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :triggerSource, { standard: "STANDARD", scheduled: "SCHEDULED", agent: "AGENT", webhook: "WEBHOOK" },
       default: :standard, validate: true

  validates :slug, presence: true, uniqueness: { scope: :workerId }
  validates :friendlyId, presence: true, uniqueness: true
  validates :filePath, presence: true

  belongs_to :worker, class_name: "BackgroundWorker", foreign_key: "workerId", inverse_of: :tasks
  belongs_to :project, foreign_key: "projectId", inverse_of: :background_worker_tasks
  belongs_to :file, class_name: "BackgroundWorkerFile", foreign_key: "fileId", optional: true, inverse_of: :tasks
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :background_worker_tasks
  belongs_to :queue, class_name: "TaskQueue", foreign_key: "queueId", optional: true, inverse_of: :tasks
  has_many :attempts, class_name: "TaskRunAttempt", foreign_key: "backgroundWorkerTaskId",
                      inverse_of: :background_worker_task, dependent: :destroy
  has_many :runs, class_name: "TaskRun", foreign_key: "lockedById", inverse_of: :locked_by, dependent: :nullify
end
