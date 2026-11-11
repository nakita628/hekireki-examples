class LiteLLMClaudeCodePluginVersion < ApplicationRecord
  self.table_name = "LiteLLM_ClaudeCodePluginVersion"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :archive_available, default: false

  validates :version, uniqueness: { scope: :plugin_id, allow_nil: true }

  belongs_to :plugin, class_name: "LiteLLMClaudeCodePluginTable", inverse_of: :versions
end
