class LiteLLMShadowEvalFunnel < ApplicationRecord
  self.table_name = "LiteLLM_ShadowEvalFunnel"
  self.primary_key = "job_id"

  attribute :not_sampled, default: 0
  attribute :unjudgeable, default: 0
  attribute :shed, default: 0
  attribute :withheld, default: 0
end
