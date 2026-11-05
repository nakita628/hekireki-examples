class McpConnection < ApplicationRecord
  self.table_name = "McpConnection"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isActive, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :emailAccountId, uniqueness: { scope: :integrationId, allow_nil: true }

  belongs_to :integration, class_name: "McpIntegration", foreign_key: "integrationId", inverse_of: :connections
  belongs_to :email_account, foreign_key: "emailAccountId", optional: true, inverse_of: :mcp_connections
  has_many :tools, class_name: "McpTool", foreign_key: "connectionId", inverse_of: :connection, dependent: :destroy
end
