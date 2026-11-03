class OrganizationIntegration < ApplicationRecord
  self.table_name = "OrganizationIntegration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :service, { slack: "SLACK", vercel: "VERCEL" }, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :integrationData, exclusion: { in: [ nil ] }

  belongs_to :token_reference, class_name: "SecretReference", foreign_key: "tokenReferenceId",
                               inverse_of: :organization_integration
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :organization_integrations
  has_many :alert_channels, class_name: "ProjectAlertChannel", foreign_key: "integrationId", inverse_of: :integration,
                            dependent: :nullify
  has_many :organization_project_integration, foreign_key: "organizationIntegrationId",
                                              inverse_of: :organization_integration, dependent: :destroy
end
