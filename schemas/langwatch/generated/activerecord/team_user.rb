class TeamUser < ApplicationRecord
  self.table_name = "TeamUser"
  self.primary_key = %w[userId teamId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { admin: "ADMIN", member: "MEMBER", viewer: "VIEWER", custom: "CUSTOM" }, validate: true

  belongs_to :assigned_role, class_name: "CustomRole", foreign_key: "assignedRoleId", optional: true,
                             inverse_of: :assigned_users
  belongs_to :user, foreign_key: "userId", inverse_of: :team_memberships
  belongs_to :team, foreign_key: "teamId", inverse_of: :members
end
