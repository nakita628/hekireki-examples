class InstalledIntegration < ApplicationRecord
  self.table_name = "InstalledIntegration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :teamId, uniqueness: { scope: :integrationId }

  belongs_to :integration, foreign_key: "integrationId", inverse_of: :installed_integrations
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :installed_integrations
  belongs_to :team, foreign_key: "teamId", inverse_of: :installed_integrations
end
