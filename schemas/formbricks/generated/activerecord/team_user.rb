class TeamUser < ApplicationRecord
  self.table_name = "TeamUser"
  self.primary_key = %w[teamId userId]

  enum :role, { admin: "admin", contributor: "contributor" }, validate: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :team_users
  belongs_to :user, foreign_key: "userId", inverse_of: :team_users
end
