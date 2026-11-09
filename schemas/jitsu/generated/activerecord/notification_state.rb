class NotificationState < ApplicationRecord
  self.table_name = "NotificationState"
  self.primary_key = %w[channelId actorId type tableName]
  self.inheritance_column = nil

  validates :workspaceId, presence: true
  validates :lastNotification, presence: true
  validates :statusChangeId, presence: true
end
