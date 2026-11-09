class WorkspaceUserProperties < ApplicationRecord
  self.table_name = "WorkspaceUserProperties"
  self.primary_key = %w[workspaceId userId]

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :workspace_user_properties
  belongs_to :user, class_name: "UserProfile", foreign_key: "userId", inverse_of: :workspace_user_properties
end
