class Notification < ApplicationRecord
  self.table_name = "Notification"
  self.inheritance_column = nil

  attribute :id, default: -> { SecureRandom.uuid }

  validates :workspaceId, presence: true
  validates :actorId, presence: true
  validates :type, presence: true
  validates :tableName, presence: true
  validates :channelId, presence: true
  validates :statusChangeId, presence: true
  validates :status, presence: true
end
