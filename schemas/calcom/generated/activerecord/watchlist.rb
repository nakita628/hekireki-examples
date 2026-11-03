class Watchlist < ApplicationRecord
  self.table_name = "Watchlist"
  self.inheritance_column = nil

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isGlobal, default: false

  enum :type, { email: "EMAIL", domain: "DOMAIN", username: "USERNAME" }, validate: true
  enum :action, { report: "REPORT", block: "BLOCK", alert: "ALERT" }, default: :report, validate: true
  enum :source, { manual: "MANUAL", free_domain_policy: "FREE_DOMAIN_POLICY", signup: "SIGNUP" }, default: :manual,
                                                                                                  validate: true

  validates :value, presence: true,
                    uniqueness: { scope: [ :type, :organizationId ], conditions: -> { where.not(organizationId: nil) } }

  has_many :booking_reports, foreign_key: "watchlistId", inverse_of: :watchlist, dependent: :nullify
  has_many :global_booking_reports, class_name: "BookingReport", foreign_key: "globalWatchlistId",
                                    inverse_of: :global_watchlist, dependent: :nullify
  has_many :audits, class_name: "WatchlistAudit", foreign_key: "watchlistId", inverse_of: :watchlist,
                    dependent: :nullify
end
