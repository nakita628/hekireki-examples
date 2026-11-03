class TeamUser < ApplicationRecord
  self.table_name = "team_user"
  self.primary_key = "team_user_id"

  validates :role, presence: true, length: { maximum: 50 }

  belongs_to :team, inverse_of: :members
  belongs_to :user, inverse_of: :teams
end
