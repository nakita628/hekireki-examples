class LiteLLMUISettings < ApplicationRecord
  self.table_name = "LiteLLM_UISettings"

  attribute :id, default: "ui_settings"

  validates :ui_settings, exclusion: { in: [ nil ] }
end
