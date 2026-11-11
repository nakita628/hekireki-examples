class LiteLLMProxyWorkerHeartbeat < ApplicationRecord
  self.table_name = "LiteLLM_ProxyWorkerHeartbeat"
  self.primary_key = "worker_id"

  validates :hostname, presence: true
end
