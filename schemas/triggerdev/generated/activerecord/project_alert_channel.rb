class ProjectAlertChannel < ApplicationRecord
  self.table_name = "ProjectAlertChannel"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :deduplicationKey, default: -> { Cuid.generate }
  attribute :userProvidedDeduplicationKey, default: false
  attribute :enabled, default: true
  attribute :alertTypes, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { email: "EMAIL", slack: "SLACK", webhook: "WEBHOOK" }, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :deduplicationKey, uniqueness: { scope: :projectId }
  validates :name, presence: true
  validates :properties, exclusion: { in: [ nil ] }

  belongs_to :integration, class_name: "OrganizationIntegration", foreign_key: "integrationId", optional: true,
                           inverse_of: :alert_channels
  belongs_to :project, foreign_key: "projectId", inverse_of: :alert_channels
  has_many :alerts, class_name: "ProjectAlert", foreign_key: "channelId", inverse_of: :channel, dependent: :destroy
  has_many :alert_storages, class_name: "ProjectAlertStorage", foreign_key: "alertChannelId", inverse_of: :alert_channel,
                            dependent: :destroy
end
