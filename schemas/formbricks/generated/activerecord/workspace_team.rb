class WorkspaceTeam < ApplicationRecord
  self.table_name = "WorkspaceTeam"
  self.primary_key = %w[workspaceId teamId]

  enum :permission, { read: "read", read_write: "readWrite", manage: "manage" }, default: :read, validate: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :workspace_teams
  belongs_to :team, foreign_key: "teamId", inverse_of: :workspace_teams
end
