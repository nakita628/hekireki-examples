class BackgroundWorker < ApplicationRecord
  self.table_name = "BackgroundWorker"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :sdkVersion, default: "unknown"
  attribute :cliVersion, default: "unknown"
  attribute :supportsLazyAttempts, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :engine, { v1: "V1", v2: "V2" }, default: :v1, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :contentHash, presence: true
  validates :version, presence: true, uniqueness: { scope: [ :projectId, :runtimeEnvironmentId ] }
  validates :metadata, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :background_workers
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :background_workers
  belongs_to :worker_group, class_name: "WorkerInstanceGroup", foreign_key: "workerGroupId", optional: true,
                            inverse_of: :background_workers
  has_one :deployment, class_name: "WorkerDeployment", foreign_key: "workerId", inverse_of: :worker, dependent: :destroy
  has_many :tasks, class_name: "BackgroundWorkerTask", foreign_key: "workerId", inverse_of: :worker, dependent: :destroy
  has_many :attempts, class_name: "TaskRunAttempt", foreign_key: "backgroundWorkerId", inverse_of: :background_worker,
                      dependent: :destroy
  has_many :locked_runs, class_name: "TaskRun", foreign_key: "lockedToVersionId", inverse_of: :locked_to_version,
                         dependent: :nullify
  has_many :prompt_versions, foreign_key: "workerId", inverse_of: :worker, dependent: :nullify
  has_many :task_identifiers, foreign_key: "currentWorkerId", inverse_of: :current_worker, dependent: :nullify
  has_and_belongs_to_many :files, class_name: "BackgroundWorkerFile",
                                  join_table: "_BackgroundWorkerToBackgroundWorkerFile", foreign_key: "A",
                                  association_foreign_key: "B"
  has_and_belongs_to_many :queues, class_name: "TaskQueue", join_table: "_BackgroundWorkerToTaskQueue", foreign_key: "A",
                                   association_foreign_key: "B"
end
