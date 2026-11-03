class TaskQueue < ApplicationRecord
  self.table_name = "TaskQueue"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :paused, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { virtual: "VIRTUAL", named: "NAMED" }, default: :virtual, validate: true
  enum :version, { v1: "V1", v2: "V2" }, default: :v1, validate: true
  enum :concurrencyVersion, { v1: "V1", v2: "V2" }, default: :v1, prefix: true, validate: true
  enum :role, { queue: "QUEUE", limit: "LIMIT" }, default: :queue, prefix: true, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :name, presence: true, uniqueness: { scope: :runtimeEnvironmentId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :task_queues
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :task_queues
  has_many :attempts, class_name: "TaskRunAttempt", foreign_key: "queueId", inverse_of: :queue, dependent: :destroy
  has_many :tasks, class_name: "BackgroundWorkerTask", foreign_key: "queueId", inverse_of: :queue, dependent: :nullify
  has_and_belongs_to_many :workers, class_name: "BackgroundWorker", join_table: "_BackgroundWorkerToTaskQueue",
                                    foreign_key: "B", association_foreign_key: "A"
end
