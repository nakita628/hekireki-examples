class ApiKeyWorkspace < ApplicationRecord
  self.table_name = "ApiKeyWorkspace"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :permission, { read: "read", write: "write", manage: "manage" }, validate: true

  validates :apiKeyId, uniqueness: { scope: :workspaceId }

  belongs_to :api_key, foreign_key: "apiKeyId", inverse_of: :api_key_workspaces
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :api_key_workspaces
end
