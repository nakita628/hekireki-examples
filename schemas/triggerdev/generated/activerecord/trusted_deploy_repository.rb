class TrustedDeployRepository < ApplicationRecord
  self.table_name = "TrustedDeployRepository"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :issuer, default: "https://token.actions.githubusercontent.com"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, uniqueness: true
  validates :repositoryOwner, presence: true
  validates :repositoryName, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :trusted_deploy_repository
  has_many :environments, class_name: "TrustedDeployEnvironment", foreign_key: "trustedDeployRepositoryId",
                          inverse_of: :trusted_deploy_repository, dependent: :destroy
end
