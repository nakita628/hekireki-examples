class SeatHold < ApplicationRecord
  self.table_name = "SeatHold"

  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true, uniqueness: { scope: :userId }
  validates :userId, presence: true
  validates :seatTakenAt, presence: true
  validates :heldUntil, presence: true
  validates :reason, presence: true
end
