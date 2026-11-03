class TeamFeatures < ApplicationRecord
  self.table_name = "TeamFeatures"
  self.primary_key = %w[teamId featureId]

  alias_attribute :updated_at, :updatedAt

  validates :enabled, inclusion: { in: [ true, false ] }
  validates :assignedBy, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :features
  belongs_to :feature, foreign_key: "featureId", inverse_of: :teams
end
