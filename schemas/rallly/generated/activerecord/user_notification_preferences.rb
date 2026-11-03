class UserNotificationPreferences < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :prefs, default: -> { JSON.parse("{}") }

  validates :user_id, uniqueness: true

  belongs_to :user, inverse_of: :notification_preferences
end
