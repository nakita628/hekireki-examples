class Webhooks < ApplicationRecord
  self.table_name = "Webhooks"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :url, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :webhooks
  has_many :integrations, class_name: "IntegrationsWebhooks", foreign_key: "webhookId", inverse_of: :webhook,
                          dependent: :restrict_with_error
end
