class LiteLLMSSOConfig < ApplicationRecord
  self.table_name = "LiteLLM_SSOConfig"

  attribute :id, default: "sso_config"

  validates :sso_settings, exclusion: { in: [ nil ] }
end
