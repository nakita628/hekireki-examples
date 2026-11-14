class GatewayTraceExportKey < ApplicationRecord
  self.table_name = "GatewayTraceExportKey"
  self.primary_key = "projectId"

  alias_attribute :created_at, :createdAt

  validates :apiKeyId, presence: true
  validates :encryptedToken, presence: true
end
