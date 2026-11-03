class SelectedSlots < ApplicationRecord
  self.table_name = "SelectedSlots"

  attribute :isSeat, default: false

  validates :eventTypeId, presence: true
  validates :userId, presence: true, uniqueness: { scope: [ :slotUtcStartDate, :slotUtcEndDate, :uid ] }
  validates :slotUtcStartDate, presence: true
  validates :slotUtcEndDate, presence: true
  validates :uid, presence: true
  validates :releaseAt, presence: true
end
