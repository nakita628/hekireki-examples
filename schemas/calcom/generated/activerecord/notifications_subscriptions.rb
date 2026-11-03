class NotificationsSubscriptions < ApplicationRecord
  self.table_name = "NotificationsSubscriptions"

  validates :subscription, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :notifications_subscriptions
end
