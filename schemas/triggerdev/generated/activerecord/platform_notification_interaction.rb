class PlatformNotificationInteraction < ApplicationRecord
  self.table_name = "PlatformNotificationInteraction"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :showCount, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :notificationId, uniqueness: { scope: :userId }

  belongs_to :notification, class_name: "PlatformNotification", foreign_key: "notificationId", inverse_of: :interactions
  belongs_to :user, foreign_key: "userId", inverse_of: :platform_notification_interactions
end
