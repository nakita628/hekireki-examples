class ScheduledEvent < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :all_day, default: false
  attribute :sequence, default: 0
  attribute :hide_attendees, default: false

  enum :status, { confirmed: "confirmed", canceled: "canceled", unconfirmed: "unconfirmed" }, default: :confirmed,
                                                                                              validate: true

  validates :sheet_slot_id, uniqueness: { allow_nil: true }
  validates :title, presence: true
  validates :start, presence: true
  validates :end, presence: true
  validates :uid, presence: true, uniqueness: true

  belongs_to :user, inverse_of: :scheduled_events
  belongs_to :space, inverse_of: :scheduled_events
  belongs_to :event_type, optional: true, inverse_of: :scheduled_events
  belongs_to :sheet_slot, optional: true
  has_many :rescheduled_dates, class_name: "RescheduledEventDate", inverse_of: :scheduled_event, dependent: :destroy
  has_many :invites, class_name: "ScheduledEventInvite", inverse_of: :scheduled_event, dependent: :destroy
  has_many :polls, dependent: :nullify
end
