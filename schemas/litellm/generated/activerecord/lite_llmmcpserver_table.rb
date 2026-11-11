class LiteLLMMCPServerTable < ApplicationRecord
  self.table_name = "LiteLLM_MCPServerTable"
  self.primary_key = "server_id"
  self.implicit_order_column = "created_at"

  attribute :server_id, default: -> { SecureRandom.uuid }
  attribute :transport, default: "sse"
  attribute :credentials, default: -> { JSON.parse("{}") }
  attribute :mcp_info, default: -> { JSON.parse("{}") }
  attribute :mcp_access_groups, default: -> { [] }
  attribute :allowed_tools, default: -> { [] }
  attribute :tool_name_to_display_name, default: -> { JSON.parse("{}") }
  attribute :tool_name_to_description, default: -> { JSON.parse("{}") }
  attribute :pinned_tools, default: -> { JSON.parse("{}") }
  attribute :extra_headers, default: -> { [] }
  attribute :static_headers, default: -> { JSON.parse("{}") }
  attribute :env_vars, default: -> { JSON.parse("[]") }
  attribute :status, default: "unknown"
  attribute :args, default: -> { [] }
  attribute :env, default: -> { JSON.parse("{}") }
  attribute :allow_all_keys, default: false
  attribute :available_on_public_internet, default: true
  attribute :delegate_auth_to_upstream, default: false
  attribute :oauth_passthrough, default: false
  attribute :per_server_oauth_discovery, default: false
  attribute :is_byok, default: false
  attribute :byok_description, default: -> { [] }
  attribute :approval_status, default: "active"
end
