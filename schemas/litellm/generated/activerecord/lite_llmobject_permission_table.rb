class LiteLLMObjectPermissionTable < ApplicationRecord
  self.table_name = "LiteLLM_ObjectPermissionTable"
  self.primary_key = "object_permission_id"

  attribute :object_permission_id, default: -> { SecureRandom.uuid }
  attribute :mcp_servers, default: -> { [] }
  attribute :mcp_access_groups, default: -> { [] }
  attribute :vector_stores, default: -> { [] }
  attribute :agents, default: -> { [] }
  attribute :agent_access_groups, default: -> { [] }
  attribute :models, default: -> { [] }
  attribute :blocked_tools, default: -> { [] }
  attribute :mcp_toolsets, default: -> { [] }
  attribute :search_tools, default: -> { [] }
  attribute :skills, default: -> { [] }

  has_many :teams, class_name: "LiteLLMTeamTable", foreign_key: "object_permission_id", inverse_of: :object_permission,
                   dependent: :nullify
  has_many :projects, class_name: "LiteLLMProjectTable", foreign_key: "object_permission_id",
                      inverse_of: :object_permission, dependent: :nullify
  has_many :verification_tokens, class_name: "LiteLLMVerificationToken", foreign_key: "object_permission_id",
                                 inverse_of: :object_permission, dependent: :nullify
  has_many :organizations, class_name: "LiteLLMOrganizationTable", foreign_key: "object_permission_id",
                           inverse_of: :object_permission, dependent: :nullify
  has_many :users, class_name: "LiteLLMUserTable", foreign_key: "object_permission_id", inverse_of: :object_permission,
                   dependent: :nullify
  has_many :end_users, class_name: "LiteLLMEndUserTable", foreign_key: "object_permission_id",
                       inverse_of: :object_permission, dependent: :nullify
  has_many :agents_table, class_name: "LiteLLMAgentsTable", foreign_key: "object_permission_id",
                          inverse_of: :object_permission, dependent: :nullify
end
