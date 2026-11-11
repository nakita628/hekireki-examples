class LiteLLMConfigOverrides < ApplicationRecord
  self.table_name = "LiteLLM_ConfigOverrides"
  self.primary_key = "config_type"

  validates :config_value, exclusion: { in: [ nil ] }
end
