class UserFeatures < ApplicationRecord
  self.table_name = "UserFeatures"
  self.primary_key = %w[userId featureId]

  alias_attribute :updated_at, :updatedAt

  validates :enabled, inclusion: { in: [ true, false ] }
  validates :assignedBy, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :features
  belongs_to :feature, foreign_key: "featureId", inverse_of: :users
end
