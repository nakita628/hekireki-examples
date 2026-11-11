class LiteLLMMCPToolsetTable < ApplicationRecord
  self.table_name = "LiteLLM_MCPToolsetTable"
  self.primary_key = "toolset_id"
  self.implicit_order_column = "created_at"

  attribute :toolset_id, default: -> { SecureRandom.uuid }
  attribute :tools, default: -> { JSON.parse("[]") }

  validates :toolset_name, presence: true, uniqueness: true
end
