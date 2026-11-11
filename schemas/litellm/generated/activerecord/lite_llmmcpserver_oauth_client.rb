class LiteLLMMCPServerOAuthClient < ApplicationRecord
  self.table_name = "LiteLLM_MCPServerOAuthClient"
  self.primary_key = "server_id"
end
