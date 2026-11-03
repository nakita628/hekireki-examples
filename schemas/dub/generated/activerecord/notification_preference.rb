class NotificationPreference < ApplicationRecord
  self.table_name = "NotificationPreference"

  attribute :id, default: -> { Cuid.generate }
  attribute :linkUsageSummary, default: true
  attribute :domainConfigurationUpdates, default: true

  validates :projectUserId, uniqueness: true

  belongs_to :project_user, class_name: "ProjectUsers", foreign_key: "projectUserId",
                            inverse_of: :notification_preference
end
