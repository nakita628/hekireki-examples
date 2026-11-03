class TravelSchedule < ApplicationRecord
  self.table_name = "TravelSchedule"

  validates :timeZone, presence: true
  validates :startDate, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :travel_schedules
end
