class Payment < ApplicationRecord
  self.table_name = "Payment"

  enum :paymentOption, { on_booking: "ON_BOOKING", hold: "HOLD" }, default: :on_booking, validate: { allow_nil: true }

  validates :uid, presence: true, uniqueness: true
  validates :amount, presence: true
  validates :fee, presence: true
  validates :currency, presence: true
  validates :success, inclusion: { in: [ true, false ] }
  validates :refunded, inclusion: { in: [ true, false ] }
  validates :data, exclusion: { in: [ nil ] }
  validates :externalId, presence: true, uniqueness: true

  belongs_to :app, foreign_key: "appId", optional: true, inverse_of: :payments
  belongs_to :booking, foreign_key: "bookingId", optional: true, inverse_of: :payment
end
