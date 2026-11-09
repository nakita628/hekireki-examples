class WorkspaceAccess < ApplicationRecord
  self.table_name = "WorkspaceAccess"
  self.primary_key = %w[userId workspaceId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { owner: "owner", editor: "editor", analyst: "analyst" }, default: :owner, validate: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :workspace_access
  belongs_to :user, class_name: "UserProfile", foreign_key: "userId", inverse_of: :workspace_access
end
