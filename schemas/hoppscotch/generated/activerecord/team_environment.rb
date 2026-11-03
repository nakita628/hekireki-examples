class TeamEnvironment < ApplicationRecord
  self.table_name = "TeamEnvironment"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true
  validates :variables, exclusion: { in: [ nil ] }

  belongs_to :team, foreign_key: "teamID", inverse_of: :environments
end
