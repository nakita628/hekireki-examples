class Availability < ApplicationRecord
  self.table_name = "Availability"

  attribute :days, default: -> { [] }

  validates :startTime, presence: true
  validates :endTime, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :availability
  belongs_to :event_type, foreign_key: "eventTypeId", optional: true, inverse_of: :availability
  belongs_to :schedule, foreign_key: "scheduleId", optional: true, inverse_of: :availability
end
