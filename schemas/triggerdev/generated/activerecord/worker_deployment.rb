class WorkerDeployment < ApplicationRecord
  self.table_name = "WorkerDeployment"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :imagePlatform, default: "linux/amd64"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", installing: "INSTALLING", building: "BUILDING", deploying: "DEPLOYING", deployed: "DEPLOYED", failed: "FAILED", canceled: "CANCELED", timed_out: "TIMED_OUT" },
       default: :pending, validate: true
  enum :type, { managed: "MANAGED", unmanaged: "UNMANAGED", v1: "V1" }, default: :v1, validate: true

  validates :contentHash, presence: true
  validates :friendlyId, presence: true, uniqueness: true
  validates :shortCode, presence: true, uniqueness: { scope: :projectId }
  validates :version, presence: true, uniqueness: { scope: :environmentId }
  validates :workerId, uniqueness: { allow_nil: true }

  belongs_to :project, foreign_key: "projectId", inverse_of: :worker_deployment
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId",
                           inverse_of: :worker_deployments
  belongs_to :worker, class_name: "BackgroundWorker", foreign_key: "workerId", optional: true, inverse_of: :deployment
  belongs_to :triggered_by, class_name: "User", foreign_key: "triggeredById", optional: true, inverse_of: :deployments
  has_many :promotions, class_name: "WorkerDeploymentPromotion", foreign_key: "deploymentId", inverse_of: :deployment,
                        dependent: :destroy
  has_many :alerts, class_name: "ProjectAlert", foreign_key: "workerDeploymentId", inverse_of: :worker_deployment,
                    dependent: :destroy
  has_many :worker_instance, class_name: "WorkerInstance", foreign_key: "deploymentId", inverse_of: :deployment,
                             dependent: :nullify
  has_many :integration_deployments, foreign_key: "deploymentId", inverse_of: :worker_deployment, dependent: :nullify
end
