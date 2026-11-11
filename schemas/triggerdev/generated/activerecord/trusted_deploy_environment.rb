class TrustedDeployEnvironment < ApplicationRecord
  self.table_name = "TrustedDeployEnvironment"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: false
  attribute :additionalClaims, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scope, { any: "ANY", branch: "BRANCH", tag: "TAG", environment: "ENVIRONMENT" }, validate: true

  validates :runtimeEnvironmentId, uniqueness: true

  belongs_to :trusted_deploy_repository, foreign_key: "trustedDeployRepositoryId", inverse_of: :environments
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :trusted_deploy_grant
end
