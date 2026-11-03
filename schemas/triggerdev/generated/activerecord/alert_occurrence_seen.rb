class AlertOccurrenceSeen < ApplicationRecord
  self.table_name = "AlertOccurrenceSeen"
  self.primary_key = %w[alertId sourceEventId]

  validates :rowTime, presence: true
  validates :expiresAt, presence: true
end
