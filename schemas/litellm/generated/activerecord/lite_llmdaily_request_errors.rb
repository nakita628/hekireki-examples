class LiteLLMDailyRequestErrors < ApplicationRecord
  self.table_name = "LiteLLM_DailyRequestErrors"
  self.primary_key = %w[date api_key team_id user_id model_group status_code]

  attribute :team_id, default: ""
  attribute :user_id, default: ""
  attribute :model_group, default: ""
  attribute :failed_requests, default: 0
end
