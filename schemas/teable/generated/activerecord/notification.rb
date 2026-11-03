class Notification < ApplicationRecord
  self.table_name = "notification"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  attribute :severity, default: "info"
  attribute :is_read, default: false

  validates :from_user_id, presence: true
  validates :to_user_id, presence: true
  validates :type, presence: true
  validates :message, presence: true
  validates :created_by, presence: true
end
