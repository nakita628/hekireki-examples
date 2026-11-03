class BookingSeat < ApplicationRecord
  self.table_name = "BookingSeat"

  validates :referenceUid, presence: true, uniqueness: true
  validates :attendeeId, uniqueness: true

  belongs_to :booking, foreign_key: "bookingId", inverse_of: :seats_references
  belongs_to :attendee, foreign_key: "attendeeId", inverse_of: :booking_seat
end
