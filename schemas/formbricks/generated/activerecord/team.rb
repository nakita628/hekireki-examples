class Team < ApplicationRecord
  self.table_name = "Team"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true, uniqueness: { scope: :organizationId }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :teams
  has_many :team_users, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :workspace_teams, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
end
