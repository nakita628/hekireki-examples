class TeamProfile < ApplicationRecord
  self.table_name = "TeamProfile"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: false

  validates :teamId, uniqueness: true

  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :profile
end
