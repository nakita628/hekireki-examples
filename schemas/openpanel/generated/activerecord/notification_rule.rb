class NotificationRule < ApplicationRecord
  attribute :sendToApp, default: false
  attribute :sendToEmail, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :config, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :notification_rules
  has_many :notifications, foreign_key: "notificationRuleId", inverse_of: :notification_rule, dependent: :destroy
  has_and_belongs_to_many :integrations, join_table: "_IntegrationToNotificationRule", foreign_key: "B",
                                         association_foreign_key: "A"
end
