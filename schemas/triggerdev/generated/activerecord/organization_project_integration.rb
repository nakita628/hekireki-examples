class OrganizationProjectIntegration < ApplicationRecord
  self.table_name = "OrganizationProjectIntegration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :externalEntityId, presence: true
  validates :integrationData, exclusion: { in: [ nil ] }

  belongs_to :organization_integration, foreign_key: "organizationIntegrationId",
                                        inverse_of: :organization_project_integration
  belongs_to :project, foreign_key: "projectId", inverse_of: :organization_project_integration
end
