class Tracking < ApplicationRecord
  self.table_name = "Tracking"

  validates :bookingId, uniqueness: true
  validates :bookingId, uniqueness: true

  belongs_to :booking, foreign_key: "bookingId", inverse_of: :tracking
end
