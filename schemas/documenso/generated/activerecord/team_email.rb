class TeamEmail < ApplicationRecord
  self.table_name = "TeamEmail"
  self.primary_key = "teamId"

  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :email, presence: true, uniqueness: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :team_email
end
