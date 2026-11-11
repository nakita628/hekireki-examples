class LiteLLMAgentIdentity < ApplicationRecord
  self.table_name = "LiteLLM_AgentIdentity"
  self.primary_key = "agent_id"

  attribute :active, default: true
  attribute :required_roles, default: -> { [] }
  attribute :required_scopes, default: -> { %w[user_impersonation] }
  attribute :revision, default: -> { SecureRandom.uuid }

  validates :provider, presence: true, uniqueness: { scope: %i[tenant_id client_id] }
  validates :issuer, presence: true,
                     uniqueness: { scope: :service_principal_id, conditions: -> { where.not(service_principal_id: nil) } }
  validates :tenant_id, presence: true
  validates :client_id, presence: true

  belongs_to :agent, class_name: "LiteLLMAgentsTable", inverse_of: :identity
end
