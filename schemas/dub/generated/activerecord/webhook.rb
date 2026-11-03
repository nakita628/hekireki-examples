class Webhook < ApplicationRecord
  self.table_name = "Webhook"

  attribute :consecutiveFailures, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :receiver, { user: "user", zapier: "zapier", make: "make", slack: "slack", segment: "segment" }, default: :user,
                                                                                                        validate: true
  enum :linkScope, { workspace: "workspace", folders: "folders", links: "links" }, validate: { allow_nil: true }

  validates :name, presence: true
  validates :url, presence: true
  validates :secret, presence: true
  validates :triggers, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :webhooks
  belongs_to :installed_integration, foreign_key: "installationId", optional: true, inverse_of: :webhooks
  has_many :links, class_name: "LinkWebhook", foreign_key: "webhookId", inverse_of: :webhook, dependent: :destroy
  has_many :folders, class_name: "FolderWebhook", foreign_key: "webhookId", inverse_of: :webhook, dependent: :destroy
end
