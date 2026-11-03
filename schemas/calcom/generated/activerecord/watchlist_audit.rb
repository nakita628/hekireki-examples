class WatchlistAudit < ApplicationRecord
  self.table_name = "WatchlistAudit"
  self.inheritance_column = nil

  attribute :id, default: -> { SecureRandom.uuid_v7 }

  enum :type, { email: "EMAIL", domain: "DOMAIN", username: "USERNAME" }, validate: true
  enum :action, { report: "REPORT", block: "BLOCK", alert: "ALERT" }, default: :report, validate: true

  validates :value, presence: true

  belongs_to :watchlist, foreign_key: "watchlistId", optional: true, inverse_of: :audits
end
