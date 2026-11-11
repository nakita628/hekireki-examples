class LiteLLMClaudeCodePluginDownload < ApplicationRecord
  self.table_name = "LiteLLM_ClaudeCodePluginDownload"

  validates :digest, presence: true
  validates :authenticated, inclusion: { in: [ true, false ] }

  belongs_to :plugin, class_name: "LiteLLMClaudeCodePluginTable", inverse_of: :downloads
end
