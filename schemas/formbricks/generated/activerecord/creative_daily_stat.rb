class CreativeDailyStat < ApplicationRecord
  self.table_name = "CreativeDailyStat"
  self.primary_key = %w[creativeId date deviceType]

  attribute :impressions, default: 0
  attribute :clicks, default: 0

  enum :deviceType, { phone: "phone", tablet: "tablet", desktop: "desktop" }, validate: true

  belongs_to :creative, foreign_key: "creativeId", inverse_of: :daily_stats
end
