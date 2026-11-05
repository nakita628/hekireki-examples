class AvailabilityWindow < ApplicationRecord
  self.table_name = "AvailabilityWindow"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :weekday, presence: true
  validates :startMinutes, presence: true
  validates :endMinutes, presence: true

  belongs_to :availability_schedule, foreign_key: "availabilityScheduleId", inverse_of: :windows
end
