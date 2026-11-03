class IntegrationDeployment < ApplicationRecord
  self.table_name = "IntegrationDeployment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :integrationName, presence: true
  validates :integrationDeploymentId, presence: true
  validates :commitSHA, presence: true

  belongs_to :worker_deployment, foreign_key: "deploymentId", optional: true, inverse_of: :integration_deployments
end
