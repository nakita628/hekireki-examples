class LiteLLMRetiredAgentIdentity < ApplicationRecord
  self.table_name = "LiteLLM_RetiredAgentIdentity"
  self.primary_key = "binding_id"

  attribute :binding_id, default: -> { SecureRandom.uuid }

  validates :provider, presence: true, uniqueness: { scope: %i[tenant_id client_id] }
  validates :issuer, presence: true
  validates :tenant_id, presence: true
  validates :client_id, presence: true

  belongs_to :agent, class_name: "LiteLLMAgentsTable", optional: true, inverse_of: :retired_identities
end
