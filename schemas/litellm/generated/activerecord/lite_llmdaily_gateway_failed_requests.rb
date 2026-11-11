class LiteLLMDailyGatewayFailedRequests < ApplicationRecord
  self.table_name = "LiteLLM_DailyGatewayFailedRequests"
  self.primary_key = %w[date category route status_code]

  attribute :failed_requests, default: 0
end
