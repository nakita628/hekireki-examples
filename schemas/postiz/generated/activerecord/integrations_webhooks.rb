class IntegrationsWebhooks < ApplicationRecord
  self.table_name = "IntegrationsWebhooks"
  self.primary_key = %w[integrationId webhookId]

  validates :integrationId, uniqueness: { scope: :webhookId }

  belongs_to :integration, foreign_key: "integrationId", inverse_of: :webhooks
  belongs_to :webhook, class_name: "Webhooks", foreign_key: "webhookId", inverse_of: :integrations
end
