class BookingDenormalized < ApplicationRecord
  self.table_name = "BookingDenormalized"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { cancelled: "cancelled", accepted: "accepted", rejected: "rejected", pending: "pending", awaiting_host: "awaiting_host" },
       validate: true

  validates :uid, presence: true
  validates :title, presence: true
  validates :startTime, presence: true
  validates :endTime, presence: true
  validates :paid, inclusion: { in: [ true, false ] }
  validates :isTeamBooking, inclusion: { in: [ true, false ] }
end
