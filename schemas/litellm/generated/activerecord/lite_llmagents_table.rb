class LiteLLMAgentsTable < ApplicationRecord
  self.table_name = "LiteLLM_AgentsTable"
  self.primary_key = "agent_id"
  self.implicit_order_column = "created_at"

  attribute :agent_id, default: -> { SecureRandom.uuid }
  attribute :static_headers, default: -> { JSON.parse("{}") }
  attribute :extra_headers, default: -> { [] }
  attribute :agent_access_groups, default: -> { [] }
  attribute :access_group_ids, default: -> { [] }
  attribute :spend, default: 0
  attribute :identity_managed, default: false
  attribute :enabled, default: true
  attribute :execution_mode, default: "autonomous"

  validates :agent_name, presence: true, uniqueness: true
  validates :agent_card_params, exclusion: { in: [ nil ] }
  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true, inverse_of: :agents_table
  has_one :identity, class_name: "LiteLLMAgentIdentity", foreign_key: "agent_id", inverse_of: :agent,
                     dependent: :destroy
  has_many :retired_identities, class_name: "LiteLLMRetiredAgentIdentity", foreign_key: "agent_id", inverse_of: :agent,
                                dependent: :nullify
end
