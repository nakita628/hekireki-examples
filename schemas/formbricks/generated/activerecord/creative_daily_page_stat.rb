class CreativeDailyPageStat < ApplicationRecord
  self.table_name = "CreativeDailyPageStat"
  self.primary_key = %w[creativeId date page]

  attribute :impressions, default: 0
  attribute :clicks, default: 0

  belongs_to :creative, foreign_key: "creativeId", inverse_of: :daily_page_stats
end
