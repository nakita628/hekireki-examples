class UserNotificationPreferences < ApplicationRecord
  self.table_name = "UserNotificationPreferences"

  attribute :id, default: -> { Cuid.generate }
  attribute :dubLinks, default: true

  validates :userId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :notification_preferences
end
