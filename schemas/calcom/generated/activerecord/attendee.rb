class Attendee < ApplicationRecord
  self.table_name = "Attendee"

  attribute :locale, default: "en"
  attribute :noShow, default: false

  validates :email, presence: true
  validates :name, presence: true
  validates :timeZone, presence: true

  belongs_to :booking, foreign_key: "bookingId", optional: true, inverse_of: :attendees
  has_one :booking_seat, foreign_key: "attendeeId", inverse_of: :attendee, dependent: :destroy
end
