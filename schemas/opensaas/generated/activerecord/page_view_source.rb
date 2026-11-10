class PageViewSource < ApplicationRecord
  self.table_name = "PageViewSource"
  self.primary_key = %w[date name]

  validates :visitors, presence: true

  belongs_to :daily_stats, foreign_key: "dailyStatsId", optional: true, inverse_of: :sources
end
