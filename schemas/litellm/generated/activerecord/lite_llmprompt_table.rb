class LiteLLMPromptTable < ApplicationRecord
  self.table_name = "LiteLLM_PromptTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :version, default: 1
  attribute :environment, default: "development"

  validates :prompt_id, presence: true, uniqueness: { scope: %i[version environment] }
  validates :litellm_params, exclusion: { in: [ nil ] }
end
