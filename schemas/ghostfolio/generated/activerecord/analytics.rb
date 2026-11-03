class Analytics < ApplicationRecord
  self.table_name = "Analytics"
  self.primary_key = "userId"

  attribute :activityCount, default: 0
  attribute :dataProviderGhostfolioDailyRequests, default: 0
  alias_attribute :updated_at, :updatedAt

  enum :lastRequestClient, { api: "API", mcp: "MCP", self_hosting: "SELF_HOSTING", web: "WEB" },
       validate: { allow_nil: true }

  belongs_to :user, foreign_key: "userId", inverse_of: :analytics
end
