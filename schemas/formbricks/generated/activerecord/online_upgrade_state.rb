class OnlineUpgradeState < ApplicationRecord
  self.table_name = "OnlineUpgradeState"
  self.primary_key = "release"

  attribute :completedPhases, default: -> { [] }
  attribute :surveyCount, default: 0
  alias_attribute :updated_at, :updatedAt

  validates :targetImage, presence: true
  validates :phase, presence: true
end
