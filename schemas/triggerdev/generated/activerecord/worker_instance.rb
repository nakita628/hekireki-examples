class WorkerInstance < ApplicationRecord
  self.table_name = "WorkerInstance"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :resourceIdentifier, presence: true, uniqueness: { scope: :workerGroupId }

  belongs_to :worker_group, class_name: "WorkerInstanceGroup", foreign_key: "workerGroupId", inverse_of: :workers
  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :worker_instances
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :workers
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", optional: true,
                           inverse_of: :worker_instances
  belongs_to :deployment, class_name: "WorkerDeployment", foreign_key: "deploymentId", optional: true,
                          inverse_of: :worker_instance
end
