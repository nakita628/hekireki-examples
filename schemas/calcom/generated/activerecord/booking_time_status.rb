class BookingTimeStatus < ApplicationRecord
  self.table_name = "BookingTimeStatus"

  alias_attribute :created_at, :createdAt

  enum :status, { cancelled: "cancelled", accepted: "accepted", rejected: "rejected", pending: "pending", awaiting_host: "awaiting_host" },
       validate: { allow_nil: true }

  validates :id, presence: true, uniqueness: true
  validates :isTeamBooking, inclusion: { in: [ true, false ] }
end
