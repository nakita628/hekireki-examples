class WatchlistEventAudit < ApplicationRecord
  self.table_name = "WatchlistEventAudit"

  attribute :id, default: -> { SecureRandom.uuid_v7 }

  enum :actionTaken, { report: "REPORT", block: "BLOCK", alert: "ALERT" }, validate: true

  validates :watchlistId, presence: true
  validates :eventTypeId, presence: true
end
