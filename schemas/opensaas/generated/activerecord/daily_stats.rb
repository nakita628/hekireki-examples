class DailyStats < ApplicationRecord
  self.table_name = "DailyStats"

  attribute :totalViews, default: 0
  attribute :prevDayViewsChangePercent, default: "0"
  attribute :userCount, default: 0
  attribute :paidUserCount, default: 0
  attribute :userDelta, default: 0
  attribute :paidUserDelta, default: 0
  attribute :totalRevenue, default: 0
  attribute :totalProfit, default: 0

  validates :date, uniqueness: true

  has_many :sources, class_name: "PageViewSource", foreign_key: "dailyStatsId", inverse_of: :daily_stats,
                     dependent: :nullify
end
