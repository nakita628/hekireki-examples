class LiteLLMConfig < ApplicationRecord
  self.table_name = "LiteLLM_Config"
  self.primary_key = "param_name"

  attribute :reload_revision, default: 0
end
