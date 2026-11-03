class RescheduledEventDate < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :all_day, default: false

  validates :start, presence: true
  validates :end, presence: true

  belongs_to :scheduled_event, inverse_of: :rescheduled_dates
end
