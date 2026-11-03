class Schedule < ApplicationRecord
  self.table_name = "Schedule"

  validates :name, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :schedules
  has_many :event_type, class_name: "EventType", foreign_key: "scheduleId", inverse_of: :schedule, dependent: :nullify
  has_many :instant_meeting_events, class_name: "EventType", foreign_key: "instantMeetingScheduleId",
                                    inverse_of: :instant_meeting_schedule, dependent: :nullify
  has_many :restriction_schedule, class_name: "EventType", foreign_key: "restrictionScheduleId",
                                  inverse_of: :restriction_schedule, dependent: :nullify
  has_many :availability, class_name: "Availability", foreign_key: "scheduleId", inverse_of: :schedule,
                          dependent: :nullify
  has_many :host, class_name: "Host", foreign_key: "scheduleId", inverse_of: :schedule, dependent: :nullify
end
