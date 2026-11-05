class McpIntegration < ApplicationRecord
  self.table_name = "McpIntegration"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :authType, { oauth: "OAUTH", api_token: "API_TOKEN", none: "NONE" }, prefix: true, validate: { allow_nil: true }

  validates :name, presence: true, uniqueness: true

  belongs_to :email_account, foreign_key: "emailAccountId", optional: true, inverse_of: :mcp_integrations
  has_many :connections, class_name: "McpConnection", foreign_key: "integrationId", inverse_of: :integration,
                         dependent: :destroy
end
