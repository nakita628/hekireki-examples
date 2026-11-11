class LiteLLMClaudeCodePluginUpload < ApplicationRecord
  self.table_name = "LiteLLM_ClaudeCodePluginUpload"
  self.primary_key = "plugin_name"

  validates :content, exclusion: { in: [ nil ] }

  belongs_to :plugin, class_name: "LiteLLMClaudeCodePluginTable", foreign_key: "plugin_name", primary_key: "name",
                      inverse_of: :uploaded_archive
end
