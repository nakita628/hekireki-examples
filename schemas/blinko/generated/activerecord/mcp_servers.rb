class McpServers < ApplicationRecord
  self.table_name = "mcpServers"
  self.inheritance_column = nil

  attribute :isEnabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, length: { maximum: 255 }
  validates :type, presence: true, length: { maximum: 50 }
  validates :url, length: { maximum: 500 }
end
