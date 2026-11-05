class Booking < ApplicationRecord
  self.table_name = "Booking"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending_provider_event: "PENDING_PROVIDER_EVENT", confirmed: "CONFIRMED", canceled: "CANCELED", failed: "FAILED" },
       default: :pending_provider_event, validate: true

  validates :guestName, presence: true
  validates :guestEmail, presence: true
  validates :startTime, presence: true
  validates :endTime, presence: true
  validates :cancelTokenHash, presence: true
  validates :idempotencyToken, uniqueness: { scope: :bookingLinkId, allow_nil: true }

  belongs_to :booking_link, foreign_key: "bookingLinkId", inverse_of: :bookings
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :bookings
end
