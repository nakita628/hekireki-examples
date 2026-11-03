class InstalledIntegration < ApplicationRecord
  self.table_name = "InstalledIntegration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: { scope: [ :integrationId, :projectId ] }

  belongs_to :user, foreign_key: "userId", inverse_of: :installed_integrations
  belongs_to :integration, foreign_key: "integrationId", inverse_of: :installations
  belongs_to :project, foreign_key: "projectId", inverse_of: :installed_integrations
  has_many :refresh_tokens, class_name: "OAuthRefreshToken", foreign_key: "installationId",
                            inverse_of: :installed_integration, dependent: :destroy
  has_many :access_tokens, class_name: "RestrictedToken", foreign_key: "installationId",
                           inverse_of: :installed_integration, dependent: :destroy
  has_many :webhooks, foreign_key: "installationId", inverse_of: :installed_integration, dependent: :destroy
end
