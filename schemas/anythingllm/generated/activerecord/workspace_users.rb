class WorkspaceUsers < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  belongs_to :workspaces, foreign_key: "workspace_id", inverse_of: :workspace_users
  belongs_to :users, foreign_key: "user_id", inverse_of: :workspace_users
end
