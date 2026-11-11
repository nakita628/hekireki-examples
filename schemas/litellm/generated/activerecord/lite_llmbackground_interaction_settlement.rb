class LiteLLMBackgroundInteractionSettlement < ApplicationRecord
  self.table_name = "LiteLLM_BackgroundInteractionSettlement"
  self.primary_key = "interaction_id"

  validates :custom_llm_provider, presence: true
  validates :create_context, exclusion: { in: [ nil ] }
end
