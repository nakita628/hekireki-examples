class UserTeam < ApplicationRecord
  self.table_name = "UserTeam"
  self.primary_key = %w[userId teamId]

  attribute :status, default: "ACTIVE"

  enum :role, { admin: "ADMIN", manager: "MANAGER", member: "MEMBER", dataroom_member: "DATAROOM_MEMBER" },
       default: :member, validate: true

  belongs_to :user, foreign_key: "userId", inverse_of: :teams
  belongs_to :team, foreign_key: "teamId", inverse_of: :users
end
