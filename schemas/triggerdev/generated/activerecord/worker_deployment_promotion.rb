class WorkerDeploymentPromotion < ApplicationRecord
  self.table_name = "WorkerDeploymentPromotion"

  attribute :id, default: -> { Cuid.generate }

  validates :label, presence: true, uniqueness: { scope: :environmentId }

  belongs_to :deployment, class_name: "WorkerDeployment", foreign_key: "deploymentId", inverse_of: :promotions
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId",
                           inverse_of: :worker_deployment_promotions
end
