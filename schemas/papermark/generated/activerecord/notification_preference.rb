class NotificationPreference < ApplicationRecord
  self.table_name = "NotificationPreference"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :frequency, default: "IMMEDIATE"
  attribute :scope, default: "ALL"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :type, presence: true, uniqueness: { scope: [ :userId, :teamId ] }

  belongs_to :user, foreign_key: "userId", inverse_of: :notification_preferences
  belongs_to :team, foreign_key: "teamId", inverse_of: :notification_preferences
end
