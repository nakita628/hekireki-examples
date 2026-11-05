class McpTool < ApplicationRecord
  self.table_name = "McpTool"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isEnabled, default: true
  attribute :isWrite, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :connectionId }

  belongs_to :connection, class_name: "McpConnection", foreign_key: "connectionId", inverse_of: :tools
end
