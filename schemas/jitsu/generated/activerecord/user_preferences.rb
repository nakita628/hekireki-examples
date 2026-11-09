class UserPreferences < ApplicationRecord
  self.table_name = "UserPreferences"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :preferences, exclusion: { in: [ nil ] }

  belongs_to :user, class_name: "UserProfile", foreign_key: "userId", inverse_of: :user_preferences
  belongs_to :workspace, foreign_key: "workspaceId", optional: true, inverse_of: :preferences
end
