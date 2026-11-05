class Integration < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :config, exclusion: { in: [ nil ] }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :integrations
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :integrations
  has_many :notifications, foreign_key: "integrationId", inverse_of: :integration, dependent: :destroy
  has_many :export_watermarks, foreign_key: "integrationId", inverse_of: :integration, dependent: :destroy
  has_and_belongs_to_many :notification_rules, join_table: "_IntegrationToNotificationRule", foreign_key: "A",
                                               association_foreign_key: "B"
end
