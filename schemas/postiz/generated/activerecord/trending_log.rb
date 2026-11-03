class TrendingLog < ApplicationRecord
  self.table_name = "TrendingLog"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :date, presence: true
end
