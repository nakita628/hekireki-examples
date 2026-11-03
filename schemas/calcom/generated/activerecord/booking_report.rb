class BookingReport < ApplicationRecord
  self.table_name = "BookingReport"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :cancelled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :reason, { spam: "SPAM", dont_know_person: "dont_know_person", other: "OTHER" }, validate: true
  enum :status, { pending: "PENDING", dismissed: "DISMISSED", blocked: "BLOCKED" }, default: :pending, validate: true
  enum :systemStatus, { pending: "PENDING", blocked: "BLOCKED", dismissed: "DISMISSED" }, default: :pending,
                                                                                          prefix: true, validate: true

  validates :bookingUid, uniqueness: true
  validates :bookerEmail, presence: true

  belongs_to :booking, foreign_key: "bookingUid", primary_key: "uid", inverse_of: :report
  belongs_to :reported_by, class_name: "User", foreign_key: "reportedById", optional: true,
                           inverse_of: :reported_bookings
  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", optional: true,
                            inverse_of: :booking_reports
  belongs_to :watchlist, foreign_key: "watchlistId", optional: true, inverse_of: :booking_reports
  belongs_to :global_watchlist, class_name: "Watchlist", foreign_key: "globalWatchlistId", optional: true,
                                inverse_of: :global_booking_reports
end
