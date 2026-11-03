class AlertEvent < ApplicationRecord
  self.table_name = "AlertEvent"
  self.primary_key = %w[id at]
  self.inheritance_column = nil

  attribute :facts, default: -> { JSON.parse("{}") }

  validates :alertId, presence: true
  validates :type, presence: true
end
