class LiteLLMSearchToolsTable < ApplicationRecord
  self.table_name = "LiteLLM_SearchToolsTable"
  self.primary_key = "search_tool_id"
  self.implicit_order_column = "created_at"

  attribute :search_tool_id, default: -> { SecureRandom.uuid }

  validates :search_tool_name, presence: true, uniqueness: true
  validates :litellm_params, exclusion: { in: [ nil ] }
end
