class Notification < ApplicationRecord
  attribute :sendToApp, default: false
  attribute :sendToEmail, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true
  validates :message, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :notifications
  belongs_to :integration, foreign_key: "integrationId", optional: true, inverse_of: :notifications
  belongs_to :notification_rule, foreign_key: "notificationRuleId", optional: true, inverse_of: :notifications
end
