class LiteLLMClaudeCodePluginTable < ApplicationRecord
  self.table_name = "LiteLLM_ClaudeCodePluginTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :files_json, default: "{}"
  attribute :enabled, default: true
  attribute :anonymous_download_count, default: 0

  validates :name, presence: true, uniqueness: true

  has_one :uploaded_archive, class_name: "LiteLLMClaudeCodePluginUpload", foreign_key: "plugin_name",
                             primary_key: "name", inverse_of: :plugin, dependent: :destroy
  has_many :versions, class_name: "LiteLLMClaudeCodePluginVersion", foreign_key: "plugin_id", inverse_of: :plugin,
                      dependent: :destroy
  has_many :downloads, class_name: "LiteLLMClaudeCodePluginDownload", foreign_key: "plugin_id", inverse_of: :plugin,
                       dependent: :destroy
end
