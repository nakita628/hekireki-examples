class LiteLLMDailyGatewayRequests < ApplicationRecord
  self.table_name = "LiteLLM_DailyGatewayRequests"
  self.primary_key = %w[date category route]

  attribute :successful_requests, default: 0
  attribute :failed_requests, default: 0
end
