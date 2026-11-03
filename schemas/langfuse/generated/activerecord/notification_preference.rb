class NotificationPreference < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true

  enum :channel, { email: "EMAIL" }, validate: true
  enum :type, { comment_mention: "COMMENT_MENTION" }, validate: true

  validates :user_id, uniqueness: { scope: %i[project_id channel type] }

  belongs_to :user, inverse_of: :notification_preferences
  belongs_to :project, inverse_of: :notification_preference
end
